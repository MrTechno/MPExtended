﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Services.MediaAccessService.Interfaces.Movie
{
    public class WebMovieDetailed : WebMovieBasic
    {
        public IList<string> Directors { get; set; }
        public IList<string> Writers { get; set; }
        public IList<string> Actors { get; set; }
        public string Summary { get; set; }
        public IList<WebMovieFile> Files { get; set; }
    
        //how to force ISO standard naming?
        public string Language { get; set; }
    }
}
