using EIHTestPortal.Helpers;
using EIHTestPortal.Interfaces;
using EIHTestPortal.Models;
using EIHTestPortal.WSGatewayHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace EIHTestPortal.Services
{

    /// <summary>
    /// This class is immplentation of IContactService interface, 
    /// this implementor will fetch data from external API
    /// </summary>
    public class ContactService : IContactService
    {
        WSClient clientObj;

        public ContactService()
        {
            WSClient.BaseAddress= ConfigurationManager.AppSettings["baseaddress"];
            clientObj = new WSClient();
            
        }
        public bool AddContact(Contact contact)
        {
            var erroModel= clientObj.AddContact(contact);
            if (erroModel.Code == HttpStatusCode.OK)
                return true;
            return false;
        }

        public bool DeleteContact(Contact contact)
        {
            var erroModel = clientObj.DeleteContact(contact);
            if (erroModel.Code == HttpStatusCode.OK)
                return true;
            return false;
        }

        public List<Contact> GetAll()
        {
            var json_str =clientObj.GetAllContacts();
            Parser obj = new Parser();
            return obj.ConvertToContactList(json_str);
        }

        public Contact GetById(string id)
        {
            var json_str = clientObj.GetContactById(id);
            Parser obj = new Parser();
            return obj.ConvertToContact(json_str);
        }

        public bool UpdateContact(Contact contact)
        {
            var erroModel= clientObj.UpdateContact(contact);
            if (erroModel.Code == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}