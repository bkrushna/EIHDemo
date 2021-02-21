using EIHTestPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIHTestPortal.Interfaces
{
    public interface IContactService
    {
        List<Contact> GetAll();

        Contact GetById(string id);
        bool AddContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(Contact contact);
    }
}
