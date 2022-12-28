using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Battleships.ViewModel
{
    public class AppWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IPageViewModel _currentPageViewModel;
        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set { _currentPageViewModel = value; }
        }

        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels; }
            set { _pageViewModels = value; }
        }

        public AppWindowViewModel()
        {
            PageViewModels = new List<IPageViewModel>();

            PageViewModels.Add(new MainPageViewModel());

            CurrentPageViewModel = PageViewModels[0];
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
