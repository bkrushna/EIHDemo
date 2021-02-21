using EIHTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Interfaces
{
    /// <summary>
    /// Interface providing functions related to user model
    /// </summary>
    public interface IUserService
    {
        UserReponse Authenticate(User model);
        IEnumerable<User> GetAll();
        User GetById(string userId);
    }

    /// <summary>
    /// Interface providing functions related to contact model
    /// </summary>
    public interface IContactService
    {
        IEnumerable<Contact> GetAll();
        Contact GetByEmail(string email);

        Contact GetById(string id);
        IEnumerable<Contact> GetAllContactsByState(int state);

        bool AddContact(Contact contact);

        bool UpdateContact(Contact contact);

        bool DeleteContact(Contact contact);

    }


    /// <summary>
    /// interface providing functions related to log model
    /// </summary>
    public interface ILogService
    {
        IEnumerable<LogModel> GetAll();
        IEnumerable<LogModel> GetAllLogsByLevel(int level);

        bool AddLog(LogModel log);

    }

    public interface ILog
    {
        public void InfoLog(string msg);
        public void WarnLog(string msg);
        public void ErrorLog(string msg);
        public void ExceptionLog(string msg);
    }
        
}
