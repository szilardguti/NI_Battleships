using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Battleships.ViewModel.Navigation
{
    public class NavigationModule
    {
        private static NavigationModule _instance;

        public static NavigationModule Instance
        {
            get
            {
                _instance ??= new NavigationModule();

                return _instance;
            }
        }

        private IPageViewModel _currentPageViewModel;
        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set { _currentPageViewModel = value; }
        }

        private Collection<IPageViewModel> _pageViewModels;

        public Collection<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels; }
            set { _pageViewModels = value; }
        }

        public NavigationModule()
        {
            PageViewModels = new Collection<IPageViewModel>
            {
                new MainPageViewModel()
            };

            CurrentPageViewModel = PageViewModels[0];
        }

        public void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
