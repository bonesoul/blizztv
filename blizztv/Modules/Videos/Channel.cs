﻿/*    
 * Copyright (C) 2010, BlizzTV Project - http://code.google.com/p/blizztv/
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
using System.Collections.Generic;
using BlizzTV.ModuleLib;

namespace BlizzTV.Modules.Videos
{
    public class Channel:ListItem
    {
        #region members

        private bool _valid = true; // did the feed parsed all okay?
        private string _name; // the channel name.
        private string _slug; // the channel slug.
        private string _provider; // the channel provider.
        private bool disposed = false;

        public bool Valid { get { return this._valid; } internal set { this._valid = value; } }
        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string Slug { get { return this._slug; } internal set { this._slug = value; }  }
        public string Provider { get { return this._provider; } internal set { this._provider = value; } }
                
        public List<Video> Videos = new List<Video>();

        #endregion

        #region ctor

        public Channel(VideoSubscription subscription)
            : base(subscription.Name)
        {
            this._name = subscription.Name;
            this._slug = subscription.Slug;
            this._provider = subscription.Provider;         

             // register context menus.
            this.ContextMenus.Add("markallaswatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Watched", null, new EventHandler(MenuMarkAllAsWatchedClicked))); // mark as read menu.
            this.ContextMenus.Add("markallasunwatched", new System.Windows.Forms.ToolStripMenuItem("Mark As Unwatched", null, new EventHandler(MenuMarkAllAsUnWatchedClicked))); // mark as unread menu.
        }

        #endregion

        #region internal logic

        public virtual void Update() { throw new NotImplementedException(); } // the channel updater. 

        public void Process()
        {
            if (this.Valid)
            {
                int unread = 0; // non-watched videos count.
                foreach (Video v in this.Videos) { if (v.Style == ItemStyle.BOLD) unread++; }

                if (unread > 0) // if there non-watched channel videos.
                {
                    this.SetTitle(string.Format("{0} ({1})", this.Title, unread.ToString()));
                    this.Style = ItemStyle.BOLD; // then mark the channel itself as unread also
                }
            }
            else
            {
                Video error = new Video("Error updating channel.", "", "", this.Provider);
                //error.Style = ItemStyle.ERROR;
                this.Videos.Add(error);
            }
        }

        private void MenuMarkAllAsWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Style = ItemStyle.REGULAR; } // marked all videos as watched.
            this.Style = ItemStyle.REGULAR; // also mark self as read.            
        }

        private void MenuMarkAllAsUnWatchedClicked(object sender, EventArgs e)
        {
            foreach (Video v in this.Videos) { v.Style = ItemStyle.BOLD; } // marked all videos as unread.
            this.Style = ItemStyle.BOLD; // also mark self as unread.      
        }

        #endregion

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) // managed resources
                {
                    foreach (Video v in this.Videos) { v.Dispose(); }
                    this.Videos.Clear();
                    this.Videos = null;
                }
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}