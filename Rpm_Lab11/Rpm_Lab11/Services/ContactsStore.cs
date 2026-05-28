using Rpm_Lab11.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpm_Lab11.Services
{
    public interface IContactsStore
    {
        ObservableCollection<Contact> Contacts { get; }
    }

    public class ContactsStore : IContactsStore
    {
        public ObservableCollection<Contact> Contacts { get; } = new();
    }
}
