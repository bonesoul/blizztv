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
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBlizzTV;
using Nini.Config;

namespace LibStreams
{
    public class Settings : PluginSettings
    {
        private static Settings _instance = new Settings();
        public static Settings Instance { get { return _instance; } }
        private Settings() : base("Streams") { }

        public int UpdateEveryXMinutes { get { return this.Config.GetInt("UpdateEveryXMinutes", 60); } set { this.Config.Set("UpdateEveryXMinutes", value); } }
        public bool AutomaticallyOpenChatForAvailableStreams { get { return this.Config.GetBoolean("AutomaticallyOpenChatForAvailableStreams", false); } set { this.Config.Set("AutomaticallyOpenChatForAvailableStreams", value); } }
        public int StreamChatWindowWidth { get { return this.Config.GetInt("StreamChatWindowWidth", 640); } set { this.Config.Set("StreamChatWindowWidth", value); } }
        public int StreamChatWindowHeight { get { return this.Config.GetInt("StreamChatWindowHeight", 385); } set { this.Config.Set("StreamChatWindowHeight", value); } }
    }
}
