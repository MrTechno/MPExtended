﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MPExtended.Libraries.ServiceLib;
using MPExtended.Services.MediaAccessService.Code;
using MPExtended.Services.MediaAccessService.Code.Helper;
using MPExtended.Services.MediaAccessService.Interfaces;

namespace MPExtended.Services.MediaAccessService
{
    public class MediaAccessService : IMediaAccessService
    {

        #region API Constants
        private const int VIDEO_API = 1;
        private const int MUSIC_API = 1;
        private const int PICTURES_API = 1;
        private const int TVSERIES_API = 1;
        private const int MOVING_PICTURES_API = 1;
        private const int STREAMING_API = 1;
        #endregion

        #region MediaPortal Attributes
        public string m_gentleConfig;
        public string m_connStr;
        public Dictionary<String, WebBannerPath> m_thumbPaths;
        private MovingPictures m_movingPictures;
        private MPTvSeries m_mptvseries;
        private MPMusic m_music;
        private MPVideo m_video;
        #endregion

        public MediaAccessService()
        {
            m_thumbPaths = Utils.GetWebBannerPaths();
            m_movingPictures = new MovingPictures();
            m_mptvseries = new MPTvSeries();
            m_music = new MPMusic();
            m_video = new MPVideo();

            WcfUsernameValidator.Init();
        }

        #region MediaPortal
        public WebServiceDescription GetServiceDescription()
        {
            DBLocations db = Utils.GetMPDbLocations();
            WebServiceDescription f = new WebServiceDescription();
            f.SupportsVideos = File.Exists(db.Videos);
            f.VideoApiVersion = VIDEO_API;
            f.SupportsMusic = File.Exists(db.Music);
            f.MusicApiVersion = MUSIC_API;
            f.SupportsPictures = File.Exists(db.Pictures);
            f.PicturesApiVersion = PICTURES_API;
            f.SupportsTvSeries = File.Exists(db.TvSeries);
            f.TvSeriesApiVersion = TVSERIES_API;
            f.SupportsMovingPictures = File.Exists(db.MovingPictures);
            f.MovingPicturesApiVersion = MOVING_PICTURES_API;

            // see notes in GlobalVersion.cs
            f.ServiceVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            return f;
        }

        #region Music
        public List<WebShare> GetAllMusicShares()
        {
            return Shares.GetAllShares(Shares.ShareType.Music);
        }

        public WebMusicTrack GetMusicTrack(int trackId)
        {
            return m_music.GetMusicTrack(trackId);
        }

