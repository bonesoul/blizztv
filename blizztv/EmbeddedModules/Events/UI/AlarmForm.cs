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
using System.Windows.Forms;

namespace BlizzTV.EmbeddedModules.Events.UI
{
    public partial class AlarmForm : Form
    {
        private readonly Event _event;

        public AlarmForm(Event @event)
        {
            InitializeComponent();

            this._event = @event;
        }

        private void AlarmForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Event: {0}", this._event.FullTitle);
            this.LabelEvent.Text = string.Format("Event {0}", this._event.FullTitle);
            this.LabelStatus.Text = string.Format("is about to start in {0}.", this._event.TimeLeft);
            this._event.DeleteAlarm();
        }

        private void ButtonView_Click(object sender, EventArgs e)
        {
            EventViewerForm f = new EventViewerForm(this._event);
            f.Show();
            this.Close();
        }

        private void ButtonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
