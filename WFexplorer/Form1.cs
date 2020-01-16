﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFexplorer
{
    public partial class Form1 : Form
    {
        public static DateTime dt = new DateTime();
        public static string outputFileDirectory = @"C:\Temp\FileCleaner\cleaner-result.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            checkForDelete();
            string value = textBox1.Text;
            GetSubDirectories(@value);
            Clipboard.SetText(outputFileDirectory);
            label2.Text = "Scan complete, output file copied to clipboard: " + outputFileDirectory;
            label2.Visible = true;
        }

        public static void GetSubDirectories(string Path)
        {
            createFileLocation();

            try
            {
                using (System.IO.StreamWriter addToFile = new System.IO.StreamWriter(outputFileDirectory, true))
                {
                    addToFile.WriteLine("Original Directory: " + Path);
                }
            }
            catch (System.IO.IOException e)
            {
                //log value here
            }
            getFilesInDirectory(Path);
            string[] subdirectoryEntries = Directory.GetDirectories(Path);
            foreach (string subdirectory in subdirectoryEntries)
            {
                LoadSubDirs(subdirectory);
            }
        }
        private static void LoadSubDirs(string dir)

        {
            try
            {
                using (System.IO.StreamWriter addToFile = new System.IO.StreamWriter(outputFileDirectory, true))
                {
                    addToFile.WriteLine("Directory: " + dir);
                }
            }
            catch (System.IO.IOException e)
            {
                //log value here
            }
            getFilesInDirectory(dir);
            string[] subdirectoryEntries = Directory.GetDirectories(dir);

            foreach (string subdirectory in subdirectoryEntries)
            {
                LoadSubDirs(subdirectory);
            }
        }

        private static void getFilesInDirectory(string currentDir)
        {
            string[] filesInDir = Directory.GetFiles(currentDir);
            foreach (string file in filesInDir)
            {
                Console.WriteLine("\tFile located: " + file + ", Length in Bytes: " + file.Length);

                try
                {
                    using (System.IO.StreamWriter addToFile = new System.IO.StreamWriter(outputFileDirectory, true))
                    {
                        addToFile.WriteLine("\tFile located: " + file + ", Length in Bytes: " + file.Length);
                    }
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine("Exception: " + e);

                }
            }
        }

        private static void checkForDelete()
        {
            File.Delete(outputFileDirectory);
        }

        private static void createFileLocation()
        {

            string path = @"C:\temp\FileCleaner";

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    // Create the file, or overwrite if the file exists.
                    outputFileDirectory += dt;
                    File.Create(outputFileDirectory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}