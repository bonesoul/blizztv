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
using System.Drawing;

namespace BlizzTV.InfraStructure.Modules
{
    /// <summary>
    /// Defines module attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttributes : Attribute 
    {
        private readonly string _iconName;
        private bool _disposed = false;

        /// <summary>
        /// The module name.
        /// </summary>
        public string Name { get; private set; } 
        
        /// <summary>
        /// The module description.
        /// </summary>
        public string Description { get; private set; } 

        /// <summary>
        /// The module icon.
        /// </summary>
        public Bitmap Icon { get; private set; } // The module icon.

        /// <summary>
        /// Module attributes constructor.
        /// </summary>
        /// <param name="name">The module name.</param>
        /// <param name="description">The module description.</param>       
        /// <param name="iconName">Module icon's name.</param>
        /// <param name="functionality">The module functionality.</param>
        public ModuleAttributes(string name, string description, string iconName = null)
        {
            Icon = null;
            this.Name = name;
            this.Description = description;
            this._iconName = iconName;
        }

        /// <summary>
        /// Resolves resources for the module and sets the icon if appliable.
        /// </summary>
        public void ResolveResources() 
        {
            if (this._iconName == null) return; // if no icon name is supplied, just ignore than.

            using (var stream = (Bitmap)Assets.Images.Icons.Png._16.ResourceManager.GetObject(this._iconName)) // get the icon stream from resource manager.
            {
                this.Icon = stream != null ? new Bitmap(stream) : Assets.Images.Icons.Png._16.blizztv; // if asked icon does not exist, then use the default icon.                                   
            }
        }

        #region de-ctor

        // IDisposable pattern: http://msdn.microsoft.com/en-us/library/fs2xkftw%28VS.80%29.aspx

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Take object out the finalization queue to prevent finalization code for it from executing a second time.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return; // if already disposed, just return

            if (disposing) // only dispose managed resources if we're called from directly or in-directly from user code.
            {
                this.Icon.Dispose();
                this.Icon = null;
            }

            _disposed = true;
        }

        ~ModuleAttributes() { Dispose(false); } // finalizer called by the runtime. we should only dispose unmanaged objects and should NOT reference managed ones.
        
        #endregion
    }
}
