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
using System.Linq;
using System.Xml.Serialization;
using BlizzTV.InfraStructure.Modules.Subscriptions;

namespace BlizzTV.EmbeddedModules.Streams
{
    public sealed class Subscriptions : SubscriptionsHandler
    {
        #region instance

        private static Subscriptions _instance = new Subscriptions();
        public static Subscriptions Instance { get { return _instance; } }

        #endregion

        private Subscriptions() : base(typeof(StreamSubscription)) { }

        public bool Add(StreamSubscription subscription)
        {
            if (this.Dictionary.ContainsKey(string.Format("{0}@{1}", subscription.Slug, subscription.Provider))) return false;

            base.Add(subscription);
            return true;
        }

        public Dictionary<string, StreamSubscription> Dictionary
        {
            get
            {
                return this.List.ToDictionary(subscription => string.Format("{0}@{1}",((StreamSubscription) subscription).Slug,((StreamSubscription) subscription).Provider.ToLower()), subscription => (subscription as StreamSubscription));
            }
        }
    }

    [Serializable]
    [XmlType("Stream")]
    public class StreamSubscription : Subscription
    {
        [XmlAttribute("Slug")]
        public string Slug { get; set; }

        [XmlAttribute("Provider")]
        public string Provider { get; set; }
    }
}