        public int GetMusicTracksCount()
        {
            return m_music.GetMusicTracksCount();
        }
        private List<WebMusicTrack> SortWebMusicTrackList(List<WebMusicTrack> tracks, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return tracks.OrderByDescending(track => track.Title).ToList();
                    }
                    return tracks.OrderBy(track => track.Title).ToList();
                case SortBy.TrackNumber:
                    if (order == OrderBy.Desc)
                    {
                        return tracks.OrderByDescending(track => track.TrackNumber).ToList();
                    }
                    return tracks.OrderBy(track => track.TrackNumber).ToList();
            }
            return tracks;
        }
        private List<WebMusicArtist> SortWebArtistList(List<WebMusicArtist> artists, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.Desc:
                    return artists.OrderByDescending(artist => artist.Title).ToList();
                case OrderBy.Asc:
                    return artists.OrderBy(artist => artist.Title).ToList();

            }
            return artists;
        }
        private List<WebMusicAlbum> SortWebAlbumList(List<WebMusicAlbum> albums, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return albums.OrderByDescending(album => album.Title).ToList();
                    }
                    return albums.OrderBy(album => album.Title).ToList();
                case SortBy.Genre:
                    if (order == OrderBy.Desc)
                    {
                        return albums.OrderByDescending(album => album.Genre).ToList();
                    }
                    return albums.OrderBy(track => track.Genre).ToList();
                case SortBy.Composer:
                    if (order == OrderBy.Desc)
                    {
                        return albums.OrderByDescending(album => album.Composer).ToList();
                    }
                    return albums.OrderBy(track => track.Composer).ToList();
                case SortBy.Year:
                    if (order == OrderBy.Desc)
                    {
                        return albums.OrderByDescending(album => album.Year).ToList();
                    }
                    return albums.OrderBy(track => track.Year).ToList();

            }
            return albums;
        }

        public List<WebMusicTrack> GetAllMusicTracks(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMusicTrackList(m_music.GetAllMusicTracks(), sort, order);
        }

        public List<WebMusicTrack> GetMusicTracks(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMusicTrackList(m_music.GetAllMusicTracks(), sort, order).GetRange(startIndex, endIndex - startIndex);
        }

        public List<WebMusicAlbum> GetAllAlbums(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebAlbumList(m_music.GetAllAlbums(), sort, order);
        }


        public List<WebMusicAlbum> GetAlbums(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            List<WebMusicAlbum> albums = SortWebAlbumList(m_music.GetAllAlbums(), sort, order).GetRange(startIndex, endIndex - startIndex + 1);
            return albums;
        }


        public int GetAlbumsCount()
        {
            return m_music.GetAlbumsCount();
        }


        public List<WebMusicArtist> GetAllArtists(OrderBy order = OrderBy.Asc)
        {
            return SortWebArtistList(m_music.GetAllArtists(), order);
        }

        public List<WebMusicArtist> GetArtists(int startIndex, int endIndex, OrderBy order = OrderBy.Asc)
        {
            List<WebMusicArtist> artists = SortWebArtistList(m_music.GetAllArtists(), order).GetRange(startIndex, endIndex - startIndex + 1);
            return artists;
        }

        public WebMusicAlbum GetAlbum(string albumArtistName, string albumName)
        {
            return m_music.GetAlbum(albumName, albumArtistName);
        }

        public int GetArtistsCount()
        {
            return m_music.GetArtistsCount();
        }


        public List<WebMusicAlbum> GetAlbumsByArtist(String artistName, SortBy sort = SortBy.Year, OrderBy order = OrderBy.Asc)
        {
            return SortWebAlbumList(m_music.GetAlbumsByArtist(artistName), sort, order);
        }


        public List<WebMusicTrack> GetSongsOfAlbum(String albumName, String albumArtistName, SortBy sort = SortBy.TrackNumber, OrderBy order = OrderBy.Asc)
        {
            return SortWebMusicTrackList(m_music.GetSongsOfAlbum(albumName, albumArtistName), sort, order);
        }


        public List<WebMusicTrack> FindMusicTracks(string album, string artist, string title, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMusicTrackList(m_music.FindMusicTracks(album, artist, title), sort, order);
        }
        #endregion
        #region videos
        public List<WebShare> GetVideoShares()
        {
            return Shares.GetAllShares(Shares.ShareType.Video);
        }

        public int GetVideosCount()
        {
            return m_video.GetVideosCount();
        }

        private List<WebMovie> SortWebMovieList(List<WebMovie> movies, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Title).ToList();
                    }
                    return movies.OrderBy(movie => movie.Title).ToList();
                case SortBy.Genre:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Genre).ToList();
                    }
                    return movies.OrderBy(movie => movie.Genre).ToList();
                case SortBy.Year:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Year).ToList();
                    }
                    return movies.OrderBy(movie => movie.Year).ToList();

            }

            return movies;
        }
        private List<WebMovieFull> SortWebMovieListDetailed(List<WebMovieFull> movies, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Title).ToList();
                    }
                    return movies.OrderBy(movie => movie.Title).ToList();
                case SortBy.Genre:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Genre).ToList();
                    }
                    return movies.OrderBy(movie => movie.Genre).ToList();
                case SortBy.Year:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.Year).ToList();
                    }
                    return movies.OrderBy(movie => movie.Year).ToList();
                case SortBy.DateAdded:
                    if (order == OrderBy.Desc)
                    {
                        return movies.OrderByDescending(movie => movie.DateAdded).ToList();
                    }
                    return movies.OrderBy(movie => movie.DateAdded).ToList();
            }

            return movies;
        }

        public List<WebMovie> GetAllVideos(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            List<WebMovie> videos = SortWebMovieList(m_video.GetAllVideos(), sort, order); //get all movies from beginning to end
            return videos;
        }


        public List<WebMovie> GetVideos(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            List<WebMovie> videos = SortWebMovieList(m_video.GetAllVideos(), sort, order).GetRange(startIndex, endIndex - startIndex + 1);
            return videos;
        }

        public WebMovieFull GetFullVideo(int videoId)
        {
            return m_video.GetFullVideo(videoId);
        }

        #endregion
        #region TvSeries

        public int GetSeriesCount()
        {
            return m_mptvseries.GetSeriesCount();
        }

        private List<WebSeries> SortWebSeriesList(List<WebSeries> series, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return series.OrderByDescending(serie => serie.PrettyName).ToList();
                    }
                    return series.OrderBy(serie => serie.PrettyName).ToList();
                case SortBy.Rating:
                    if (order == OrderBy.Desc)
                    {
                        return series.OrderByDescending(serie => serie.Rating).ToList();
                    }
                    return series.OrderBy(serie => serie.Rating).ToList();
                case SortBy.Genre:
                    if (order == OrderBy.Desc)
                    {
                        return series.OrderByDescending(serie => serie.GenreString).ToList();
                    }
                    return series.OrderBy(serie => serie.GenreString).ToList();
            }


            return series;
        }
        private List<WebSeason> SortWebSeasonList(List<WebSeason> seasons, OrderBy order)
        {
            switch (order)
            {
                case OrderBy.Desc:
                    return seasons.OrderByDescending(season => season.SeriesId).ThenByDescending(season => season.SeasonNumber).ToList();
                case OrderBy.Asc:
                    return seasons.OrderBy(season => season.SeriesId).ThenBy(season => season.SeasonNumber).ToList();

            }
            return seasons;
        }
        private List<WebEpisode> SortWebEpisodeList(List<WebEpisode> episodes, SortBy sort, OrderBy order)
        {
            switch (sort)
            {
                case SortBy.SeasonNumber_EpisodeNumber:
                    if (order == OrderBy.Desc)
                    {
                        return episodes.OrderByDescending(episode => episode.IdSerie).ThenByDescending(episode => episode.SeasonNumber).ThenByDescending(episode => episode.EpisodeNumber).ToList();
                    }
                    return episodes.OrderBy(episode => episode.IdSerie).ThenBy(episode => episode.SeasonNumber).ThenBy(episode => episode.EpisodeNumber).ToList();
                case SortBy.Date:
                    if (order == OrderBy.Desc)
                    {
                        return episodes.OrderByDescending(episode => episode.FirstAired).ToList();
                    }
                    return episodes.OrderBy(episode => episode.FirstAired).ToList();
                case SortBy.Rating:
                    if (order == OrderBy.Desc)
                    {
                        return episodes.OrderByDescending(episode => episode.Rating).ToList();
                    }
                    return episodes.OrderBy(episode => episode.Rating).ToList();
                case SortBy.Name:
                    if (order == OrderBy.Desc)
                    {
                        return episodes.OrderByDescending(episode => episode.Name).ToList();
                    }
                    return episodes.OrderBy(episode => episode.Name).ToList();
            }
            return episodes;
        }

        public List<WebSeries> GetAllSeries(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebSeriesList(m_mptvseries.GetAllSeries(), sort, order);
        }


        public List<WebSeries> GetSeries(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            List<WebSeries> retList = SortWebSeriesList(m_mptvseries.GetAllSeries(), sort, order).GetRange(startIndex, endIndex - startIndex + 1);
            return retList;
        }


        public WebSeriesFull GetFullSeries(int seriesId)
        {
            return m_mptvseries.GetFullSeries(seriesId);
        }


        public List<WebSeason> GetAllSeasons(int seriesId, OrderBy order = OrderBy.Asc)
        {
            return SortWebSeasonList(m_mptvseries.GetAllSeasons(seriesId), order);//all seasons;
        }


        public WebSeason GetSeason(int seriesId, int seasonNumber)
        {
            return m_mptvseries.GetSeason(seriesId, seasonNumber);
        }


        public List<WebEpisode> GetAllEpisodes(int seriesId, SortBy sort = SortBy.SeasonNumber_EpisodeNumber, OrderBy order = OrderBy.Asc)
        {
            return SortWebEpisodeList(m_mptvseries.GetAllEpisodes(seriesId), sort, order);
        }


        public List<WebEpisode> GetAllEpisodesForSeason(int seriesId, int seasonNumber, SortBy sort = SortBy.EpisodeNumber, OrderBy order = OrderBy.Asc)
        {
            return SortWebEpisodeList(m_mptvseries.GetAllEpisodesForSeason(seriesId, seasonNumber), sort, order);
        }


        public List<WebEpisode> GetEpisodes(int seriesId, int startIndex, int endIndex, SortBy sort = SortBy.SeasonNumber_EpisodeNumber, OrderBy order = OrderBy.Asc)
        {
            return SortWebEpisodeList(m_mptvseries.GetAllEpisodes(seriesId), sort, order).GetRange(startIndex, endIndex - startIndex);
        }


        public List<WebEpisode> GetEpisodesForSeason(int seriesId, int seasonId, int startIndex, int endIndex, SortBy sort = SortBy.EpisodeNumber, OrderBy order = OrderBy.Asc)
        {
            return SortWebEpisodeList(m_mptvseries.GetAllEpisodesForSeason(seriesId, seasonId), sort, order).GetRange(startIndex, endIndex - startIndex);
        }


        public int GetEpisodesCount(int seriesId)
        {
            return m_mptvseries.GetEpisodesCount(seriesId);
        }


        public int GetEpisodesCountForSeason(int seriesId, int season)
        {
            return m_mptvseries.GetEpisodesCountForSeason(seriesId, season);
        }


        public WebEpisodeFull GetFullEpisode(int episodeId)
        {
            return m_mptvseries.GetFullEpisode(episodeId);
        }

        #endregion
        #region Movies

        public WebMovieFull GetFullMovie(int movieId)
        {
            return m_movingPictures.GetFullMovie(movieId);
        }


        public int GetMovieCount()
        {
            return m_movingPictures.GetMovieCount();
        }


        public List<WebMovie> GetAllMovies(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMovieList(m_movingPictures.GetAllMovies(), sort, order);//get all movies from beginning to end
        }

        public List<WebMovieFull> GetAllMoviesDetailed(SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMovieListDetailed(m_movingPictures.GetAllMoviesDetailed(), sort, order);//get all movies from beginning to end
        }
        public List<WebMovieFull> GetMoviesDetailed(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMovieListDetailed(m_movingPictures.GetAllMoviesDetailed(), sort, order).GetRange(startIndex, endIndex - startIndex);
        }



        public List<WebMovie> GetMovies(int startIndex, int endIndex, SortBy sort = SortBy.Name, OrderBy order = OrderBy.Asc)
        {
            return SortWebMovieList(m_movingPictures.GetAllMovies(), sort, order).GetRange(startIndex, endIndex - startIndex);
        }


        public List<WebMovie> SearchForMovie(String searchString)
        {
            return m_movingPictures.SearchForMovie(searchString);
        }

        #endregion
        #region Pictures

        public List<WebShare> GetPictureRootShares()
        {
            return Shares.GetAllShares(Shares.ShareType.Picture);
        }

        public WebPictureDirectory GetPictureDirectory(string path)
        {
            return MPPictures.GetPictureDirectory(path);
        }

        #endregion

        #endregion

        #region FileSystemAccess
        public List<String> GetDirectoryListByPath(string path)
        {
            return Shares.GetDirectoryListByPath(path);
        }

        public List<WebFileInfo> GetFilesFromDirectory(string filepath)
        {
            return Shares.GetFileListByPath(filepath);
        }

        public byte[] GetFile(string path)
        {
            if (!Shares.IsAllowedPath(path))
            {
                Log.Warn("Called GetFile() for non-existent or non-allowed file {0}", path);
                return null;
            }
            return Filesystem.GetFile(path);
        }
        #endregion

        #region Media
        /// <summary>
        /// Get the full path to a media item as specified by the client
        /// </summary>
        /// <param name="type">The type of the media we need the path</param>
        /// <param name="itemId">
        /// The identifier of the media to retrieve:
        /// - ID for tv episode (TvSeriesItem)
        /// - ID for music and video from database (MusiTrackItem, VideoDatabaseItem)
        /// - Path for music or video shares item (MusicShareItem, VideoShareItem)
        /// - ID-part for movies (that's a dash that separates them, parts start couning at one, e.g. 15-2, defaults to first part, MovieItem) 
        /// </param>
        /// <returns>Full path to the file</returns>
        public string GetPath(MediaItemType type, string itemId)
        {
            switch (type)
            {
                case MediaItemType.MusicShareItem:
                    if (Shares.IsAllowedPath(itemId, Shares.ShareType.Music))
                        return itemId;
                    break;
                case MediaItemType.VideoShareItem:
                    if (Shares.IsAllowedPath(itemId, Shares.ShareType.Video))
                        return itemId;
                    break;
                case MediaItemType.VideoDatabaseItem:
                    return m_video.GetVideoPath(itemId);
                case MediaItemType.TvSeriesItem:
                    return m_mptvseries.GetSeriesPath(itemId);
                case MediaItemType.MovieItem:
                    if (itemId.IndexOf("-") == -1)
                    {
                        return m_movingPictures.GetMoviePath(Int32.Parse(itemId), 1);
                    }
                    else
                    {
                        string[] items = itemId.Split('-');
                        return m_movingPictures.GetMoviePath(Int32.Parse(items[0]), Int32.Parse(items[1]));
                    }
                case MediaItemType.MusicTrackItem:
                    return m_music.GetTrackPath(itemId);
            }

            return null;
        }

        /// <summary>
        /// Returns the image object of the given path or null if the image doesn't exists
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <returns>Stream of image or null</returns>
        public Stream GetImage(string path)
        {
            return MPPictures.GetImage(path);
        }

        /// <summary>
        /// Returns the image object of the given path, resized to fit the maxWidth and maxHeight
        /// parameters, or null if the image doesn't exists
        /// </summary>
        /// <param name="path">Path to image</param>
        /// <param name="maxWidth">Maximum width of image</param>
        /// <param name="maxHeight">Maximum height of image</param>
        /// <returns>Stream of image or null</returns>
        public Stream GetImageResized(string path, int maxWidth, int maxHeight)
        {
            return MPPictures.GetImageResized(path, maxWidth, maxHeight);
        }

        /// <summary>
        /// Returns the stream to a media item (video, music, image,...)
        /// </summary>
        /// <param name="type">Type of the item</param>
        /// <param name="itemId">Id of the item (id for series, movie, db video, musictrac and path for shares item)</param>
        /// <returns>Stream of file or null if file not found</returns>
        public Stream GetMediaItem(MediaItemType type, string itemId)
        {
            return Filesystem.GetFileStream(GetPath(type, itemId));
        }

        public WebFileInfo GetFileInfo(MediaItemType type, string itemId)
        {
            return Shares.GetFileInfo(GetPath(type, itemId));
        }
        #endregion 
      
    }
}
