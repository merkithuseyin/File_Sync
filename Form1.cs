using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Sync
{
    public partial class Form1 : Form
    {

        DirectoryInfo directory1Info;
        DirectoryInfo directory2Info;

        FileSystemWatcher fileSystemWatcher1;
        FileSystemWatcher fileSystemWatcher2;

        public Form1()
        {
            InitializeComponent();
        }
        /*
        // Remove from Alt+Tab dialog
        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }*/
        private void Button_Path1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_Path1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button_Path2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_Path2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Button_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                directory1Info = new DirectoryInfo(textBox_Path1.Text);
                directory2Info = new DirectoryInfo(textBox_Path2.Text);

                #region Path Controls
                if (!directory1Info.Exists)
                {
                    MessageBox.Show("First directory doesn't exist.");
                    return;
                }

                if (!directory2Info.Exists)
                {
                    MessageBox.Show("Second directory doesn't exist.");
                    return;
                }

                if (directory1Info.FullName == directory2Info.FullName)
                {
                    MessageBox.Show("Should be different paths");
                    return;
                }
                #endregion

                if (radioButton_FirstPath.Checked)
                {
                    if (MessageBox.Show($"All data in {directory2Info.FullName} will be lost. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        SyncFolders(textBox_Path1.Text, textBox_Path2.Text);
                    else
                        return;
                }
                else
                {
                    if (MessageBox.Show($"All data in {directory1Info.FullName} will be lost. Do you want to continue?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        SyncFolders(textBox_Path2.Text, textBox_Path1.Text);
                    else
                        return;
                }

                #region FileSystemWatcher Events
                fileSystemWatcher1 = new FileSystemWatcher(textBox_Path1.Text);
                fileSystemWatcher2 = new FileSystemWatcher(textBox_Path2.Text);
                fileSystemWatcher1.EnableRaisingEvents = true;
                fileSystemWatcher2.EnableRaisingEvents = true;

                fileSystemWatcher1.IncludeSubdirectories = true;
                fileSystemWatcher1.Changed += FileSystemWatcher1_CreateChange;
                fileSystemWatcher1.Created += FileSystemWatcher1_CreateChange;
                fileSystemWatcher1.Deleted += FileSystemWatcher1_Deleted;
                fileSystemWatcher1.Renamed += FileSystemWatcher1_Renamed;

                fileSystemWatcher2.IncludeSubdirectories = true;
                fileSystemWatcher2.Changed += FileSystemWatcher2_CreateChange;
                fileSystemWatcher2.Created += FileSystemWatcher2_CreateChange;
                fileSystemWatcher2.Deleted += FileSystemWatcher2_Deleted;
                fileSystemWatcher2.Renamed += FileSystemWatcher2_Renamed;

                void FileSystemWatcher1_CreateChange(object innerSender, FileSystemEventArgs innerE)
                {
                    fileSystemWatcher2.EnableRaisingEvents = false;
                    SyncSingle_CreateChange(directory1Info, directory2Info, innerE.FullPath);
                    fileSystemWatcher2.EnableRaisingEvents = true;
                }
                void FileSystemWatcher2_CreateChange(object innerSender, FileSystemEventArgs innerE)
                {
                    fileSystemWatcher1.EnableRaisingEvents = false;
                    SyncSingle_CreateChange(directory2Info, directory1Info, innerE.FullPath);
                    fileSystemWatcher1.EnableRaisingEvents = true;
                }
                void FileSystemWatcher1_Deleted(object innerSender, FileSystemEventArgs innerE)
                {
                    fileSystemWatcher2.EnableRaisingEvents = false;
                    SyncSingle_Delete(directory1Info, directory2Info, innerE.FullPath);
                    fileSystemWatcher2.EnableRaisingEvents = true;
                }
                void FileSystemWatcher2_Deleted(object innerSender, FileSystemEventArgs innerE)
                {
                    fileSystemWatcher1.EnableRaisingEvents = false;
                    SyncSingle_Delete(directory2Info, directory1Info, innerE.FullPath);
                    fileSystemWatcher1.EnableRaisingEvents = true;
                }
                void FileSystemWatcher1_Renamed(object innerSender, RenamedEventArgs innerE)
                {
                    fileSystemWatcher2.EnableRaisingEvents = false;
                    SyncSingle_Rename(directory1Info, directory2Info, innerE.OldFullPath, innerE.FullPath);
                    fileSystemWatcher2.EnableRaisingEvents = true;
                }
                void FileSystemWatcher2_Renamed(object innerSender, RenamedEventArgs innerE)
                {
                    fileSystemWatcher1.EnableRaisingEvents = false;
                    SyncSingle_Rename(directory2Info, directory1Info, innerE.OldFullPath, innerE.FullPath);
                    fileSystemWatcher1.EnableRaisingEvents = true;
                }
                #endregion

                button_Apply.Enabled = false;
                button_Stop.Enabled = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(exception.Message);
#if DEBUG
                throw exception;
#endif
            }
        }

        private void SyncSingle_CreateChange(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, string sourcePath)
        {
            try
            {
                FileInfo sourceFileInfo = new FileInfo(sourcePath);
                DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourcePath);

                if (sourceFileInfo.Exists)
                {
                    string targetFilePath = String.Copy(sourcePath);
                    targetFilePath = targetFilePath.Replace(sourceDirectory.FullName, targetDirectory.FullName);

                    // File.Copy() SOMETIMES throws 'used by antoher process' exception, so i believe i need to wait operating system to continue.
                    while (!IsFileReady(sourcePath)) Thread.Sleep(100);

                    File.Delete(targetFilePath);
                    File.Copy(sourcePath, targetFilePath, true);

                }
                else if (sourceDirectoryInfo.Exists)
                {
                    string targetFolderPath = String.Copy(sourcePath);
                    targetFolderPath = targetFolderPath.Replace(sourceDirectory.FullName, targetDirectory.FullName);
                    Directory.CreateDirectory(targetFolderPath);
                }
                else
                {
                    Console.WriteLine("Single sync unsuccessful, trying to sync the whole folder");
                    //if not successful force to copy whole folder and its files
                    SyncFolders(sourceDirectory.FullName, targetDirectory.FullName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(exception.Message);
#if DEBUG
                throw exception;
#endif
            }


        }

        private void SyncSingle_Delete(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, string sourcePath)
        {
            try
            {
                FileInfo sourceFileInfo = new FileInfo(sourcePath);
                DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourcePath);
                try
                {
                    try
                    {
                        string targetFilePath = String.Copy(sourcePath);
                        targetFilePath = targetFilePath.Replace(sourceDirectory.FullName, targetDirectory.FullName);
                        File.Delete(targetFilePath);
                    }
                    catch (Exception)
                    {
                        string targetFolderPath = String.Copy(sourcePath);
                        targetFolderPath = targetFolderPath.Replace(sourceDirectory.FullName, targetDirectory.FullName);
                        Directory.Delete(targetFolderPath);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Single sync unsuccessful, trying to sync the whole folder");
                    //if not successful force to copy whole folder and its files
                    SyncFolders(sourceDirectory.FullName, targetDirectory.FullName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(exception.Message);
#if DEBUG
                throw exception;
#endif
            }

        }

        private void SyncSingle_Rename(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory, string sourceOldPath, string sourceNewPath)
        {
            try
            {
                FileInfo sourceFileInfo = new FileInfo(sourceNewPath);
                DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceNewPath);

                if (sourceFileInfo.Exists)
                {
                    string targetOldFilePath = String.Copy(sourceOldPath);
                    targetOldFilePath = targetOldFilePath.Replace(sourceDirectory.FullName, targetDirectory.FullName);

                    string targetNewFilePath = String.Copy(sourceNewPath);
                    targetNewFilePath = targetNewFilePath.Replace(sourceDirectory.FullName, targetDirectory.FullName);

                    //while (!IsFileReady(sourceNewPath)) Thread.Sleep(100);
                    File.Move(targetOldFilePath, targetNewFilePath);

                }
                else if (sourceDirectoryInfo.Exists)
                {
                    string targetOldFolderPath = String.Copy(sourceOldPath);
                    targetOldFolderPath = targetOldFolderPath.Replace(sourceDirectory.FullName, targetDirectory.FullName);

                    string targetNewFolderPath = String.Copy(sourceNewPath);
                    targetNewFolderPath = targetNewFolderPath.Replace(sourceDirectory.FullName, targetDirectory.FullName);

                    Directory.Move(targetOldFolderPath, targetNewFolderPath);
                }
                else
                {
                    Console.WriteLine("Single sync unsuccessful, trying to sync the whole folder");
                    //if not successful force to copy whole folder and its files
                    SyncFolders(sourceDirectory.FullName, targetDirectory.FullName);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(exception.Message);
#if DEBUG
                throw exception;
#endif
            }
            
        }

        /// <summary>
        /// Sync folders
        /// </summary>
        /// <param name="sourcePath">Source Folder that won't be changed</param>
        /// <param name="targetPath">Target Folder that will be the same as Source Folder</param>
        /// <returns></returns>
        private bool SyncFolders(string sourcePath, string targetPath)
        {
            try
            {
                DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourcePath);
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetPath);

                CopyFiles(sourceDirectoryInfo, targetDirectoryInfo);
                TrimFiles(sourceDirectoryInfo, targetDirectoryInfo);

                Console.WriteLine($"Directory sync successful. Source: {sourcePath} , Target: {targetPath}");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Directory sync unsuccessful. Source: {sourcePath} , Target: {targetPath}");
                Console.WriteLine(e.Message);
#if DEBUG
                throw e;
#endif
                MessageBox.Show($"Sync unsuccessful.\r\n{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Copy all files and sub-folders (including their files and folders recursively) to target folder. Returns true if operation successful.
        /// </summary>
        /// <param name="sourceDirectoryInfo">Source Directory</param>
        /// <param name="targetDirectoryInfo">Target Directory</param>
        private bool CopyFiles(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo)
        {
            try
            {
                foreach (FileInfo fileInfo in sourceDirectoryInfo.GetFiles())
                {
                    fileInfo.CopyTo(Path.Combine(targetDirectoryInfo.ToString(), fileInfo.Name), true);
                }
                foreach (DirectoryInfo subDirectory in sourceDirectoryInfo.GetDirectories())
                {
                    DirectoryInfo newDirectory = targetDirectoryInfo.CreateSubdirectory(subDirectory.Name);
                    CopyFiles(subDirectory, newDirectory);
                }
                Console.WriteLine($"Files copy successful. Source: {sourceDirectoryInfo.FullName} , Target: {targetDirectoryInfo.FullName}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Files copy unsuccessful. Source: {sourceDirectoryInfo.FullName} , Target: {targetDirectoryInfo.FullName}");
                Console.WriteLine(e.Message);
#if DEBUG
                throw e;
#endif
                return false;
            }
        }

        /// <summary>
        /// Delete target-folder files that don't exist in source-folder. Returns true if operation successful.
        /// </summary>
        /// <param name="sourceDirectoryInfo">Source directory where the existence of files will be controlled</param>
        /// <param name="targetDirectoryInfo">Target directory where the files will be deleted if they don't exist in source directory</param>
        private bool TrimFiles(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo)
        {
            try
            {
                foreach (FileInfo fileInfo in targetDirectoryInfo.GetFiles())
                {
                    FileInfo possibleSourceFileInfo = new FileInfo(Path.Combine(sourceDirectoryInfo.FullName, fileInfo.Name));
                    if (!File.Exists(possibleSourceFileInfo.FullName))
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
                foreach (DirectoryInfo targetSubDirectory in targetDirectoryInfo.GetDirectories())
                {
                    DirectoryInfo possibleSourceDirectoryInfo = new DirectoryInfo(Path.Combine(sourceDirectoryInfo.FullName, targetSubDirectory.Name));
                    if (!possibleSourceDirectoryInfo.Exists)
                    {
                        targetSubDirectory.Delete(true);
                    }
                    else
                    {
                        TrimFiles(possibleSourceDirectoryInfo, targetSubDirectory);
                    }
                }
                Console.WriteLine($"Files trim successful. Target: {targetDirectoryInfo.FullName} , Source: {sourceDirectoryInfo.FullName}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Files trim unsuccessful. Target: {targetDirectoryInfo.FullName} , Source: {sourceDirectoryInfo.FullName}");
                Console.WriteLine(e.Message);
#if DEBUG
                throw e;
#endif
                return false;
            }
        }

        bool IsFileReady(string filePath)
        {
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            fileSystemWatcher1.Dispose();
            fileSystemWatcher2.Dispose();

            button_Stop.Enabled = false;
            button_Apply.Enabled = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }

            else if (this.WindowState == FormWindowState.Normal  )
            {
                notifyIcon1.Visible = false;
            }
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }

}
