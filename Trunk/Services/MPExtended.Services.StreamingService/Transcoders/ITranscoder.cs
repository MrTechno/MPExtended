﻿#region Copyright (C) 2011 MPExtended
// Copyright (C) 2011 MPExtended Developers
// http://mpextended.codeplex.com/
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
// along with MediaPortal. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using MPExtended.Services.StreamingService.Units;
using MPExtended.Services.StreamingService.Code;
using MPExtended.Services.StreamingService.Interfaces;
using MPExtended.Services.StreamingService.Util;

namespace MPExtended.Services.StreamingService.Transcoders
{
    internal interface ITranscoder
    {
        TranscoderProfile Profile { get; set; }
        string Input { get; set; }

        bool InputReaderWanted();
        EncoderUnit.TransportMethod GetInputMethod();
        EncoderUnit.TransportMethod GetOutputMethod();
        string GetTranscoderPath();
        string GenerateArguments(WebMediaInfo info, Resolution outputSize, int position, int? audioId, int? subtitleId);
        ILogProcessingUnit GetLogParsingUnit(Reference<EncodingInfo> save);
    }
}
