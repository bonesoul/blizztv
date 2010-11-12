﻿namespace LibEvents
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxAllowNotificationOfEventsInProgress = new System.Windows.Forms.CheckBox();
            this.numericUpDownMinutesBefore = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAllowEventNotifications = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 269);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxAllowNotificationOfEventsInProgress);
            this.tabPage1.Controls.Add(this.numericUpDownMinutesBefore);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.checkBoxAllowEventNotifications);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 243);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Notifications";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxAllowNotificationOfEventsInProgress
            // 
            this.checkBoxAllowNotificationOfEventsInProgress.AutoSize = true;
            this.checkBoxAllowNotificationOfEventsInProgress.Location = new System.Drawing.Point(6, 29);
            this.checkBoxAllowNotificationOfEventsInProgress.Name = "checkBoxAllowNotificationOfEventsInProgress";
            this.checkBoxAllowNotificationOfEventsInProgress.Size = new System.Drawing.Size(209, 17);
            this.checkBoxAllowNotificationOfEventsInProgress.TabIndex = 7;
            this.checkBoxAllowNotificationOfEventsInProgress.Text = "Allow notification of events in-progress.";
            this.checkBoxAllowNotificationOfEventsInProgress.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMinutesBefore
            // 
            this.numericUpDownMinutesBefore.Location = new System.Drawing.Point(174, 47);
            this.numericUpDownMinutesBefore.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDownMinutesBefore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinutesBefore.Name = "numericUpDownMinutesBefore";
            this.numericUpDownMinutesBefore.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownMinutesBefore.TabIndex = 6;
            this.numericUpDownMinutesBefore.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Minutes to notify before an event:";
            // 
            // checkBoxAllowEventNotifications
            // 
            this.checkBoxAllowEventNotifications.AutoSize = true;
            this.checkBoxAllowEventNotifications.Location = new System.Drawing.Point(6, 6);
            this.checkBoxAllowEventNotifications.Name = "checkBoxAllowEventNotifications";
            this.checkBoxAllowEventNotifications.Size = new System.Drawing.Size(143, 17);
            this.checkBoxAllowEventNotifications.TabIndex = 4;
            this.checkBoxAllowEventNotifications.Text = "Allow event notifications.";
            this.checkBoxAllowEventNotifications.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 243);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Alarms";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(443, 243);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Calendar";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 269);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinutesBefore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxAllowNotificationOfEventsInProgress;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutesBefore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAllowEventNotifications;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;


    }
}