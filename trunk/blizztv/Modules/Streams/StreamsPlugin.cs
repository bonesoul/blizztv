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
using System.Timers;
using BlizzTV.CommonLib.Utils;
using BlizzTV.CommonLib.Logger;
using BlizzTV.CommonLib.Settings;
using BlizzTV.CommonLib.UI;
using BlizzTV.ModuleLib;
using BlizzTV.ModuleLib.Subscriptions.Providers;
using BlizzTV.CommonLib.Workload;

namespace BlizzTV.Modules.Streams
{
    [ModuleAttributes("Streams", "Live stream aggregator plugin.","stream_16")]
    public class StreamsPlugin:Module
    {
        internal Dictionary<string,Stream> _streams = new Dictionary<string,Stream>();
        private Timer _updateTimer;
        private bool _disposed = false;

        public static StreamsPlugin Instance;

        public StreamsPlugin()
        {
            StreamsPlugin.Instance = this;
            this.RootListItem = new ListItem("Streams");
            this.RootListItem.Icon = new NamedImage("stream_16", Properties.Resources.stream_16);

            // register context-menu's.
            this.RootListItem.ContextMenus.Add("manualupdate", new System.Windows.Forms.ToolStripMenuItem("Update Streams", null, new EventHandler(RunManualUpdate))); // mark as unread menu.
        }

        public override void Run()
        {
            this.UpdateStreams();
            this.SetupUpdateTimer();
        }

        public override System.Windows.Forms.Form GetPreferencesForm()
        {
            return new frmSettings();
        }

        public override bool TryDragDrop(string link)
        {
            foreach (KeyValuePair<string, IProvider> pair in Providers.Instance.Dictionary)
            {
                if (((StreamProvider) pair.Value).LinkValid(link))
                {
                    StreamSubscription s = new StreamSubscription();
                    s.Slug = (pair.Value as StreamProvider).GetSlug(link);                    
                    s.Provider = pair.Value.Name;
                    s.Name = (pair.Value as StreamProvider).GetSlug(link);
                    
                    if(s.Provider.ToLower()=="own3dtv")
                    {
                        string name = "";
                        if (InputBox.Show("Add New Stream", "Please enter name for the new stream", ref name) == System.Windows.Forms.DialogResult.OK) s.Name = name;
                        else return false;
                    }

                    if(Subscriptions.Instance.Add(s)) this.RunManualUpdate(this, new EventArgs());
                    else System.Windows.Forms.MessageBox.Show(string.Format("The stream already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[s.Slug].Name), "Subscription Exists", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return true;
                }
            }
            return false;
        }

        internal void UpdateStreams()
        {
            if (this.Updating) return;

            this.Updating = true;
            this.NotifyUpdateStarted();

            if (this._streams.Count > 0)// clear previous entries before doing an update.
            {
                this._streams.Clear();
                this.RootListItem.Childs.Clear();
            }

            this.RootListItem.SetTitle("Updating streams..");

            foreach (KeyValuePair<string, StreamSubscription> pair in Subscriptions.Instance.Dictionary)
            {
                this._streams.Add(pair.Value.Slug, StreamFactory.CreateStream(pair.Value));
            }

            int availableCount = 0; // available live streams count
            Workload.Instance.Add(this, this._streams.Count);

            foreach (KeyValuePair<string, Stream> pair in this._streams) // loop through all streams
            {
                try
                {
                    pair.Value.Update(); // update the stream
                    if (pair.Value.IsLive) // if it's live
                    {
                        pair.Value.SetTitle(string.Format("{0} ({1})", pair.Value.Title, pair.Value.ViewerCount)); // put stream viewers count on title.
                        availableCount++; // increment available live streams count.
                        this.RootListItem.Childs.Add(pair.Key, pair.Value);
                    }
                    Workload.Instance.Step(this);
                }
                catch (Exception e) { Log.Instance.Write(LogMessageTypes.Error, string.Format("StreamsPlugin ParseStreams() Error: \n {0}", e)); } // catch errors for inner stream-handlers.
            }

            this.RootListItem.SetTitle(string.Format("Streams ({0})", availableCount));  // put available streams count on root object's title.

            NotifyUpdateComplete(new PluginUpdateCompleteEventArgs(true));
            this.Updating = false;
        }

        public void OnSaveSettings()
        {
            this.SetupUpdateTimer();
        }

        private void SetupUpdateTimer()
        {
            if (this._updateTimer != null)
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer = null;
            }

            _updateTimer = new Timer(Settings.Instance.UpdateEveryXMinutes * 60000);
            _updateTimer.Elapsed += OnTimerHit;
            _updateTimer.Enabled = true;
        }

        private void OnTimerHit(object source, ElapsedEventArgs e)
        {
            if (!GlobalSettings.Instance.InSleepMode) UpdateStreams();
        }

        private void RunManualUpdate(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(UpdateStreams) {IsBackground = true};
            thread.Start();
        }

        #region de-ctor

        protected override void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing) // managed resources
            {
                this._updateTimer.Enabled = false;
                this._updateTimer.Elapsed -= OnTimerHit;
                this._updateTimer.Dispose();
                this._updateTimer = null;
                foreach (KeyValuePair<string,Stream> pair in this._streams) { pair.Value.Dispose(); }
                this._streams.Clear();
                this._streams = null;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
