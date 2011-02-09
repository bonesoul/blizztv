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
using System.Windows.Forms;
using BlizzTV.Modules.Settings;

namespace BlizzTV.BlizzBlues
{
    public partial class SettingsForm : Form, IModuleSettingsForm
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.LoadSettings();
        }

        private void LoadSettings()
        {
            numericUpDownUpdatePeriod.Value = (decimal)Settings.Instance.UpdatePeriod;
            checkBoxTrackWorldofWarcraft.Checked = Settings.Instance.TrackWorldofWarcraft;
            checkBoxTrackStarcraft.Checked = Settings.Instance.TrackStarcraft;
            checkBoxEnableNotifications.Checked = Settings.Instance.NotificationsEnabled;
        }

        public void SaveSettings()
        {
            Settings.Instance.UpdatePeriod = (int)numericUpDownUpdatePeriod.Value;
            Settings.Instance.TrackWorldofWarcraft = checkBoxTrackWorldofWarcraft.Checked;
            Settings.Instance.TrackStarcraft = checkBoxTrackStarcraft.Checked;
            Settings.Instance.NotificationsEnabled = checkBoxEnableNotifications.Checked;
            Settings.Instance.Save();
            ModuleBlizzBlues.Instance.OnSaveSettings();
        }
    }
}
