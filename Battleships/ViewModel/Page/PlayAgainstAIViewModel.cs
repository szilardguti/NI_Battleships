using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Battleships.Model;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PlayAgainstAIViewModel() : base()
        {
            Player1Model.Player.Name = string.Empty;
            Player2Model.Player.Name = "Robot kapitány";

            Player2Ready = true;
        }

        public override void ExecuteTileClick(object parameter)
        {
            var mouseArguments = parameter as MouseButtonEventArgs;
            var point = mouseArguments.GetPosition((IInputElement)mouseArguments.Source);

            int xIndex = (int)point.X / 30;
            int yIndex = (int)point.Y / 30;

            if (CanShoot && Winner == 0)
            {
                if (CurrentPlayer == 1)
                {
                    TileStatus status = Player2Model.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Tuple<bool, bool> winAndDestroy = Player2Model.Hit(xIndex, yIndex);
                        Winner = winAndDestroy.Item1 ? 1 : 0;

                        Player1Model.Player.HitCount += 1;
                        OnPropertyChanged(nameof(Player1Model.Player.HitCount));

                        if (winAndDestroy.Item2)
                        {
                            OnPropertyChanged(nameof(Player2Model.Player.CarrierCount));
                            OnPropertyChanged(nameof(Player2Model.Player.SubmarineCount));
                            OnPropertyChanged(nameof(Player2Model.Player.DestroyerCount));
                            OnPropertyChanged(nameof(Player2Model.Player.BattleshipCount));
                            OnPropertyChanged(nameof(Player2Model.Player.CruiserCount));
                        }
                    }
                    else if (status == TileStatus.Empty)
                    {
                        Player2Model.Miss(xIndex, yIndex);
                        CurrentPlayer = 2;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                }
                else
                {
                    TileStatus status = Player1Model.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Tuple<bool, bool> winAndDestroy = Player1Model.Hit(xIndex, yIndex);
                        Winner = winAndDestroy.Item1 ? 1 : 0;

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
                        Player1Model.Miss(xIndex, yIndex);
                        CurrentPlayer = 1;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
                }
            }
        }
    }
}
