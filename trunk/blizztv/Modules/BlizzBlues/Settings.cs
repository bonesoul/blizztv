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

using BlizzTV.ModuleLib.Settings;

namespace BlizzTV.Modules.BlizzBlues
{
    public class Settings : ModuleSettings
    {
        #region instance

        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }

        #endregion

        private Settings() : base("BlizzBlues") { }

        public int UpdateEveryXMinutes { get { return this.GetInt("UpdateEveryXMinutes", 60); } set { this.Set("UpdateEveryXMinutes", value); } }
        public bool TrackWorldofWarcraft { get { return this.GetBoolean("TrackWorldofWarcraft", true); } set { this.Set("TrackWorldofWarcraft", value); } }
        public bool TrackStarcraft { get { return this.GetBoolean("TrackStarcraft", true); } set { this.Set("TrackStarcraft", value); } }
    }
}