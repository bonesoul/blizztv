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
using System.Linq;
using System.Xml.Linq;
using BlizzTV.Log;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Videos.Parsers
{
    public class Youtube : Channel
    {
        public Youtube(VideoSubscription subscription) : base(subscription) { }

        protected override bool Parse()
        {
            try
            {
                var apiUrl = string.Format("http://gdata.youtube.com/feeds/api/users/{0}/uploads?alt=rss&max-results={1}", this.Slug, Settings.ModuleSettings.Instance.NumberOfVideosToQueryChannelFor); // the api url.
                WebReader.Result result = WebReader.Read(apiUrl); // read the api response.
                if (result.State != WebReader.States.Success) return false;

                var xdoc = XDocument.Parse(result.Response); // parse the api response.
                var entries = from item in xdoc.Descendants("item") // get the videos
                              select new
                              {
                                  GUID = item.Element("guid").Value,
                                  Title = item.Element("title").Value,
                                  Description = item.Element("description").Value,
                                  Link = item.Element("link").Value
                              };

                foreach (Video.Youtube video in entries.Select(entry => new Video.Youtube(this.Text, entry.Title, entry.GUID,entry.Description, entry.Link, this.Provider)))
                {
                    video.StateChanged += OnChildStateChanged;
                    video.Process();
                    this.Videos.Add(video);
                }
                return this.Videos.Count > 0;
            }
            catch (Exception e)
            {
                LogManager.Instance.Write(LogMessageTypes.Error, string.Format("Video module - Youtube parser caught an exception: {0}", e)); 
                return false;
            }
        }
    }
}
