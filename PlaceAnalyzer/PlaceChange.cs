namespace PlaceAnalyzer; 
/// <summary>
/// Record for a change in color
/// </summary>
/// <param name="ColorBefore">The previous color of the pixel</param>
/// <param name="ColorAfter">The new color of the pixel</param>
/// <param name="Timestamp">The timestamp that the change occurred</param>
/// <param name="X">The X location of the change</param>
/// <param name="Y">The Y location of the change</param>
public record struct PlaceChange(byte ColorBefore, byte ColorAfter, int Timestamp, ushort X, ushort Y);
