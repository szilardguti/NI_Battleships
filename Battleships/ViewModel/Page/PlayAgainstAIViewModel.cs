using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.Services.AIService;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public PlayAgainstAIViewModel() : base()
        {
            Player1Model.Player.Name = string.Empty;
            Player2Model.Player.Name = "Robot kapitány";
            Player2Model.Player.IsARobot = true;

            Player2Ready = true;
        }
        public override void RobotPlay()
        {
            do
            {
                Tile tile = AIMoveService.GetAIMove(Player1Model);
                TileStatus status = tile.TileStatus;

                if (status == TileStatus.Ship)
                {
                    Tuple<bool, bool> winAndDestroy = Player1Model.Hit(tile.X, tile.Y);
                    Winner = winAndDestroy.Item1 ? 2 : 0;

                    Player2Model.Player.HitCount += 1;
                    OnPropertyChanged(nameof(Player2Model.Player.HitCount));

                    if (winAndDestroy.Item2)
                    {
                        OnPropertyChanged(nameof(Player1Model.Player.CarrierCount));
                        OnPropertyChanged(nameof(Player1Model.Player.SubmarineCount));
                        OnPropertyChanged(nameof(Player1Model.Player.DestroyerCount));
                        OnPropertyChanged(nameof(Player1Model.Player.BattleshipCount));
                        OnPropertyChanged(nameof(Player1Model.Player.CruiserCount));
                    }
                }
                else if (status == TileStatus.Empty)
                {
                    Player1Model.Miss(tile.X, tile.Y);
                    CurrentPlayer = 1;
                    CanShoot = false;
                }

                DrawOtherPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
            }
            while (CanShoot);

            NextPlayer(null);
        }
    }
}
