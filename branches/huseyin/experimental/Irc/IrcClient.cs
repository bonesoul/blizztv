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
 * $Id: IrcClient.cs 462 2011-03-11 10:00:55Z shalafiraistlin@gmail.com $
 */

using System.Collections.Generic;
using BlizzTV.EmbeddedModules.Irc.Connection;

namespace BlizzTV.EmbeddedModules.Irc
{
    public class IrcClient
    {
        public readonly List<IrcServer> ActiveServers = new List<IrcServer>();

        public IrcClient()
        {
            //var server = new IrcServer("Quakenet", "bonesoul-ev.mine.nu", 6667);
            var server = new IrcServer("Quakenet", "irc.quakenet.org", 6667);
            //var server = new IrcServer("Quakenet", "irc.freenode.net", 6667);
            this.ActiveServers.Add(server);
            server.Connect();
        }
    }
}