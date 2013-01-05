﻿#region Copyright (C) 2011-2012 MPExtended
// Copyright (C) 2011-2012 MPExtended Developers, http://mpextended.github.com/
// 
// MPExtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MPExtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MPExtended. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using MPExtended.Applications.ServiceConfigurator.Code;
using MPExtended.Libraries.Service;
using MPExtended.Libraries.Service.Strings;
using MPExtended.Services.UserSessionService.Interfaces;

namespace MPExtended.Applications.ServiceConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mIsAppExiting = false;
        private ServiceController mServiceController;
        private DispatcherTimer mServiceWatcher;
        private int lastTabIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            Log.Debug("MPExtended.Applications.ServiceConfigurator starting...");

            // tray application 
            UserServices.Setup(host: true);

            if (StartupArguments.RunAsTrayApp && !StartupArguments.OpenOnStart)
                Hide();

            HandleMediaPortalState(UserServices.USS.IsMediaPortalRunning());

            if (!Installation.IsProductInstalled(MPExtendedProduct.WebMediaPortal))
            {
                taskbarItemContextMenu.Items.Remove(MenuOpenWebMP);
            }

            // service controller
            InitServiceController();

            // hide tabs not applicable for current situation
            if (!Installation.IsServiceInstalled(MPExtendedService.MediaAccessService))
            {
                tcMainTabs.Items.Remove(tiPlugin);
                tcMainTabs.Items.Remove(tiSocial);
            }
            if (!Installation.IsServiceInstalled(MPExtendedService.StreamingService))
            {
                tcMainTabs.Items.Remove(tiStreaming);
                tcMainTabs.Items.Remove(tiSocial);
            }
            if (!Installation.IsProductInstalled(MPExtendedProduct.WebMediaPortal) || !IsWebMediaPortalServiceAvailable())
            {
                tcMainTabs.Items.Remove(tiWebMediaPortal);
            }

            // initialize some tabs
            Pages.TabConfiguration.StartLoadingTranslations();
        }

        private void CloseTab()
        {
            try
            {
                if (lastTabIndex >= 0 && ((tcMainTabs.Items[lastTabIndex] as TabItem).Content as Frame).Content is ITabCloseCallback)
                {
                    (((tcMainTabs.Items[lastTabIndex] as TabItem).Content as Frame).Content as ITabCloseCallback).TabClosed();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to handle TabClosed() callback when leaving tab", ex);
                ErrorHandling.OnlyShowError(ex);
            }
        }

        /// <summary>
        /// Loads the pages into the content
        /// </summary>
        private void tcMainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lastTabIndex != tcMainTabs.SelectedIndex)
                {
                    CloseTab();
                    TabItem item = tcMainTabs.SelectedItem as TabItem;
                    Frame f = new Frame();
                    f.Source = new Uri((string)item.Tag, UriKind.Relative);
                    item.Content = f;
                    lastTabIndex = tcMainTabs.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to change tab", ex);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            CommonEventHandlers.NavigateHyperlink(sender, e);
        }

        private bool IsWebMediaPortalServiceAvailable()
        {
            try
            {
                ServiceController controller = new ServiceController("MPExtended WebMediaPortal");
                string tmp = controller.DisplayName;
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }


        #region Tray application
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // deactive the current tab too, as this is a close action of the user and should have the same effect as selecting another tab
                CloseTab();
                this.Hide();

                // exit when we aren't running as tray app
                if (!StartupArguments.RunAsTrayApp)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to handle OK click event", ex);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // When the application is closed, check whether the application is exiting from menu or forms close button
            if (!mIsAppExiting && StartupArguments.RunAsTrayApp)
            {
                // If the form close button is triggered, cancel the event and hide the form.
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                // When we exit the app, make sure to unload the tab and stop USS.
                UserServices.Shutdown();
                CloseTab();
            }
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            mIsAppExiting = true;
            this.Close();
        }

        private void MenuStartCloseMp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!UserServices.USS.IsMediaPortalRunning())
                {
                    UserServices.USS.StartMediaPortal();
                    HandleMediaPortalState(true);
                }
                else
                {
                    UserServices.USS.CloseMediaPortal();
                    HandleMediaPortalState(false);
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Exception while trying to start/stop MediaPortal", ex);
            }
        }

        private void HandleMediaPortalState(bool _running)
        {
            if (_running)
            {
                MenuStartCloseMp.Header = UI.TrayCloseMediaPortal;
            }
            else
            {
                MenuStartCloseMp.Header = UI.TrayStartMediaPortal;
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            HandleMediaPortalState(UserServices.USS.IsMediaPortalRunning());
        }

        private void MenuOpenConfigurator_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void TaskbarIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void MenuPowermodeLogoff_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.LogOff);
        }

        private void MenuPowermodeSuspend_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.Suspend);
        }

        private void MenuPowermodeHibernate_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.Hibernate);
        }

        private void MenuPowermodeReboot_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.Reboot);
        }

        private void MenuPowermodeShutdown_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.PowerOff);
        }

        private void MenuPowermodeLock_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.Lock);
        }

        private void MenuPowermodeMonitorOff_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.ScreenOff);
        }

        private void MenuPowermodeScreensaverOn_Click(object sender, RoutedEventArgs e)
        {
            UserServices.USS.SetPowerMode(WebPowerMode.Screensaver);
        }

        private void MenuOpenWebMP_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://localhost:" + Configuration.WebMediaPortalHosting.Port));
        }
        #endregion

        #region Service controller
        /// <summary>
        /// Initialize the service watcher.
        /// </summary>
        private void InitServiceController()
        {
            // try to load service
            try
            {
                mServiceController = new ServiceController("MPExtended Service");
                HandleServiceState(mServiceController.Status);

                // start service watcher
                btnStartStopService.IsEnabled = true;
                mServiceWatcher = new DispatcherTimer();
                mServiceWatcher.Interval = TimeSpan.FromSeconds(2);
                mServiceWatcher.Tick += serviceWatcher_Tick;
                mServiceWatcher.Start();
            }
            catch (InvalidOperationException)
            {
                // service not installed
                mServiceController = null;
                lblServiceState.Content = UI.ServiceNotInstalled;
                btnStartStopService.IsEnabled = false;
                if (Installation.GetFileLayoutType() != FileLayoutType.Source)
                {
                    Log.Error("MPExtended Service not installed");
                    MessageBox.Show(UI.ServiceNotInstalledPopup, "MPExtended", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void serviceWatcher_Tick(object sender, EventArgs e)
        {
            try
            {
                mServiceController.Refresh();
                HandleServiceState(mServiceController.Status);
            }
            catch (Exception ex)
            {
                ErrorHandling.ShowError(ex);
                mServiceWatcher.Stop();
            }
        }

        private void HandleServiceState(ServiceControllerStatus _status)
        {
            switch (_status)
            {
                case ServiceControllerStatus.Stopped:
                    btnStartStopService.Content = UI.Start;
                    lblServiceState.Content = UI.ServiceStopped;
                    lblServiceState.Foreground = Brushes.Red;
                    break;
                case ServiceControllerStatus.Running:
                    btnStartStopService.Content = UI.Stop;
                    lblServiceState.Content = UI.ServiceStarted;
                    lblServiceState.Foreground = Brushes.Green;
                    break;
                case ServiceControllerStatus.StartPending:
                    btnStartStopService.Content = UI.Stop;
                    lblServiceState.Content = UI.ServiceStartingFixed;
                    lblServiceState.Foreground = Brushes.Teal;
                    break;
                default:
                    lblServiceState.Foreground = Brushes.Teal;
                    lblServiceState.Content = UI.ServiceUnknown;
                    break;
            }
        }

        private void btnStartStopService_Click(object sender, RoutedEventArgs e)
        {
            Log.Debug("StartStopService: inside btnStartStopService_Click()");
            if (!UacServiceHelper.IsAdmin())
            {
                Log.Debug("StartStopService: no admin rights, use UacServiceHandler");
                switch (mServiceController.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        UacServiceHelper.StartService();
                        break;
                    case ServiceControllerStatus.Running:
                        UacServiceHelper.StopService();
                        break;
                }
            }
            else
            {
                Log.Debug("StartStopService: have admin rights, start/stop ourselves");
                switch (mServiceController.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        mServiceController.Start();
                        break;
                    case ServiceControllerStatus.Running:
                        mServiceController.Stop();
                        break;

                }
            }
        }
        #endregion
    }
}
