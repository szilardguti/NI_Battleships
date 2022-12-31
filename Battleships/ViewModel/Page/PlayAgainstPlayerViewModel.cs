using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Battleships.Model;

namespace Battleships.ViewModel.Page
{
    public class PlayAgainstPlayerViewModel : PlayPageViewModel
    {
        public PlayAgainstPlayerViewModel()
        {
            Player1Model.Player.Name = string.Empty;
            Player2Model.Player.Name = string.Empty;
        }
        public override void RobotPlay()
        {
            throw new NotImplementedException();
        }
    }
}
