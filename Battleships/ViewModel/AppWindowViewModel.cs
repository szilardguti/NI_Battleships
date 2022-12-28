using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Battleships.ViewModel
{
    public class AppWindowViewModel : INotifyPropertyChanged
    {
        private IPageViewModel currentPageViewModel;
        private List<IPageViewModel> pageViewModels;

        public event PropertyChangedEventHandler PropertyChanged;

        public AppWindowViewModel()
        {
            pageViewModels.Add(new MainPageViewModel());

            currentPageViewModel = pageViewModels[0];
        }

        public IPageViewModel CurrentPageViewModel { get { return currentPageViewModel; } }
        public List<IPageViewModel> PageViewModels { get { return pageViewModels; } }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!pageViewModels.Contains(viewModel))
                pageViewModels.Add(viewModel);

            currentPageViewModel = pageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
