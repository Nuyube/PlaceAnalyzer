using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlaceAnalyzer {

    public partial class Form1 : Form {

        //Literally just to allow me to write to the log from other threads.
        // It's a pretty garbage way to do it, but eh
        public static Queue<string> MessageQueue { get; private set; } = new(4096);

        //Processor threads
        private Thread historyThread;

        private Thread diffThread;

        //The amalgamate image for the History that could have totally been under HistoryBuilder
        // but I did it here for some reason.
        public static PlaceImage History { get; private set; } = null;

        public Form1() {
            InitializeComponent();
            logTimer.Start();
        }

        public static void ResetHistory() {
            History = new PlaceImage();
            History.Clear();
        }

        //Try to guess the input image path (input images should be on white, r/place 2kx2k stitched canvases).
        private void LOAD_FORM(object sender, EventArgs e) {
            textBox1.Text = Environment.CurrentDirectory + "/Data";
        }

        /// <summary>
        /// Starts the loading process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LOAD_BUTTON_CLICK(object sender, EventArgs e) {
            //If the path is invalid, notify the user
            if (!(new DirectoryInfo(diffPath.Text).Exists)) {
                loadStatusLabel.Text = "Invalid path!";
                return;
            }
            //Start the load in another thread so the timer can continue to update the UI
            historyThread = new Thread(PlaceHistoryBuilder.Work);
            historyThread.Start(textBox1.Text);
            //Start the status update timer
            loadStatusTimer.Start();
        }

        /// <summary>
        /// Updates the status of loading each tick
        /// </summary>
        private void LOAD_STATUS_TICK(object sender, EventArgs e) {
            loadStatusLabel.Text = $"{PlaceImage.CurrentTimestamp}/{PlaceHistoryBuilder.NumImages}";
            changeCountLabel.Text = $"Total Changes: {History.Changes.Count}";
        }

        /// <summary>
        /// Updates the log every tick
        /// </summary>
        private void LOG_TICK(object sender, EventArgs e) {
            //While anything is in the queue
            while (MessageQueue.Any()) {
                //Get the item
                string item = MessageQueue.Dequeue();
                if (item == null) continue;
                //Set the selection to the end and append the item
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.AppendText(item + "\n");
                //Set the selection to the end again to force a scroll.
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }
        }

        /// <summary>
        /// Kills the app when the form is closed
        /// </summary>
        private void CLOSE_FORM(object sender, FormClosedEventArgs e) {
            //Kill the app to close threads that may still be running.
            Environment.Exit(0);
        }

        /// <summary>
        /// Run when the user presses the "Generate Diffs" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CREATE_DIFFS_CLICK(object sender, EventArgs e) {
            //If the path is invalid, create it
            if (!(new DirectoryInfo(diffPath.Text).Exists)) {
                try {
                    Directory.CreateDirectory(diffPath.Text);
                } catch {
                    diffStatusLabel.Text = "Invalid path!";
                    return;
                }
            }
            //Start the status timer
            diffStatusTimer.Start();

            //Create a thread to run the diff calculation and start it with the selected recency
            diffThread = new Thread(START_DIFFS_THREAD);
            diffThread.Start(Tuple.Create(diffPath.Text, (int)recencyNUD.Value));
        }

        /// <summary>
        /// Starts the diff creation in a thread so the window doesn't freeze when it's running
        /// </summary>
        private void START_DIFFS_THREAD(object items) {
            Tuple<string, int> item = (Tuple<string, int>)items;
            DiffBuilder.RenderDiffs(item.Item1, item.Item2);
        }

        /// <summary>
        /// Runs every 100ms to update the diff status label
        /// </summary>
        private void DIFF_STATUS_TICK(object sender, EventArgs e) {
            diffStatusLabel.Text = $"Diffs: {DiffBuilder.IMAGES_RENDERED} / {PlaceImage.CurrentTimestamp}";
        }
    }
}