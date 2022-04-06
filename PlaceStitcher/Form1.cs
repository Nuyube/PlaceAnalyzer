using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlaceStitcher {

    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        /// <summary>
        /// Runs when the It's What I Do(tm) button is pressed.
        /// </summary>
        private void BUTTON_CLICK(object sender, EventArgs e) {
            //Gets the directory info from the textbox
            DirectoryInfo dinf = new DirectoryInfo(path.Text);
            if (!dinf.Exists) {
                Text = "Directory doesn't exist!";
                return;
            }
            //Get all files in the directory that start with 0- (top-left tile)
            FileInfo[] allFiles = dinf.GetFiles("0-.png", SearchOption.AllDirectories);
            //Create a queue of these quads.
            Queue<FileInfo> zeroquads = new Queue<FileInfo>(allFiles);
            //Thread stuff
            Thread[] herewego = new Thread[16];
            for (int i = 0; i < 16; i++) {
                herewego[i] = new Thread(thread_spin);
                herewego[i].Start();
            }
            for (int i = 0; i < 16; i++) {
                herewego[i].Join();
            }
            //Thread work function - while there's data to process, process it.
            void thread_spin() {
                while (zeroquads.Count > 0) {
                    FileInfo item = null;
                    lock (zeroquads) {
                        item = zeroquads.Dequeue();
                    }
                    if (item != null) {
                        thread_work(item);
                    }
                }
            }
            //File work function
            void thread_work(FileInfo file) {
                //Calculate our output file name
                string fname = file.Name.Replace("0-", "Amalgam-");
                //Create our output image and graphics
                Bitmap b = new Bitmap(2000, 2000);
                Graphics g = Graphics.FromImage(b);
                //Set a background of white.
                g.Clear(Color.White);
                //Find the files for the four quadrants
                FileInfo q0 = file;
                FileInfo q1 = allFiles.FirstOrDefault(x => x.Name == file.Name.Replace("0-", "1-"));
                FileInfo q2 = allFiles.FirstOrDefault(x => x.Name == file.Name.Replace("0-", "2-"));
                FileInfo q3 = allFiles.FirstOrDefault(x => x.Name == file.Name.Replace("0-", "3-"));

                //And now stitch them together
                void draw_image(FileInfo inputFile, int x, int y) {
                    if (inputFile != null && inputFile.Exists) {
                        Bitmap a = (Bitmap)Image.FromFile(inputFile.FullName);
                        g.DrawImageUnscaled(a, x, y);
                        g.Flush();
                        a.Dispose();
                    }
                }
                //Draw each quadrant
                draw_image(q0, 0, 0);
                draw_image(q1, 1000, 0);
                draw_image(q2, 0, 1000);
                draw_image(q3, 1000, 1000);
                g.Dispose();
                //Write the file
                b.Save(path.Text + "/" + fname);
                //Dispose the image.
                b.Dispose();
            }
        }
    }
}