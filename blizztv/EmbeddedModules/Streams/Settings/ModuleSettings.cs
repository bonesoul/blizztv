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

using BlizzTV.InfraStructure.Modules.Settings;

namespace BlizzTV.EmbeddedModules.Streams.Settings
{
    public class ModuleSettings : ModuleSettingsBase
    {
        #region instance

        private static ModuleSettings _instance = new ModuleSettings();
        public static ModuleSettings Instance { get { return _instance; } }

        #endregion

        private ModuleSettings() : base("Streams") { }

        /// <summary>
        /// Enables notifications for streams module.
        /// </summary>
        public bool NotificationsEnabled { get { return this.GetBoolean("NotificationsEnabled", true); } set { this.Set("NotificationsEnabled", value); } }

        /// <summary>
        /// Sets update period for streams module.
        /// </summary>
        public int UpdatePeriod { get { return this.GetInt("UpdatePeriod", 60); } set { this.Set("UpdatePeriod", value); } }

        /// <summary>
        /// Automatically loads chat window open stream load.
        /// </summary>
        public bool AutomaticallyOpenChat { get { return this.GetBoolean("AutomaticallyOpenChat", false); } set { this.Set("AutomaticallyOpenChat", value); } }

        /// <summary>
        /// Sets chat window's width.
        /// </summary>
        public int ChatWindowWidth { get { return this.GetInt("ChatWindowWidth", 350); } set { this.Set("ChatWindowWidth", value); } }

        /// <summary>
        /// Sets chat window's height.
        /// </summary>
        public int ChatWindowHeight { get { return this.GetInt("ChatWindowHeight", 385); } set { this.Set("ChatWindowHeight", value); } }

        /// <summary>
        /// Sets stream player windows width.
        /// </summary>
        public int PlayerWidth { get { return this.GetInt("PlayerWidth", 640); } set { this.Set("PlayerWidth", value); } }

        /// <summary>
        /// Sets stream player windows height.
        /// </summary>
        public int PlayerHeight { get { return this.GetInt("PlayerHeight", 385); } set { this.Set("PlayerHeight", value); } }
    }
}
