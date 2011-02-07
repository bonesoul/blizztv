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
using System.Windows.Forms;
using BlizzTV.Modules.Subscriptions.Providers;

namespace BlizzTV.Videos
{
    public partial class frmAddChannel : Form
    {
        public VideoSubscription Subscription = new VideoSubscription();

        public frmAddChannel()
        {
            InitializeComponent();
        }

        private void frmAddChannel_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, IProvider> pair in Providers.Instance.Dictionary) { comboBoxProviders.Items.Add(pair.Value.Name); }
            comboBoxProviders.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" || txtURL.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the channel name and url fields!", "All fields required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            VideoProvider provider = (VideoProvider)Providers.Instance.Dictionary[comboBoxProviders.SelectedItem.ToString()];
            if (!provider.LinkValid(txtURL.Text))
            {
                MessageBox.Show(string.Format("{0} is an invalid {1} url. Please correct the url and retry.", txtURL.Text, provider.Name), "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String slug = provider.GetSlug(txtURL.Text);
            if(Subscriptions.Instance.Dictionary.ContainsKey(slug))
            {
                MessageBox.Show(string.Format("The channel already exists in your subscriptions named as '{0}'.", Subscriptions.Instance.Dictionary[slug].Name), "Subscription Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Subscription.Name = txtName.Text;
            this.Subscription.Slug = slug;
            this.Subscription.Provider = provider.Name;

            using (Channel channel = ChannelFactory.CreateChannel(this.Subscription))
            {
                if (!channel.IsValid())
                {
                    MessageBox.Show("There was an error parsing the video channel. Please check the channel URL and retry.", "Error parsing video channel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
                        
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelHint.Text = ((VideoProvider) Providers.Instance.Dictionary[(string) comboBoxProviders.SelectedItem]).URLHint;
        }
    }
}
