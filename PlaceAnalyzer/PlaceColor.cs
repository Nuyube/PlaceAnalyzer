using System.Collections.Generic;
using System.Drawing;

namespace PlaceAnalyzer; 
public static class PlaceColor {

    /// <summary>
    /// Full color table. It's kind of shit but it works.
    /// </summary>
    public static readonly Color[] colorTable = {
        Color.FromArgb(255,255,69,0),
        Color.FromArgb(255,109,72,47),
        Color.FromArgb(255,156,105,38),
        Color.FromArgb(255,255,168,0),
        Color.FromArgb(255,255,214,53),
        Color.FromArgb(255,126,237,86),
        Color.FromArgb(255,0,204,120),
        Color.FromArgb(255,0,163,104),
        Color.FromArgb(255,0,158,170),
        Color.FromArgb(255,36,80,164),
        Color.FromArgb(255,129,30,159),
        Color.FromArgb(255,180,74,192),
        Color.FromArgb(255,81,233,244),
        Color.FromArgb(255,54,144,234),
        Color.FromArgb(255,255,153,170),
        Color.FromArgb(255,0,0,0),
        Color.FromArgb(255,137,141,144),
        Color.FromArgb(255,212,215,217),
        Color.FromArgb(255,255,255,255),
        Color.FromArgb(255,255,56,129),
        Color.FromArgb(255,73,58,193),
        Color.FromArgb(255,190,0,57),
        Color.FromArgb(255,0,117,111),
        Color.FromArgb(255,106,92,255),
        Color.FromArgb(255,222,16,127),
        Color.FromArgb(255,0,204,192),
        Color.FromArgb(255,148,179,255),
        Color.FromArgb(255,255,248,184),
        Color.FromArgb(255,81,82,82),
        Color.FromArgb(255,109,0,26),
        Color.FromArgb(255,255,180,112),
        Color.FromArgb(255,228,171,255),
    };

    /// <summary>
    /// Holds a cached value of the index of white to speed up image initialization
    /// </summary>
    private static byte? idxWhiteCached = null;

    /// <summary>
    /// Gets the color table index of white
    /// </summary>
    public static byte IdxWhite => GetWhite();

    ///<summary>Gets and caches or gets the cached value of white.</summary>
    private static byte GetWhite() {
        if (idxWhiteCached == null)
            idxWhiteCached = IdxOf(Color.White);
        return idxWhiteCached.Value;
    }

    /// <summary>
    /// Holds the map of colors to their indices
    /// </summary>
    private static Dictionary<Color, byte> colorMapping;

    /// <summary>
    /// Returns the index of a specific color
    /// </summary>
    /// <param name="c">The color to find an index of</param>
    /// <returns>The color's index, if found, or 0xFF otherwise.</returns>
    public static byte IdxOf(Color c) {
        if (colorMapping == null) {
            colorMapping = new();
            for (byte i = 0; i < colorTable.Length; i++) {
                colorMapping.Add(colorTable[i], i);
            }
        }

        return colorMapping.TryGetValue(c, out byte val) ? val : (byte)0xff;
    }
}
