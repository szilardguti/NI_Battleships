using System;
using System.Collections.ObjectModel;
using System.Linq;
using Battleships.ViewModel.Page;

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

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private Collection<ViewModelBase> _pageViewModels;

        public Collection<ViewModelBase> PageViewModels
        {
            get { return _pageViewModels; }
            set { _pageViewModels = value; }
        }

        public NavigationModule()
        {
            PageViewModels = new Collection<ViewModelBase>
            {
                new MainPageViewModel()
            };

            CurrentViewModel = PageViewModels[0];
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public void ChangeViewModel(ViewModelBase viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
