using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;

namespace PlaceAnalyzer; 
public static class PlaceHistoryBuilder {
    public static int NumImages { get; private set; } = 0;

    public static void Work(object val) {
        //Slow down the garbage collector? The memory for this app is a hell of a lot better than it used to be so
        // this may not help much.
        GCSettings.LatencyMode = GCLatencyMode.LowLatency;
        //Get the directory we're working in
        DirectoryInfo di = new ((string)val);
        if (di.Exists) {
            //Get all PNG files
            IEnumerable<FileInfo> files = di.GetFiles("*.png");
            //Set the NumImages field to how many images we have to read
            NumImages = files.Count();
            //Output that we have X images to read
            Form1.MessageQueue.Enqueue($"{NumImages} images to read.");
            //Create the history image and clear it to white
            Form1.ResetHistory();
            foreach (FileInfo file in files) {
                //Add the next image to the history and catalogue its changes
                Form1.History.AddNextImage(file.FullName);
                //Increment the current timestamp.
                PlaceImage.IncrementTimestamp();
            }
        }
        else {
            //This should be impossible since we check it during spawning this thread but we can check it anyway.
            throw new DirectoryNotFoundException("The directory doesn't exist.");
        }
        //Reset the garbage collector
        GCSettings.LatencyMode = GCLatencyMode.Interactive;
    }
}
