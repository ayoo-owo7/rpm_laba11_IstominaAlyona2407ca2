using Rpm_Lab11.Basic;
using Rpm_Lab11.Models;
using Rpm_Lab11.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rpm_Lab11.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigation;
        private readonly IContactsStore _contactsStore;
        private string _newName = string.Empty;
        private string _newPhone = string.Empty;
        private string _errorMessage = string.Empty;
        private Contact? _selectedContact;

        public ObservableCollection<Contact> Contacts => _contactsStore.Contacts;

        public string NewName { get => _newName; set { _newName = value; OnPropertyChanged(); } }
        public string NewPhone { get => _newPhone; set { _newPhone = value; OnPropertyChanged(); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(); } }
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set { _selectedContact = value; OnPropertyChanged(); CommandManager.InvalidateRequerySuggested(); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditContactCommand { get; }

        public ContactsListViewModel(IDialogService dialogService, INavigationService navigation, IContactsStore contactsStore)
        {
            _dialogService = dialogService;
            _navigation = navigation;
            _contactsStore = contactsStore;

            AddCommand = new RelayCommand(ExecuteAdd);
            DeleteCommand = new RelayCommand(ExecuteDelete, _ => SelectedContact != null);
            // Навигация к экрану редактирования с передачей выбранного контакта
            EditContactCommand = new RelayCommand(_ => _navigation.NavigateTo<ContactEditViewModel>(SelectedContact), _ => SelectedContact != null);
        }

        private void ExecuteAdd(object? _)
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(NewName)) { ErrorMessage = "Имя не может быть пустым."; return; }
            if (!Regex.IsMatch(NewPhone, @"^(\+7|7|8)?\d{10}$")) { ErrorMessage = "Неверный формат телефона."; return; }
            if (Contacts.Any(c => c.Phone.Trim().Equals(NewPhone.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует."); return;
            }
            Contacts.Add(new Contact { Name = NewName.Trim(), Phone = NewPhone.Trim() });
            NewName = string.Empty; NewPhone = string.Empty;
            _dialogService.ShowInfo("Контакт успешно добавлен.");
        }

        private void ExecuteDelete(object? _)
        {
            if (SelectedContact != null && _dialogService.ShowConfirmation($"Удалить '{SelectedContact.Name}'?"))
            {
                Contacts.Remove(SelectedContact);
                SelectedContact = null;
            }
        }
    }
}
