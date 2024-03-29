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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BlizzTV.Assets.i18n;
using BlizzTV.Audio;
using BlizzTV.InfraStructure.Modules;
using BlizzTV.InfraStructure.Modules.Settings;
using BlizzTV.Notifications;
using BlizzTV.Settings;
using BlizzTV.Utility.Helpers;
using BlizzTV.Utility.UI;

namespace BlizzTV.UI
{
    public partial class PreferencesForm : Form
    {
        private readonly List<TabPage> _pluginTabs = new List<TabPage>();
        private bool _loadComplete = false;

        public PreferencesForm() { InitializeComponent(); }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            this.LoadSettings(); // load settings.
            this.LoadModuleTabs(); // load module setting-tabs.
            this._loadComplete = true;
        }

        public DialogResult ShowDialog(string tabPageName) // show dialog overrider that accepts a tab-name.
        {
            TabControl.SelectedTab = TabControl.TabPages[tabPageName];
            return this.ShowDialog();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.SaveSettings()) // try saving settings.
            {                
                foreach (TabPage t in this._pluginTabs) { ((IModuleSettingsForm) t.Controls[0]).SaveSettings(); } // also notify module's to let them save their settings.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadSettings()
        {
            // global settings.
            if (GlobalSettings.Instance.UseInternalViewers) radioButtonUseInternalViewers.Checked = true; else radioButtonUseDefaultWebBrowser.Checked = true;
            CheckBoxPlayerAlwaysOnTop.Checked = GlobalSettings.Instance.PlayerWindowsAlwaysOnTop;
            checkBoxNotificationsEnabled.Checked = GlobalSettings.Instance.NotificationsEnabled;
            groupBoxNotificationSounds.Enabled = GlobalSettings.Instance.NotificationsEnabled;
            checkBoxNotificationSoundsEnabled.Checked = GlobalSettings.Instance.NotificationSoundsEnabled;
            checkBoxAllowAutomaticUpdateChecks.Checked = GlobalSettings.Instance.AllowAutomaticUpdateChecks;
            checkBoxAllowBetaVersionNotifications.Checked = GlobalSettings.Instance.AllowBetaVersionNotifications;
            volumeBar.Value = (int) (GlobalSettings.Instance.NotificationSoundVolume*10);
            foreach (string sound in NotificationSound.Instance.Names) this.comboBoxNotificationSound.Items.Add(sound);
            this.comboBoxNotificationSound.SelectedIndex = this.NotificationSoundGetSelectedIndex();

            // UI settings.
            if (!SystemStartup.IsSupported)
            {
                checkBoxStartOnSystemStartup.Enabled = false;
                checkBoxStartOnSystemStartup.Text += @" (requires administrative privileges)";
            }
            else checkBoxStartOnSystemStartup.Checked = SystemStartup.Enabled;
            checkBoxMinimimizeToSystemTray.Checked = Settings.Instance.MinimizeToSystemTray;
            checkBoxEnableDebugLogging.Checked = Settings.Instance.EnableDebugLogging;
            checkBoxEnableDebugConsole.Checked = Settings.Instance.EnableDebugConsole;

            // load module enabled-disabled status settings.
            foreach (KeyValuePair<string, ModuleController> pair in ModuleManager.Instance.AvailableModules)
            {
                var item = new ListviewModuleItem(pair.Value);
                if (!this.listviewModules.SmallImageList.Images.ContainsKey(pair.Value.Attributes.Name)) this.listviewModules.SmallImageList.Images.Add(pair.Value.Attributes.Name, pair.Value.Attributes.Icon);
                this.listviewModules.Items.Add(item);
                if (Settings.Instance.Modules.Enabled(pair.Value.Attributes.Name)) item.Checked = true;
            }          
        }

        private void LoadModuleTabs() // loads module  specific preferences tabs
        {
            foreach (KeyValuePair<string, bool> pair in Settings.Instance.Modules.List)
            {
                if (pair.Value) // if module is enabled
                {
                    Form moduleForm = ModuleManager.Instance.LoadedModules[pair.Key].GetPreferencesForm(); // get module's preferences form.

                    if (moduleForm != null) // if module supplies a preferenes form.
                    {
                        var tab = new TabPage(pair.Key); // create up a new tabpage for it.
                        moduleForm.TopLevel = false; // module form should not behave as top most.
                        moduleForm.Dock = DockStyle.Fill; // let it fill it's parent.
                        moduleForm.FormBorderStyle = FormBorderStyle.None; // it should not have borders too.
                        moduleForm.BackColor = Color.White; 
                        moduleForm.Show(); // show the settings form.
                        tab.ImageIndex = TabControl.ImageList.Images.IndexOfKey(pair.Key); // set the icon.
                        tab.Controls.Add(moduleForm); // add the form to tabpage.                        
                        this._pluginTabs.Add(tab); // add tabpage to list so we can access it later.
                        this.TabControl.TabPages.Insert(this.TabControl.TabPages.Count - 1, tab); // add tabpage to our tabcontrol -- insert it before the last tab, tabDebug.                        
                    }
                }
            }
        }

        private bool SaveSettings() // sets the settings.
        {
            // globals ettings.
            GlobalSettings.Instance.UseInternalViewers = radioButtonUseInternalViewers.Checked;
            GlobalSettings.Instance.PlayerWindowsAlwaysOnTop = CheckBoxPlayerAlwaysOnTop.Checked;
            GlobalSettings.Instance.NotificationsEnabled = checkBoxNotificationsEnabled.Checked;
            GlobalSettings.Instance.NotificationSoundsEnabled = checkBoxNotificationSoundsEnabled.Checked;
            GlobalSettings.Instance.NotificationSound = comboBoxNotificationSound.SelectedItem.ToString();
            GlobalSettings.Instance.NotificationSoundVolume = (float)volumeBar.Value / 10;
            GlobalSettings.Instance.AllowAutomaticUpdateChecks = checkBoxAllowAutomaticUpdateChecks.Checked;
            GlobalSettings.Instance.AllowBetaVersionNotifications = checkBoxAllowBetaVersionNotifications.Checked;

            // UI settings.
            if(SystemStartup.IsSupported) SystemStartup.Enabled = checkBoxStartOnSystemStartup.Checked;
            Settings.Instance.MinimizeToSystemTray = checkBoxMinimimizeToSystemTray.Checked;
            Settings.Instance.EnableDebugLogging = checkBoxEnableDebugLogging.Checked;
            Settings.Instance.EnableDebugConsole = checkBoxEnableDebugConsole.Checked;

            // save module-status settings
            foreach (ListviewModuleItem item in listviewModules.Items)
            {
                if (item.Checked) Settings.Instance.Modules.Enable(item.ModuleName); // enable the module.
                else
                {
                    if (ModuleManager.Instance.LoadedModules.ContainsKey(item.ModuleName))
                    {
                        if (ModuleManager.Instance.LoadedModules[item.ModuleName].RefreshingData) // don't allow disabling modules that are currently updating data.
                        {
                            MessageBox.Show(i18n.CanNotDeactivateUpdatingModules, string.Format(i18n.ModuleUpdating, item.ModuleName), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    Settings.Instance.Modules.Disable(item.ModuleName); // disable the module.                   
                }
            }

            Settings.Instance.Save(); // save the settings.
            return true;
        }

        #region notifications-tab specific

        public int NotificationSoundGetSelectedIndex()
        {
            string selection=GlobalSettings.Instance.NotificationSound;
            if (!comboBoxNotificationSound.Items.Contains(selection)) selection = "DefaultNotification";
            return comboBoxNotificationSound.Items.IndexOf(selection);
        }        

        private void checkBoxNotificationsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxNotificationSounds.Enabled = checkBoxNotificationsEnabled.Checked;
        }

        private void checkBoxNotificationSoundsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxNotificationSound.Enabled = checkBoxNotificationSoundsEnabled.Checked;
        }

        private void comboBoxNotificationSound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loadComplete)
            {
                string selection = this.comboBoxNotificationSound.SelectedItem.ToString();
                NotificationSound.Instance.Play(selection);
            }
        }

        #endregion

        #region misc code

        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            AudioManager.Instance.SetVolume((float)this.volumeBar.Value/10);
        }

        #endregion
    }
}
