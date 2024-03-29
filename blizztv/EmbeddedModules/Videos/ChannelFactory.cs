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
using BlizzTV.EmbeddedModules.Videos.Parsers;

namespace BlizzTV.EmbeddedModules.Videos
{
    public static class ChannelFactory
    {
        public static Channel CreateChannel(VideoSubscription subscription)
        {
            Channel channel = null;
            switch (subscription.Provider.ToLower())
            {
                case "youtube": channel = new Youtube(subscription); break;
                case "bliptv": channel = new BlipTv(subscription); break;
                default: break;
            }

            if(channel==null) throw new NotImplementedException(string.Format("Module video's channel factory have been asked for an invalid channel-provider: '{0}'", subscription.Provider)); // throw an exception if video channel was not associated with a valid provider.
            return channel;
        }
    }
}
