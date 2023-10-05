# PlaceAnalyzer

A framework for generating analyses of reddit.com/r/place.

This application is a rough work, and is almost certainly not the best way to do it, but I'm posting it in hopes that someone will find it useful.
This application is built for Windows using WinForms and .NET 6, built with Visual Studio.

The input data for this program can be found at https://place.thatguyalex.com/


## Usage
To get started with this program, first download the Raw Data Final V1 data from Alex's website above.
Then, extract it into a folder and copy that folder's path. The folder should contain all images (it doesn't matter if they're in subfolders).

Then, build and run PlaceStitcher. It will take all of the images and stitch them into large 2000x2000 canvas images, automatically ignoring tiles that don't exist yet.
The output for PlaceStitcher is the input for PlaceAnalyzer, so move the output images to a new folder and copy its path.

By default, PlaceAnalyzer is set up for generating a difference map (dmap) of the input data. There are currently no other analyses it can create.
To get started with it, paste the path to the stitched images into the "path to data" text box. Then, press Load and wait for it to complete.

After that, create a directory to put the differential data in. For example, I used C:/Users/Emera/Desktop/Diff. However, it's important to write a trailing slash on your 
data directory since the output file name is "<diff dump text box text>_recency_frame.png". Maybe it would be a good idea to write something like "C:/Users/Emera/Desktop/Diff/Place" into that textbox.

Then, set your recency (number of frames to draw for the diff map). One frame is approximately 30 seconds. The default is 50, or about 0.4167 hours that each frame represents. Keep in mind that longer recencies will equal longer processing time (at least, until it's optimized).

When you're ready, press Generate Diffs to generate the images. It'll be a PNG-Sequence for output, so if you were looking to create a video, this command is particularly useful to you:

`ffmpeg -r FPS -i "File name %d.png" -c:v codec -b:v bitrate video.mp4`

For example, my videos were rendered with

`ffmpeg -r 60 -i "\_250\_%d.png" -c:v hevc_nvenc -b:v 30M video.mp4`
