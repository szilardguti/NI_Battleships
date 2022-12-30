using System.Linq;
using Battleships.Model;
using Battleships.Model.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleships.Services.AIService.Tests
{
#pragma warning disable LRT001 // There is only one restricted namespace
#pragma warning disable NI1007 // Test classes must ultimately inherit from 'AutoTest'
    [TestClass]
    public class AIMoveServiceTests
    {
        [TestMethod]
        public void AIShouldChooseAnyTile()
        {
            // Given
            PlayBoardModel playBoard = GenerateEmptyPlayBoard();

            // When
            Tile nextTile = AIMoveService.GetAIMove(playBoard);

            // Then
            Assert.IsTrue(nextTile != null);
            Assert.IsTrue(nextTile.Y >= 0 && nextTile.X >= 0);
            Assert.IsTrue(nextTile.Y < Constants.PlayerBoardSize && nextTile.X < Constants.PlayerBoardSize);
        }

        [TestMethod]
        public void AIShouldChooseAdjacentToAlreadyHitTile()
        {
            // Given
            Tile expectedTileOne = new Tile()
            {
                X = 0,
                Y = 1,
                TileStatus = TileStatus.Empty
            };
            Tile expectedTileTwo = new Tile()
            {
                X = 1,
                Y = 0,
                TileStatus = TileStatus.Empty
            };
            PlayBoardModel playBoard = GenerateEmptyPlayBoard();
            playBoard.Tiles.First().TileStatus = TileStatus.HitShot;

            // When
            Tile nextTile = AIMoveService.GetAIMove(playBoard);

            // Then
            Assert.IsTrue(nextTile != null);
            Assert.IsTrue(nextTile.Equals(expectedTileOne) || nextTile.Equals(expectedTileTwo));
        }

        [TestMethod]
        public void AIShouldChooseTheOnlyEmptyTile()
        {
            // Given
            Tile expectedTileOne = new Tile()
            {
                X = 0,
                Y = 1,
                TileStatus = TileStatus.Empty
            };
            Tile expectedTileTwo = new Tile()
            {
                X = 1,
                Y = 0,
                TileStatus = TileStatus.Empty
            };
            PlayBoardModel playBoard = GenerateEmptyPlayBoard();
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 1).TileStatus = TileStatus.HitShot;
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 0).TileStatus = TileStatus.HitShot;
            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 0).TileStatus = TileStatus.MissShot;
            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 1).TileStatus = TileStatus.MissShot;
            playBoard.Tiles.First(tile => tile.X == 2 && tile.Y == 0).TileStatus = TileStatus.Destroyed;
            playBoard.Tiles.First(tile => tile.X == 2 && tile.Y == 1).TileStatus = TileStatus.Destroyed;

            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 2).TileStatus = TileStatus.Ship;

            // When
            Tile nextTile = AIMoveService.GetAIMove(playBoard);

            // Then
            Assert.IsTrue(nextTile != null);
            Assert.IsTrue(nextTile.X == 1 && nextTile.Y == 2);
            Assert.IsTrue(nextTile.TileStatus == TileStatus.Ship);
        }

        [TestMethod]
        public void AIShouldNotChooseAlreadyDestroyedShip()
        {
            // Given
            Tile expectedTileOne = new Tile()
            {
                X = 0,
                Y = 0,
                TileStatus = TileStatus.HitShot
            };
            Tile expectedTileTwo = new Tile()
            {
                X = 1,
                Y = 0,
                TileStatus = TileStatus.Destroyed
            };
            Tile expectedTileThree = new Tile()
            {
                X = 0,
                Y = 1,
                TileStatus = TileStatus.Destroyed
            };
            Tile expectedTileFour = new Tile()
            {
                X = 1,
                Y = 1,
                TileStatus = TileStatus.Destroyed
            };
            PlayBoardModel playBoard = GenerateEmptyPlayBoard();
            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 0).TileStatus = TileStatus.HitShot;
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 0).TileStatus = TileStatus.Destroyed;
            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 1).TileStatus = TileStatus.Destroyed;
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 1).TileStatus = TileStatus.Destroyed;

            // When
            Tile nextTile = AIMoveService.GetAIMove(playBoard);

            // Then
            Assert.IsTrue(nextTile != null);
            Assert.IsFalse(nextTile.Equals(expectedTileOne));
            Assert.IsFalse(nextTile.Equals(expectedTileTwo));
            Assert.IsFalse(nextTile.Equals(expectedTileThree));
            Assert.IsFalse(nextTile.Equals(expectedTileFour));
            Assert.IsTrue(nextTile.TileStatus == TileStatus.Empty);
        }

        [TestMethod]
        public void AIShouldChooseCorrectTileInASequence()
        {
            // Given
            PlayBoardModel playBoard = GenerateEmptyPlayBoard();
            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 0).TileStatus = TileStatus.HitShot;
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 0).TileStatus = TileStatus.MissShot;
            playBoard.Tiles.First(tile => tile.X == 1 && tile.Y == 1).TileStatus = TileStatus.MissShot;

            playBoard.Tiles.First(tile => tile.X == 0 && tile.Y == 2).TileStatus = TileStatus.Ship;

            // When
            Tile midTile = AIMoveService.GetAIMove(playBoard);
            playBoard.Tiles.First(tile => tile.Equals(midTile)).TileStatus = TileStatus.HitShot;
            Tile nextTile = AIMoveService.GetAIMove(playBoard);

            // Then
            Assert.IsTrue(nextTile != null);
            Assert.IsTrue(nextTile.X == 0 && nextTile.Y == 2);
            Assert.IsTrue(nextTile.TileStatus == TileStatus.Ship);
        }

        private PlayBoardModel GenerateEmptyPlayBoard()
        {
            PlayBoardModel resultBoard = new PlayBoardModel();

            foreach (Tile tile in resultBoard.Tiles)
            {
                tile.TileStatus = TileStatus.Empty;
            }
            return resultBoard;
        }
    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
#pragma warning restore LRT001 // There is only one restricted namespace
}