#region Copyright (C) 2011-2013 MPExtended, 2010-2011 TV4Home
// Copyright (C) 2010-2011 TV4Home, http://tv4home.codeplex.com/
// Copyright (C) 2011-2013 MPExtended Developers, http://www.mpextended.com/
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.ServiceModel;
using MPExtended.Services.Common.Interfaces;
using MPExtended.Services.TVAccessService.Interfaces;

namespace MPExtended.PlugIns.TAS.TVE35
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
    [Export(typeof(ITVAccessService))]
    [ExportMetadata("Name", "TVE35 Plugin")]
    [ExportMetadata("Id", 2)]
    public class TVPlugin : ITVAccessService
    {
        public WebTVServiceDescription GetServiceDescription()
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult TestConnectionToTVService()
        {
            return true;
        }

        public WebStringResult ReadSettingFromDatabase(string tagName)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult WriteSettingToDatabase(string tagName, string value)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebDiskSpaceInformation> GetLocalDiskInformation(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebTVSearchResult> Search(string text, WebTVSearchResultType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public WebDictionary<string> GetExternalMediaInfo(WebMediaType? type, string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebTVSearchResult> SearchResultsByRange(string text, int start, int end, WebTVSearchResultType? type = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebCard> GetCards(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebVirtualCard> GetActiveCards(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebUser> GetActiveUsers(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebRtspClient> GetStreamingClients(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebDiskSpaceInformation> GetAllRecordingDiskInformation(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public WebDiskSpaceInformation GetRecordingDiskInformationForCard(int id)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult StartRecordingManual(string userName, int channelId, string title)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult AddSchedule(int channelId, string title, System.DateTime startTime, System.DateTime endTime, WebScheduleType scheduleType)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult AddScheduleDetailed(int channelId, string title, System.DateTime startTime, System.DateTime endTime, WebScheduleType scheduleType, int preRecordInterval, int postRecordInterval, string directory, int priority)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetScheduleCount(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebScheduleBasic> GetSchedules(string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebScheduleBasic> GetSchedulesByRange(int start, int end, string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public WebScheduleBasic GetScheduleById(int scheduleId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult CancelSchedule(int programId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult EditSchedule(int scheduleId, int? channelId = null, string title = null, System.DateTime? startTime = null, System.DateTime? endTime = null, WebScheduleType? scheduleType = null, int? preRecordInterval = null, int? postRecordInterval = null, string directory = null, int? priority = null)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult DeleteSchedule(int scheduleId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult StopRecording(int scheduleId)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebScheduledRecording> GetScheduledRecordingsForDate(System.DateTime date, string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebScheduledRecording> GetScheduledRecordingsForToday(string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetRecordingCount(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebRecordingBasic> GetRecordings(string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebRecordingBasic> GetRecordingsByRange(int start, int end, string filter = null, WebSortField? sort = WebSortField.Title, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public WebRecordingBasic GetRecordingById(int id)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult DeleteRecording(int id)
        {
            throw new System.NotImplementedException();
        }

        public WebRecordingFileInfo GetRecordingFileInfo(int id)
        {
            throw new System.NotImplementedException();
        }

        public Stream ReadRecordingFile(int id)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetGroupCount(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelGroup> GetGroups(string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelGroup> GetGroupsByRange(int start, int end, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public WebChannelGroup GetGroupById(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetChannelCount(int? groupId = null, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelBasic> GetChannelsBasic(int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelBasic> GetChannelsBasicByRange(int start, int end, int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelDetailed> GetChannelsDetailed(int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelDetailed> GetChannelsDetailedByRange(int start, int end, int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelState> GetAllChannelStatesForGroup(int groupId, string userName, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetRadioGroupCount(string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelGroup> GetRadioGroups(string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelGroup> GetRadioGroupsByRange(int start, int end, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public WebChannelGroup GetRadioGroupById(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult GetRadioChannelCount(int? groupId = null, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelBasic> GetRadioChannelsBasic(int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelBasic> GetRadioChannelsBasicByRange(int start, int end, int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelDetailed> GetRadioChannelsDetailed(int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelDetailed> GetRadioChannelsDetailedByRange(int start, int end, int? groupId = null, string filter = null, WebSortField? sort = WebSortField.User, WebSortOrder? order = WebSortOrder.Asc)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelState> GetAllRadioChannelStatesForGroup(int groupId, string userName, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public WebChannelBasic GetChannelBasicById(int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebChannelDetailed GetChannelDetailedById(int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebChannelState GetChannelState(int channelId, string userName)
        {
            throw new System.NotImplementedException();
        }

        public WebVirtualCard SwitchTVServerToChannelAndGetVirtualCard(string userName, int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebStringResult SwitchTVServerToChannelAndGetStreamingUrl(string userName, int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebStringResult SwitchTVServerToChannelAndGetTimeshiftFilename(string userName, int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult SendHeartbeat(string userName)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult CancelCurrentTimeShifting(string userName)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramBasic> GetProgramsBasicForChannel(int channelId, System.DateTime startTime, System.DateTime endTime, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramDetailed> GetProgramsDetailedForChannel(int channelId, System.DateTime startTime, System.DateTime endTime, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelPrograms<WebProgramBasic>> GetProgramsBasicForGroup(int groupId, System.DateTime startTime, System.DateTime endTime, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebChannelPrograms<WebProgramDetailed>> GetProgramsDetailedForGroup(int groupId, System.DateTime startTime, System.DateTime endTime, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public WebProgramDetailed GetCurrentProgramOnChannel(int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebProgramDetailed GetNextProgramOnChannel(int channelId)
        {
            throw new System.NotImplementedException();
        }

        public WebIntResult SearchProgramsCount(string searchTerm)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramBasic> SearchProgramsBasic(string searchTerm, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramBasic> SearchProgramsBasicByRange(string searchTerm, int start, int end, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramDetailed> SearchProgramsDetailed(string searchTerm, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramDetailed> SearchProgramsDetailedByRange(string searchTerm, int start, int end, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramBasic> GetNowNextWebProgramBasicForChannel(int channelId, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public IList<WebProgramDetailed> GetNowNextWebProgramDetailedForChannel(int channelId, string filter = null)
        {
            throw new System.NotImplementedException();
        }

        public WebProgramBasic GetProgramBasicById(int programId)
        {
            throw new System.NotImplementedException();
        }

        public WebProgramDetailed GetProgramDetailedById(int programId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult GetProgramIsScheduledOnChannel(int channelId, int programId)
        {
            throw new System.NotImplementedException();
        }

        public WebBoolResult GetProgramIsScheduled(int programId)
        {
            throw new System.NotImplementedException();
        }
    }
}
