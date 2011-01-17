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

using System.IO;
using Microsoft.Isam.Esent.Collections.Generic;

namespace BlizzTV.CommonLib.Storage
{
    public sealed class KeyValueStorage
    {
        #region instance

        private static KeyValueStorage _instance = new KeyValueStorage();
        public static KeyValueStorage Instance { get { return _instance; } }

        #endregion

        private const string StorageFolder = "Storage";
        private readonly PersistentDictionary<string, byte> _dictionary;        

        private KeyValueStorage()
        {
            if (!this.StorageExists()) Directory.CreateDirectory(StorageFolder);
            this._dictionary = new PersistentDictionary<string, byte>(StorageFolder);
        }

        public byte GetByte(string key)
        {
            return this.Exists(key) ? this._dictionary[key] : (byte)0;
        }

        public void SetByte(string key, byte value)
        {
            this._dictionary[key] = value;
            this._dictionary.Flush();
        }

        public bool Exists(string key)
        {
            return this._dictionary.ContainsKey(key);
        }

        public bool Delete(string key)
        {
            return this._dictionary.Remove(key);
        }

        internal bool StorageExists()
        {
            return Directory.Exists(StorageFolder);
        }
    }
}