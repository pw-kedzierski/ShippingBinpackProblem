namespace BinPackingApp.Core.Models;

public class UnplacedShip
{
    public ShipDimensions Dimensions { get; set; } = new();
    public string Designation { get; set; } = string.Empty;
    public int RemainingCount { get; set; }
    public bool IsRotated { get; set; } = false;
}
