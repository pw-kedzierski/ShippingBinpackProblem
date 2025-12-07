using BinPackingApp.Core.Models;

namespace BinPackingApp.Core.Services;

public interface IBinPackService
{
    bool CanPlaceShip(PlacedShip ship, List<PlacedShip> placedShips, int anchorageWidth, int anchorageHeight);
    void PlaceShip(PlacedShip ship, List<PlacedShip> placedShips);
    void RemoveShip(int shipId, List<PlacedShip> placedShips);
    PlacedShip? GetShipAt(int x, int y, List<PlacedShip> placedShips);
}
