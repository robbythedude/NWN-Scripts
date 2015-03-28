/*
 * Author: Robert Steiner (robbythedude@hotmail.com)
 * Date: 3/27/15
 * Purpose of this application is to fetch the most recent NWN Tethyr server updates and automatically install for user.
 * 
 * Thread/Function Process
 * ----------------
 * 1)beginDownload
 * 2)unzipDownload
 * 3)allocateFiles
 * 4)cleanUp
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Threading;

namespace TethyrAutoUpdater
{
    public partial class MainForm : Form
    {
        private const string downloadFile = "tethyrupdate.zip";  //File to look for and download from remote server
        private Uri urlDownload = new Uri("http://www.BrogrammerRob.com/Downloads/NWN/" + downloadFile);  //Download location via website
        private const string nwnEXE = "nwn.exe";  //File used to verify correct directory
        private const string tempFolder = "tempTethyrUpdate";  //Folder name used for temporary actions during update process
        private string tempFolderPath;  //The path to the temp folder, assigned in beginDownload()
        private Thread threadToUpdate;  //Thread that executes the update so the program stays responsive to the user while updating

        //Constructor
        public MainForm()
        {
            InitializeComponent();
            this.textboxPathToNWN.Text = "NWN Directory Here";
        }

        //This function will execute when the browser button is pressed
        //This will allow for the user to browser for their NWN directory and get verification of correct directory
        private void buttonBrowse_Click(object sender, EventArgs e)
        {            
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)  //Open up the browser dialog
            {
                textboxPathToNWN.Text = folderBrowserDialog1.SelectedPath;  //Will take the captured directory and store it in the textbox
            }
            verifyDirectory();  //Just a helpful verification for the user to see
        }

        //This function will execute when the update button is clicked.
        //This function will start the thread process and disable all possible interaction with the application
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if(verifyDirectory())  //Only perform the update if the directory is verified
            {
                buttonUpdate.Enabled = false;  //Stop any user alteration while update in progress
                textboxPathToNWN.Enabled = false; //Stop any user alteration while update in progress
                buttonBrowse.Enabled = false; //Stop any user alteration while update in progress
                threadToUpdate = new Thread(beginDownload);                
                threadToUpdate.Start();
            }
            
        }

        //This function will execute when the about button is clicked
        //Will simply show the about form with basic information
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutForm abf = new AboutForm();
            abf.Show();
        }

        //Function will verify if the directory contains the needed NWN.exe file
        //Will indicate to user if selection is correct
        //Returns if file is found or not
        private bool verifyDirectory()
        {
            try
            {
                string[] fileArray = Directory.GetFiles(textboxPathToNWN.Text);  //Capture all file names in selected directory
                string neededFilePath = textboxPathToNWN.Text + "\\" + nwnEXE;  //Construct path to the file we are looking for

                foreach (string name in fileArray)  //Scan captured file names
                {
                    if (name == neededFilePath)
                    {
                        textboxPathToNWN.BackColor = System.Drawing.Color.LightGreen;  //Indication to user
                        return true;  //File found
                    }

                }
                textboxPathToNWN.BackColor = System.Drawing.Color.PaleVioletRed;  //Indication to user
                return false;  //File not found
            }
            catch(Exception e)
            {
                return false;
            }            
        }

        //This function will update the status based on the passed in status string
        //This function was intentially created to handle UI updates outside the UI-thread
        private void updateStatus(string status)
        {
            if(labelStatus.InvokeRequired)
                labelStatus.BeginInvoke((MethodInvoker)delegate() { labelStatus.Text = "Status: " + status; });
            else
                labelStatus.Text = "Status: " + status;
        }

        //This function handles the creation of the temporary directory and downloading the .ZIP file from the remote server
        //First function apart of the thread process
        private void beginDownload()
        {
            tempFolderPath = textboxPathToNWN.Text + "\\" + tempFolder; //Temp directory

            updateStatus("Managing Temporary Folder");  //Update visible status            
            if (!Directory.Exists(tempFolderPath))  //checking if the temporary directory does not exists
                Directory.CreateDirectory(tempFolderPath);  //create the temporary folder
            else  //It does exist, delete all the contents inside
            {
                DirectoryInfo tempDirectoryInfo = new DirectoryInfo(tempFolderPath);
                foreach (FileInfo file in tempDirectoryInfo.GetFiles())
                {
                    file.Delete();  //Killing all files in the directory
                }
                foreach (DirectoryInfo dir in tempDirectoryInfo.GetDirectories())
                {
                    dir.Delete(true);  //Killing all directories in the directory include files/directories in the directories
                }
            }

            updateStatus("Beginning Download");  //Update visible status
            try //Will catch if any connection errors occur
            {
                using (var client = new WebClient())  //Creating a webclient
                {
                    updateStatus("Downloading Tethyr Files");  //Update visible status
                    client.DownloadFile(urlDownload, tempFolderPath + "\\" + downloadFile);  //Downloading file via webclient
                    updateStatus("Download Complete");  //Update visible status
                    unzipDownload(); //Moving the process to the next step
                }
            }
            catch (Exception e)  //Error with connection has occured
            {
                updateStatus("Unable to contact server");  //Update visible status
            }            
        }

        //This function will unzsip the downloaded .ZIP from the remote server
        //Second function of the thread process
        private void unzipDownload()
        {
            string downloadPath = tempFolderPath + "\\" + downloadFile;  //Path location to the downloaded file

            try
            {
                updateStatus("Unzipping Tethyr Files");  //Update visible status
                ZipFile.ExtractToDirectory(downloadPath, tempFolderPath);
                updateStatus("Unzipping Completed");  //Update visible status
                allocateFiles();  //Moving the process to the next step

            }
            catch(Exception e)
            {
                updateStatus("Need to update .NET");  //Update visible status
            }
        }

        //This function will move all unzipped files into their respective NWN directories
        //Third function of the thread process
        private void allocateFiles()
        {
            updateStatus("Allocating Tethyr Files");  //Update visible status
            DirectoryInfo tempDirectoryInfo = new DirectoryInfo(tempFolderPath);
            foreach (FileInfo file in tempDirectoryInfo.GetFiles()) //This first iteration will handle files put into the root directory
            {
                if(file.Extension != ".zip")  //Skip over any zip files, aka the actual update that was downloaded
                    file.CopyTo(textboxPathToNWN.Text + "\\" + file.Name, true);  //Copying and overwriting files into root NWN directory
            }
            foreach (DirectoryInfo dir in tempDirectoryInfo.GetDirectories())  //This second iteration will handle subdirectories found in the root directory
            {
                string directoryFolderName = dir.Name;
                if (!Directory.Exists(textboxPathToNWN.Text + "\\" + directoryFolderName))  //checking if the named folder does not exists
                    Directory.CreateDirectory(textboxPathToNWN.Text + "\\" + directoryFolderName);  //create the named folder
                foreach (FileInfo file in dir.GetFiles()) //This third iteration will handle files put into the sub-directory
                {
                    file.CopyTo(textboxPathToNWN.Text + "\\" + directoryFolderName + "\\" + file.Name, true);  //Copying and overwriting files into sub NWN directories
                }
            }
            

            updateStatus("Allocation Complete");  //Update visible status
            cleanUp();  //Moving the process to the next step
        }

        //This function will remove all files downloaded and the temporary directory, plus reenable all button commands
        //Last function of the thread process
        private void cleanUp()
        {
            updateStatus("Managing Temporary Folder");  //Update visible status
            DirectoryInfo tempDirectoryInfo = new DirectoryInfo(tempFolderPath);
            foreach (FileInfo file in tempDirectoryInfo.GetFiles())
            {
                file.Delete();  //Killing all files in the directory
            }
            foreach (DirectoryInfo dir in tempDirectoryInfo.GetDirectories())
            {
                dir.Delete(true);  //Killing all directories in the directory include files/directories in the directories
            }
            Directory.Delete(tempFolderPath);
            updateStatus("Managing Completed");  //Update visible status

            buttonUpdate.Enabled = true;  //Enabling Controls
            textboxPathToNWN.Enabled = true; //Enabling Controls
            buttonBrowse.Enabled = true; //Enabling Controls
            updateStatus("Update Completed");  //Update visible status
        }
    }
}
