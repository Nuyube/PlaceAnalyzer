using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlaceAnalyzer;

public class PlaceImage {

    // #Configuration If you're dealing with a single tile you might want to consider changing these to be 1k each.
    /// <summary>
    /// Holds the height of images
    /// </summary>
    public const int IMAGE_HEIGHT = 2000;

    /// <summary>
    /// Holds the width of images
    /// </summary>
    public const int IMAGE_WIDTH = 2000;

    /// <summary>
    /// Holds the current number of procressed images
    /// </summary>
    public static int CurrentTimestamp { get; private set; } = 0;

    /// <summary>
    /// Used to prevent collissions when incrementing the timestamp
    /// </summary>
    private static readonly object currentTimestampTalkingStick = new();

    /// <summary>
    /// Increments the timestamp by one
    /// </summary>
    public static void IncrementTimestamp() {
        lock (currentTimestampTalkingStick) {
            CurrentTimestamp++;
        }
    }

    /// <summary>
    /// Holds the internal pixel data
    /// </summary>
    private readonly PlacePixel[,] Data;

    /// <summary>
    /// Holds a list of changes made to the image
    /// </summary>
    public List<PlaceChange> Changes { get; private set; }

    /// <summary>
    /// Gets the differences between two points in time.
    /// </summary>
    /// <param name="beginTimestamp">The beginning frame index</param>
    /// <param name="duration">The number of frames to process for</param>
    /// <returns>The image containing only the differences between the two points</returns>
    /// <exception cref="ArgumentException">If either argument is invalid. See Remarks.</exception>
    /// <remarks>
    /// An ArgumentException is thrown if <paramref name="beginTimestamp"/> is past <see cref="CurrentTimestamp"/> (out of range).
    /// However, if <paramref name="beginTimestamp"/> is less than 0, it is updated to be zero.
    /// <paramref name="duration"/> Can be any positive integer that is not zero. If it runs past the end of History(tm), it'll be capped
    /// so it won't fail.
    /// </remarks>
    public Bitmap GetDifferentialBitmap(int beginTimestamp, int duration) {
        //Get the maximum possible timestamp and proposed end timestamp
        int MaximumTimestamp = CurrentTimestamp;
        int EndTimestamp = beginTimestamp + duration;
        //If the beginning timestamp is out of range, throw an exception
        if (beginTimestamp > MaximumTimestamp) {
            throw new ArgumentException("Begin was past the maximum possible timestamp!", nameof(beginTimestamp));
        }
        //If it's too low, just update it to be zero.
        else if (beginTimestamp < 0) {
            beginTimestamp = 0;
        }
        //If the duration is zero or lower, throw an exception.
        if (duration <= 0) {
            throw new ArgumentException("Duration was negative or zero!", nameof(duration));
        }
        //If the end timestamp is too far in the future, just cap it to its max value.
        if (EndTimestamp > MaximumTimestamp) {
            EndTimestamp = MaximumTimestamp;
        }

        //Create a new image to store the changes in
        Bitmap b = new(IMAGE_WIDTH, IMAGE_HEIGHT);
        //Lock the bits so we can change them directly.
        BitmapData bd = b.LockBits(new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

        // #Optimizable: We rewrite pixels that have already been written. Iterating backwards and looking for transparent pixels for our changes might be faster?
        // Especially at large sizes.
        unsafe {
            //For each change in that range,
            IEnumerable<PlaceChange> relevantChanges = Changes.Where(x => x.Timestamp >= beginTimestamp && x.Timestamp <= EndTimestamp);
            //Iterate through them
            foreach (PlaceChange change in relevantChanges) {
                //And change the color of that pixel to what it should be.
                int* pointedData = (int*)(bd.Scan0 + ((change.Y * IMAGE_WIDTH + change.X) * 4));
                *pointedData = PlaceColor.colorTable[change.ColorAfter].ToArgb();
            }
        }
        //Unlock the bitmap and return it.
        b.UnlockBits(bd);
        return b;
    }

    /// <summary>
    /// Creates a new PlaceImage
    /// </summary>
    public PlaceImage() {
        //Create our data array
        Data = new PlacePixel[IMAGE_HEIGHT, IMAGE_WIDTH];
        //Create our changes list, allowing for an initial capacity of 16 Million.
        //You may want to change this #Configuration.
        Changes = new(16000000);
    }

    /// <summary>
    /// Clears this image to white.
    /// </summary>
    public void Clear() {
        for (ushort y = 0; y < IMAGE_HEIGHT; y++)
            for (ushort x = 0; x < IMAGE_WIDTH; x++) {
                Data[x, y] = new PlacePixel(x, y, PlaceColor.IdxWhite);
            }
    }

    // #Unused AddNewImage is significantly faster and loading two images and adding them.
    /// <summary>
    /// Creates a PlaceImage from an image
    /// </summary>
    /// <param name="i">The image to read from</param>
    /// <returns>The PlaceImage object created</returns>
    /// <exception cref="NotSupportedException">Thrown if any pixel in the image contains an unknown color.
    /// <see cref="PlaceColor"/></exception>
    public static PlaceImage FromImage(Bitmap i) {
        //Number of threads to use to read the image. #Configurable. Should divide into IMAGE_HEIGHT evenly.
        const int NUM_THREADS = 4;
        //Create a new image to work from
        PlaceImage res = new();
        //Lock the image data
        BitmapData bdata = i.LockBits(new Rectangle(0, 0, i.Width, i.Height),
            ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);
        unsafe {
            //Create, start, and join our thread pool.
            Thread[] threads = new Thread[NUM_THREADS];
            for (int z = 0; z < threads.Length; z++) {
                threads[z] = new Thread(thread_work);
                threads[z].Start(z);
            }
            for (int z = 0; z < threads.Length; z++) {
                threads[z].Join();
            }
            //Thread work function
            void thread_work(object state) {
                //Extract the thread index from the state object.
                int tdx = (int)state;
                //For the lines that this thread selects
                for (ushort y = (ushort)(tdx * (IMAGE_HEIGHT / NUM_THREADS)); y < (tdx + 1) * (IMAGE_HEIGHT / NUM_THREADS); y++) { 
                    //And all of the width
                    for (ushort x = 0; x < IMAGE_WIDTH; x++) {
                        //Get the pointer to the data
                        int* data = (int*)(bdata.Scan0 + (4 * ((y * IMAGE_WIDTH) + x)));
                        //Get the color pointed at by that pointer
                        Color col = Color.FromArgb(*data);
                        //Get the color table index of that color
                        byte idx = PlaceColor.IdxOf(col);
                        //If it's 0xFF, throw an exception (the pixel's color is unknown).
                        // #KindaShit: We could probably modify the code to simply append it to the color list but considering
                        // they're not going to be adding any more colors anytime soon I think this is probably fine.
                        if (idx == 0xff) throw new NotSupportedException($"Invalid color {i.GetPixel(x, y)} at {x}, {y}!");
                        //Else, create a new pixel for that value.
                        else {
                            res.Data[x, y] = new PlacePixel(x, y, idx);
                        }
                    }
                }
            }
        }
        //Unlock the bits and return the result.
        i.UnlockBits(bdata);
        return res;
    }

    /// <summary>
    /// Adds an image to this one
    /// </summary>
    /// <param name="FilePath">The path of the image file</param>
    public void AddNextImage(string FilePath) {
        try {
            //Number of threads to run on #Configurable. Should divide evenly into IMAGE_HEIGHT.
            const int NUM_THREADS = 50;
            //Read the file.
            Bitmap i = Image.FromFile(FilePath) as Bitmap;
            //Lock the image data
            BitmapData bdata = i.LockBits(new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            unsafe {
                //Threading stuff.
                Thread[] threads = new Thread[NUM_THREADS];
                for (int z = 0; z < threads.Length; z++) {
                    threads[z] = new Thread(thread_work);
                    threads[z].Start(z);
                }
                for (int z = 0; z < threads.Length; z++) {
                    threads[z].Join();
                }
                //Thread function
                void thread_work(object state) {
                    //Read thread index
                    int tdx = (int)state;
                    //For each line this thread selects
                    for (ushort y = (ushort)(tdx * (IMAGE_HEIGHT / NUM_THREADS)); y < (tdx + 1) * (IMAGE_HEIGHT / NUM_THREADS); y++)
                        //And all of the width,
                        for (ushort x = 0; x < IMAGE_WIDTH; x++) {
                            try {
                                //Get the pointer to the color data
                                int* data = (int*)(bdata.Scan0 + (4 * ((y * IMAGE_WIDTH) + x)));
                                //Get the color pointed to by that pointer
                                Color col = Color.FromArgb(*data);
                                //Get the index of the color
                                byte idx = PlaceColor.IdxOf(col);
                                //If the color wasn't found, throw an exception
                                if (idx == 0xff)
                                    throw new NotSupportedException($"Invalid color {col} at {x}, {y}!");

                                //Else, if the colors don't match,
                                if (Data[x, y].Color != idx) {
                                    //Change the color at this position
                                    Data[x, y].Color = idx;
                                    //Lock the changes database
                                    lock (Changes) {
                                        //And add a new record.
                                        Changes.Add(new PlaceChange(Data[x, y].Color, idx, CurrentTimestamp, x, y));
                                    }
                                }
                                //If an exception occurs (shouldn't happen now with official data), show a message box.
                            } catch (Exception ex) {
                                MessageBox.Show(ex.StackTrace, ex.Message);
                            }
                        }
                }
            }
            //Unlock the image data and dispose it
            // (since we were only using the image to change color data and append Changes)
            i.UnlockBits(bdata);
            i.Dispose();
            //If any funny business happened, show a messagebox
        } catch (Exception e) {
            MessageBox.Show(e.StackTrace, e.Message);
        }
    }
}