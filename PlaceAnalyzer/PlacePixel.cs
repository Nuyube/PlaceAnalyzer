namespace PlaceAnalyzer; 
public struct PlacePixel {

    /// <summary>
    /// Holds the X position of this pixel
    /// </summary>
    public ushort X { get; private set; }

    /// <summary>
    /// Holds the Y position of this pixel
    /// </summary>
    public ushort Y{ get; private set; }

    /// <summary>
    /// Holds the color index of this pixel
    /// </summary>
    /// <seealso cref="PlaceColor"/>
    public byte Color { get; set; }

    /// <summary>
    /// Constructs a new PlacePixel
    /// </summary>
    /// <param name="x">The X position of the pixel</param>
    /// <param name="y">The Y position of the pixel</param>
    /// <param name="color">The color index of the pixel, or white if unspecified.</param>
    public PlacePixel(ushort x, ushort y, byte color = 0xff) {
        Color = color == 0xff ? PlaceColor.IdxWhite : color;
        X = x;
        Y = y;
    }
}
