/*
 * Author: Robert Steiner (robbythedude@hotmail.com)
 * Date: 3/27/15
 * Purpose of this application is to fetch the most recent NWN Tethyr server updates and automatically install for user.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TethyrAutoUpdater
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.BrogrammerRob.com/";
            linklabelSite.Links.Add(link);
        }

        private void linklabelSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
