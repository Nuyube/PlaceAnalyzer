using System;
using System.Drawing;
using System.Threading;

namespace PlaceAnalyzer; 
public static class DiffBuilder {

    //Counter for the label status updater
    public static int IMAGES_RENDERED { get; private set; } = 0;

    //Talking stick to hopefully avoid collisions in writing the number
    private static readonly object IMAGES_RENDERED_TALKING_STICK = new();

    /// <summary>
    /// Draws the diff map for a specified time and outputs to a location
    /// </summary>
    /// <param name="fstem">The file name stem for the output. See Remarks</param>
    /// <param name="Recency">The number of frames (~30s each) to draw</param>
    /// <remarks>
    /// <paramref name="fstem"/> is used to create the output file names. "_<paramref name="Recency"/>_idx.png" is appended to this value.
    /// For example, using an fstem of "C:/Dir/nice" will get you files like "C:/Dir/nice_100_595.png".
    /// </remarks>
    public static void RenderDiffs(string fstem, int Recency) {
        //Reset the images rendered counter
        IMAGES_RENDERED = 0;
        //Set the number of threads to run in this operation
        const int NUM_THREADS = 20;
        //Calculate how many images a thread should render
        int Partition = (int)Math.Ceiling((double)PlaceImage.CurrentTimestamp / NUM_THREADS);
        //Create our thread pool
        Thread[] threads = new Thread[NUM_THREADS];
        //Start the threads
        for (int i = 0; i < NUM_THREADS; i++) {
            threads[i] = new(thread_spin);
            threads[i].Start(i);
        }
        //Join them
        for (int i = 0; i < NUM_THREADS; i++) {
            threads[i].Join();
        }
        //Thread work function (objidx is int thread idx)
        void thread_spin(object objidx) {
            //Unbox our thread index
            int idx = (int)objidx;
            //Loop from (items threads run per idx * idx) to (items threads run per idx * (idx + 1)),
            // with an additional check to make sure we're not iterating our of range.
            for (int i = Partition * idx; (i < (Partition) * (idx + 1) && i < PlaceImage.CurrentTimestamp); i++) {
                //Get the diff bitmap from the History image.
                Bitmap b = Form1.History.GetDifferentialBitmap(i, Recency);
                //Literally all of this just draws it on white so it's opaque.
                Bitmap o = new(2000, 2000);
                Graphics g = Graphics.FromImage(o);
                g.Clear(Color.White);
                g.DrawImageUnscaled(b, 0, 0);
                g.Flush();
                g.Dispose();
                b.Dispose();

                //Save the image.
                o.Save(fstem + $"_{Recency}_{i}.png");
                //Dispose it to save memory
                o.Dispose();
                //Increment the IMAGES_RENDERED counter.
                lock (IMAGES_RENDERED_TALKING_STICK) {
                    IMAGES_RENDERED++;
                }
            }
        }
    }
}
