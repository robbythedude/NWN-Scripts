namespace TethyrAutoUpdater
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.labelAuthor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linklabelSite = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(91, 9);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(112, 13);
            this.labelAuthor.TabIndex = 0;
            this.labelAuthor.Text = "Author: Robert Steiner";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email: robbythedude@hotmail.com";
            // 
            // linklabelSite
            // 
            this.linklabelSite.AutoSize = true;
            this.linklabelSite.Location = new System.Drawing.Point(62, 35);
            this.linklabelSite.Name = "linklabelSite";
            this.linklabelSite.Size = new System.Drawing.Size(174, 13);
            this.linklabelSite.TabIndex = 2;
            this.linklabelSite.TabStop = true;
            this.linklabelSite.Text = "http:///www.BrogrammerRob.com/";
            this.linklabelSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabelSite_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 59);
            this.Controls.Add(this.linklabelSite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAuthor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linklabelSite;
    }
}