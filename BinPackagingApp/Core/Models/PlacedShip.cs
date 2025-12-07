namespace BinPackingApp.Core.Models;

public class PlacedShip
{
    public int Id { get; set; }
    public ShipDimensions Dimensions { get; set; } = new();
    public string Designation { get; set; } = string.Empty;
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsRotated { get; set; }

    public int ActualWidth => IsRotated ? Dimensions.Height : Dimensions.Width;
    public int ActualHeight => IsRotated ? Dimensions.Width : Dimensions.Height;

    public bool Intersects(PlacedShip other)
    {
        return X < other.X + other.ActualWidth &&
               X + ActualWidth > other.X &&
               Y < other.Y + other.ActualHeight &&
               Y + ActualHeight > other.Y;
    }

    public bool IsWithinBounds(int anchorageWidth, int anchorageHeight)
    {
        return X >= 0 &&
               Y >= 0 &&
               X + ActualWidth <= anchorageWidth &&
               Y + ActualHeight <= anchorageHeight;
    }
}
