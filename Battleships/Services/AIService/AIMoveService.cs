using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships.Model;
using Battleships.Model.Helpers;

namespace Battleships.Services.AIService
{
    public static class AIMoveService
    {
        public static Tile GetAIMove(PlayBoardModel enemyPlayBoard)
        {
            Random rand = new Random();
            if (HasUnfinishedShip(enemyPlayBoard, out Tile unfinishedShipTile))
            {
                List<Tile> possibleTiles = new List<Tile>();
                if (unfinishedShipTile.X != 0)
                {
                    Tile currentTile = enemyPlayBoard.GetTile(unfinishedShipTile.X - 1, unfinishedShipTile.Y);
                    if (currentTile.TileStatus != TileStatus.MissShot &&
                        currentTile.TileStatus != TileStatus.HitShot &&
                        currentTile.TileStatus != TileStatus.Destroyed)
                    {
                        possibleTiles.Add(currentTile);
                    }
                }
                if (unfinishedShipTile.X != Constants.PlayerBoardSize - 1)
                {
                    Tile currentTile = enemyPlayBoard.GetTile(unfinishedShipTile.X + 1, unfinishedShipTile.Y);
                    if (currentTile.TileStatus != TileStatus.MissShot &&
                        currentTile.TileStatus != TileStatus.HitShot &&
                        currentTile.TileStatus != TileStatus.Destroyed)
                    {
                        possibleTiles.Add(currentTile);
                    }
                }
                if (unfinishedShipTile.Y != 0)
                {
                    Tile currentTile = enemyPlayBoard.GetTile(unfinishedShipTile.X, unfinishedShipTile.Y - 1);
                    if (currentTile.TileStatus != TileStatus.MissShot &&
                        currentTile.TileStatus != TileStatus.HitShot &&
                        currentTile.TileStatus != TileStatus.Destroyed)
                    {
                        possibleTiles.Add(currentTile);
                    }
                }
                if (unfinishedShipTile.Y != Constants.PlayerBoardSize - 1)
                {
                    Tile currentTile = enemyPlayBoard.GetTile(unfinishedShipTile.X, unfinishedShipTile.Y + 1);
                    if (currentTile.TileStatus != TileStatus.MissShot &&
                        currentTile.TileStatus != TileStatus.HitShot &&
                        currentTile.TileStatus != TileStatus.Destroyed)
                    {
                        possibleTiles.Add(currentTile);
                    }
                }

                return possibleTiles[rand.Next(possibleTiles.Count)];
            }
            List<Tile> emptyTiles = enemyPlayBoard.Tiles.Where(tile => tile.TileStatus == TileStatus.Empty).ToList();
            return emptyTiles[rand.Next(emptyTiles.Count)];
        }

        private static bool HasUnfinishedShip(PlayBoardModel enemyPlayBoard, out Tile unfinishedShipTile)
        {
            foreach (Tile shipTile in enemyPlayBoard.Tiles.Where(tile => tile.TileStatus == TileStatus.HitShot))
            {
                if (shipTile.Y != 0)
                {
                    if (enemyPlayBoard.GetTile(shipTile.X, shipTile.Y - 1).TileStatus == TileStatus.Empty)
                    {
                        unfinishedShipTile = shipTile;
                        return true;
                    }

                    if (shipTile.X != 0)
                    {
                        if (enemyPlayBoard.GetTile(shipTile.X - 1, shipTile.Y - 1).TileStatus == TileStatus.Empty)
                        {
                            unfinishedShipTile = shipTile;
                            return true;
                        }
                    }

                    if (shipTile.X != Constants.PlayerBoardSize - 1)
                    {
                        if (enemyPlayBoard.GetTile(shipTile.X + 1, shipTile.Y - 1).TileStatus == TileStatus.Empty)
                        {
                            unfinishedShipTile = shipTile;
                            return true;
                        }
                    }
                }

                if (shipTile.Y != Constants.PlayerBoardSize - 1)
                {
                    if (enemyPlayBoard.GetTile(shipTile.X, shipTile.Y + 1).TileStatus == TileStatus.Empty)
                    {
                        unfinishedShipTile = shipTile;
                        return true;
                    }

                    if (shipTile.X != 0)
                    {
                        if (enemyPlayBoard.GetTile(shipTile.X - 1, shipTile.Y + 1).TileStatus == TileStatus.Empty)
                        {
                            unfinishedShipTile = shipTile;
                            return true;
                        }
                    }

                    if (shipTile.X != Constants.PlayerBoardSize - 1)
                    {
                        if (enemyPlayBoard.GetTile(shipTile.X + 1, shipTile.Y + 1).TileStatus == TileStatus.Empty)
                        {
                            unfinishedShipTile = shipTile;
                            return true;
                        }
                    }
                }

                if (shipTile.X != 0)
                {
                    if (enemyPlayBoard.GetTile(shipTile.X - 1, shipTile.Y).TileStatus == TileStatus.Empty)
                    {
                        unfinishedShipTile = shipTile;
                        return true;
                    }
                }

                if (shipTile.X != Constants.PlayerBoardSize - 1)
                {
                    if (enemyPlayBoard.GetTile(shipTile.X + 1, shipTile.Y).TileStatus == TileStatus.Empty)
                    {
                        unfinishedShipTile = shipTile;
                        return true;
                    }
                }
            }

            unfinishedShipTile = null;
            return false;
        }
    }
}
