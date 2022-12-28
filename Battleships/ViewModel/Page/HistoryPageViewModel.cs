using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Battleships.ViewModel.Page
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static HistoryPageViewModel _instance;

        public static ViewModelBase Instance
        {
            get
            {
                _instance ??= new HistoryPageViewModel();

                return _instance;
            }
        }

        private Collection<object> _listOfResults;

        public Collection<object> ListOfResults
        {
            get
            {
                _listOfResults ??= new Collection<object>();

                return _listOfResults;
            }

            set
            {
                _listOfResults = value;
            }
        }

        private void LoadData()
        {
            // Load in list of database entities
            // TODO
            Collection<object> collection = new Collection<object>();

            ListOfResults = collection;
        }
    }
}
