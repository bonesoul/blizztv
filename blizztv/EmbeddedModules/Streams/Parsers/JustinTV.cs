﻿/*    
 * Copyright (C) 2010-2011, BlizzTV Project - http://code.google.com/p/blizztv/
 *  
 * This program is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the 
 * implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License 
 * for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program.  If not, see 
 * <http://www.gnu.org/licenses/>. 
 * 
 * $Id$
 */

using System;
using System.Collections;
using BlizzTV.Log;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Streams.Parsers
{
    /// <summary>
    /// Parser for justin.tv streams.
    /// </summary>
    public class JustinTv : Stream
    {
        public JustinTv(StreamSubscription subscription) : base(subscription) { }

        public override bool Parse()
        {
            this.Link = string.Format("http://www.justin.tv/{0}", this.Slug); // the stream link.

            try
            {
                var apiUrl = string.Format("http://api.justin.tv/api/stream/list.json?channel={0}", this.Slug); // the api url.
                WebReader.Result result = WebReader.Read(apiUrl); // read the api response.

                var data = (ArrayList)Json.JsonDecode(result.Response); // start parsing the json.

                if (data.Count == 0) return true;
                    
                this.IsLive = true; // is the stream live?                    
                var table = (Hashtable)data[0];
                this.ViewerCount = Int32.Parse(table["stream_count"].ToString()); // stream viewers count.
                if (table.Contains("title")) this.Description = (string)table["title"].ToString(); // stream description.
                this.Process();
                return true;
            }
            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Stream module - JustinTV parser caught an exception: {0}", e)); 
                return false;
            }
        }
    }
}
