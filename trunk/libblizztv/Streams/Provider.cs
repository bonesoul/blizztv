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

namespace LibBlizzTV.Streams
{
    public class Provider
    {
        private string _name;
        private string _video_template;

        public string Name { get { return this._name; } internal set { this._name = value; } }
        public string VideoTemplate { get { return this._video_template; } internal set { this._video_template = value; } }

        public Provider(string Name, string VideoTemplate)
        {
            this._name = Name;
            this._video_template = VideoTemplate;            
        }
    }
}
