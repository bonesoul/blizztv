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

using System.Collections.Generic;
using System.Text.RegularExpressions;
using BlizzTV.Utility.Web;

namespace BlizzTV.EmbeddedModules.Feeds.Parsers
{
    /// <summary>
    /// Parses a feed using supported spefication-parsers.
    /// </summary>
    public class FeedParser
    {
        #region instance

        private static FeedParser _instance = new FeedParser();
        public static FeedParser Instance { get { return _instance; } }

        #endregion 

        private readonly Regex _blizzardAtomRegex = new Regex(@".*\.battle.net/.*/en", RegexOptions.Compiled); // regex used for parsing blizzard atom feeds.
        private readonly IFeedParser[] _parsers = new IFeedParser[] { new Rss2(),new Atom1() }; // our supported parsers.

        public bool Parse(string url,ref List<FeedItem> items)
        {
            if (items == null) items = new List<FeedItem>();
            if (items.Count > 0) items.Clear();

            WebReader.Result result = WebReader.Read(url);
            if (result.State != WebReader.States.Success) return false; 

            string linkFallback = "";
            Match m=this._blizzardAtomRegex.Match(url); // blizzard's atom feeds does not contain any links, so let's hack it.
            if (m.Success) linkFallback = string.Format("{0}/blog/", m.Groups[0]);
            
            foreach (IFeedParser parser in this._parsers) { if (parser.Parse(result.Response, ref items, linkFallback)) return true; }

            return false;
        }
    }

    public class FeedItem
    {
        public string Title { get; private set; }
        public string Id { get; private set; }
        public string Link { get; private set; }

        public FeedItem(string title, string id, string link)
        {
            this.Title = title;
            this.Id = id;
            this.Link = link;
        }
    }
}
