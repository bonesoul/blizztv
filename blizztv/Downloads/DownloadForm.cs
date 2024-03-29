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

using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Utility.Extensions;

namespace BlizzTV.Downloads
{
    public partial class DownloadForm : Form
    {
        private Download _download;

        public DownloadForm()
        {
            InitializeComponent();
        }

        public DownloadForm(string title)
        {
            InitializeComponent();
            if (title != string.Empty) this.Text = title;
        }

        public void StartDownload(Download download)
        {
            this._download = download;
            this.labelStatistics.Text = i18n.ConnectingDownloadServer;
            this._download.Progress += OnDownloadProgress;
            this._download.Complete += OnDownloadComplete;
            this._download.Start();
        }

        private void OnDownloadProgress(int progress)
        {
            this.progressBar.AsyncInvokeHandler(() =>
            {
                this.progressBar.Value = progress;
                this.labelStatistics.Text = string.Format("{0} of {1} with {2}. (%{3})", this._download.Downloaded, this._download.Size, this._download.Speed, this._download.DownlodadedPercent);
            });            
        }

        protected virtual void OnDownloadComplete(bool success) 
        {
            this.progressBar.AsyncInvokeHandler(() =>
                {
                    this.DialogResult = success ? DialogResult.OK : DialogResult.Abort;
                    this.Close();
                });
        }
    }
}
