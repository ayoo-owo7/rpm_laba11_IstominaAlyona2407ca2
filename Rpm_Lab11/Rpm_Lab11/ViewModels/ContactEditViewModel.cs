using Rpm_Lab11.Basic;
using Rpm_Lab11.Models;
using Rpm_Lab11.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rpm_Lab11.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private Contact _contact = null!;

        public string EditName
        {
            get => _contact.Name;
            set { _contact.Name = value; OnPropertyChanged(); }
        }
        public string EditPhone
        {
            get => _contact.Phone;
            set { _contact.Phone = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            SaveCommand = new RelayCommand(_ => _navigation.NavigateTo<ContactsListViewModel>());
            CancelCommand = new RelayCommand(_ => _navigation.NavigateTo<ContactsListViewModel>());
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c) _contact = c;
        }
    }
}
