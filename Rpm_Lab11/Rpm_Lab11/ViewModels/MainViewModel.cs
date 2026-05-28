using Rpm_Lab11.Basic;
using Rpm_Lab11.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rpm_Lab11.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public INavigationService NavigationService { get; }
        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigation)
        {
            NavigationService = navigation;
            ShowContactsCommand = new RelayCommand(_ => NavigationService.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(_ => NavigationService.NavigateTo<AboutViewModel>());
            NavigationService.NavigateTo<ContactsListViewModel>();
        }
    }
}
