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

namespace BlizzTV.InfraStructure.Modules.Settings
{
    /// <summary>
    /// Provides settings functionality to module's.
    /// </summary>
    public class ModuleSettingsBase : BlizzTV.Settings.Settings 
    {
        /// <summary>
        /// Constructs a ModuleSettings instance.
        /// </summary>
        /// <param name="name">The module name.</param>
        protected ModuleSettingsBase(string name) : base(string.Format("Module-{0}", name)) { }
    }
}
