﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Services.MediaAccessService.Interfaces.TVShow
{
    public class WebTVShowDetailed : WebTVShowBasic
    {
        public IList<string> FanArtPaths { get; set; }
        public IList<string> PosterPaths { get; set; }
        public IList<string> Actors { get; set; }
    }
}
