using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Battleships.Model;
using Battleships.Model.Helpers;
using Battleships.ViewModel.Command;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstAIViewModel : PlayPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static PlayAgainstAIViewModel _instance;
        private PlayBoardModel _playerModel;
        private PlayBoardModel _aiModel;

        public PlayBoardModel PlayerModel { get { return _playerModel; } }
        public PlayBoardModel AIModel { get { return _aiModel; } }

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
            _playerModel = new PlayBoardModel();
            _aiModel = new PlayBoardModel();

            DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
            DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
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
                    TileStatus status = AIModel.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Winner = AIModel.Hit(xIndex, yIndex) ? 1 : 0;
                    }
                    else if (status == TileStatus.Empty)
                    {
                        AIModel.Miss(xIndex, yIndex);
                        CurrentPlayer = 2;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                }
                else
                {
                    TileStatus status = PlayerModel.GetTile(xIndex, yIndex).TileStatus;
                    if (status == TileStatus.Ship)
                    {
                        Winner = PlayerModel.Hit(xIndex, yIndex) ? 2 : 0;
                    }
                    else if (status == TileStatus.Empty)
                    {
                        PlayerModel.Miss(xIndex, yIndex);
                        CurrentPlayer = 1;
                        CanShoot = false;
                    }

                    DrawOtherPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                }
            }
        }

        public override void NextPlayer(object parameter)
        {
            if (!CanShoot)
            {
                if (CurrentPlayer == 1)
                {
                    DrawPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                }
                else
                {
                    DrawPlayBoardToCanvas(AIModel, SecondPlayerTileItems);
                    DrawOtherPlayBoardToCanvas(PlayerModel, FirstPlayerTileItems);
                }
                CanShoot = true;
            }
        }
    }
}
