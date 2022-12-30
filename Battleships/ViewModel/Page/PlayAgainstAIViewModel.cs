﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Battleships.Model;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;
        public static PlayPageViewModel Instance
        {
            get
            {
                _instance ??= new PlayAgainstAIViewModel();

                return _instance;
            }
        }
        public PlayAgainstAIViewModel() : base()
        {
            Player2Model.Player.Name = "Robot kapitány";
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
                        Winner = Player2Model.Hit(xIndex, yIndex) ? 1 : 0;

                        OnPropertyChanged(nameof(Player1Model.Player.HitCount));

                        OnPropertyChanged(nameof(Player2Model.Player.CarrierCount));
                        OnPropertyChanged(nameof(Player2Model.Player.BattleshipCount));
                        OnPropertyChanged(nameof(Player2Model.Player.SubmarineCount));
                        OnPropertyChanged(nameof(Player2Model.Player.CruiserCount));
                        OnPropertyChanged(nameof(Player2Model.Player.DestroyerCount));
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
                        Winner = Player1Model.Hit(xIndex, yIndex) ? 2 : 0;

                        OnPropertyChanged(nameof(Player2Model.Player.HitCount));

                        OnPropertyChanged(nameof(Player1Model.Player.CarrierCount));
                        OnPropertyChanged(nameof(Player1Model.Player.BattleshipCount));
                        OnPropertyChanged(nameof(Player1Model.Player.SubmarineCount));
                        OnPropertyChanged(nameof(Player1Model.Player.CruiserCount));
                        OnPropertyChanged(nameof(Player1Model.Player.DestroyerCount));
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

        public override void NextPlayer(object parameter)
        {
            if (!CanShoot)
            {
                if (CurrentPlayer == 1)
                {
                    DrawPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                }
                else
                {
                    DrawPlayBoardToCanvas(Player2Model, SecondPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(Player1Model, FirstPlayerTileItems);
                }
                CanShoot = true;
            }
        }
    }
}
