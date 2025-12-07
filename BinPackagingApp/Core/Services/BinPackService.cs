using BinPackingApp.Core.Models;

namespace BinPackingApp.Core.Services;

public class BinPackService : IBinPackService
{
    public bool CanPlaceShip(PlacedShip ship, List<PlacedShip> placedShips, int anchorageWidth, int anchorageHeight)
    {
        if (!ship.IsWithinBounds(anchorageWidth, anchorageHeight))
        {
            return false;
        }

        for (int i = 0; i < placedShips.Count; i++)
        {
            var placed = placedShips[i];
            if (placed.Id != ship.Id && ship.Intersects(placed))
            {
                return false;
            }
        }

        return true;
    }

    public void PlaceShip(PlacedShip ship, List<PlacedShip> placedShips)
    {
        for (int i = 0; i < placedShips.Count; i++)
        {
            if (placedShips[i].Id == ship.Id)
            {
                placedShips[i].X = ship.X;
                placedShips[i].Y = ship.Y;
                placedShips[i].IsRotated = ship.IsRotated;
                return;
            }
        }

        placedShips.Add(ship);
    }

    public void RemoveShip(int shipId, List<PlacedShip> placedShips)
    {
        for (int i = 0; i < placedShips.Count; i++)
        {
            if (placedShips[i].Id == shipId)
            {
                placedShips.RemoveAt(i);
                return;
            }
        }
    }

    public PlacedShip? GetShipAt(int x, int y, List<PlacedShip> placedShips)
    {
        for (int i = 0; i < placedShips.Count; i++)
        {
            var ship = placedShips[i];
            if (x >= ship.X &&
                x < ship.X + ship.ActualWidth &&
                y >= ship.Y &&
                y < ship.Y + ship.ActualHeight)
            {
                return ship;
            }
        }

        return null;
    }
}
