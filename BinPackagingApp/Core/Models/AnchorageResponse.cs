namespace BinPackingApp.Core.Models;

public class AnchorageResponse
{
    public AnchorageSize AnchorageSize { get; set; } = new();
    public List<Fleet> Fleets { get; set; } = [];
}

public class AnchorageSize
{
    public int Width { get; set; }
    public int Height { get; set; }
}

public class Fleet
{
    public ShipDimensions SingleShipDimensions { get; set; } = new();
    public string ShipDesignation { get; set; } = string.Empty;
    public int ShipCount { get; set; }
}

public class ShipDimensions
{
    public int Width { get; set; }
    public int Height { get; set; }
}
