using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Helpers
{
    /// <summary>
    /// Class implementing functionality for CRUD operations of contact
    /// </summary>
    public class ContactService : IContactService
    {

        private readonly AppSettings _appSettings;
        private readonly IMongoContext _mongoContext;
        private readonly ILog _log;

        public ContactService(IOptions<AppSettings> appSettings,IMongoContext mongoContext, ILog log)
        {
            _appSettings = appSettings.Value;
            _mongoContext = mongoContext;
            _log = log;
        }

        public bool AddContact(Contact contact)
        {
            //check if contact is already present or  not
            var is_present=_mongoContext.Contact.Find(x => x.Email == contact.Email).SingleOrDefault();
            _log.InfoLog("Adding contact");
            if (is_present == null)
            {
                _mongoContext.Contact.InsertOne(contact);
                return true;
            }
            return false;
        }

        public bool DeleteContact(Contact contact)
        {
            _log.InfoLog("Deleting contact");
            try
            {
                var res = _mongoContext.Contact.DeleteOne<Contact>(c=>c.Id==contact.Id);
                if(res.IsAcknowledged)
                {
                    if (res.DeletedCount <= 0)
                        return false;
                }
            }
            catch(Exception ex)
            {
                _log.ExceptionLog("Exception caught in DeleteContact" + ex.ToString());
                return false;
            }
            return true;
        }

        public  IEnumerable<Contact> GetAll()
        {
            var lst = _mongoContext.Contact.Find(_ => true).ToList<Contact>();
            return lst;
        }

        public IEnumerable<Contact> GetAllContactsByState(int state)
        {
            var lst = _mongoContext.Contact.Find(x=>x.Status==state).ToList<Contact>();
            return lst;
        }

        public Contact GetByEmail(string email)
        {
            var contactObj = _mongoContext.Contact.Find(x => x.Email == email).SingleOrDefault<Contact>();
            return contactObj;
        }

        public Contact GetById(string id)
        {
            var contactObj = _mongoContext.Contact.Find(x => x.Id == id).SingleOrDefault<Contact>();
            return contactObj;
        }

        public bool UpdateContact(Contact contact)
        {
            _log.InfoLog("Updating contact");
            try
            {
                var res = _mongoContext.Contact.FindOneAndReplace<Contact>(c => c.Id.Equals(contact.Id), contact);

                if (res == null)
                    return false;

            }
            catch(Exception ex)
            {
                _log.ExceptionLog("Exception caught in updatecontact"+ex.ToString());
                return false;
            }
            return true;
        }
    }
}
