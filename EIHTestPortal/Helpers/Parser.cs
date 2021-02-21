using EIHTestPortal.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EIHTestPortal.Helpers
{
    public class Parser
    {
        public List<Contact> ConvertToContactList(string contactlistjsonstr)
        {
            if (string.IsNullOrEmpty(contactlistjsonstr))
                return null;

            List<Contact> contacts = new List<Contact>();
            var array = BsonSerializer.Deserialize<BsonArray>(contactlistjsonstr);

            foreach (var elem in array)
            {
                Contact add = new Contact();

                var doc = elem.ToBsonDocument();

                if (doc.Names.Contains("id"))
                {
                    add.Id = doc["id"].IsBsonNull ? "" : doc["id"].ToString();
                }

                if (doc.Names.Contains("firstName"))
                {
                    add.FirstName = doc["firstName"].IsBsonNull ? "" : doc["firstName"].ToString();
                }

                if (doc.Names.Contains("lastName"))
                {
                    add.LastName = doc["lastName"].IsBsonNull ? "" : doc["lastName"].ToString();
                }



                if (doc.Names.Contains("email"))
                {
                    add.Email = doc["email"].IsBsonNull ? "" : doc["email"].ToString();
                }
                if (doc.Names.Contains("phoneNumber"))
                {
                    add.PhoneNumber = doc["phoneNumber"].IsBsonNull ? "" : doc["phoneNumber"].ToString();
                }

                if (doc.Names.Contains("status"))
                {
                    add.Status = doc["status"].ToInt32();
                }
                contacts.Add(add);
            }
            return contacts;
        }

        public Contact ConvertToContact(string contactlistjsonstr)
        {
            if (string.IsNullOrEmpty(contactlistjsonstr))
                return null;

            var elem = BsonSerializer.Deserialize<BsonDocument>(contactlistjsonstr);
            {
                var doc = elem.ToBsonDocument();
                Contact add = new Contact();

                if (doc.Names.Contains("id"))
                {
                    add.Id = doc["id"].IsBsonNull ? "" : doc["id"].ToString();
                }

                if (doc.Names.Contains("firstName"))
                {
                    add.FirstName = doc["firstName"].IsBsonNull ? "" : doc["firstName"].ToString();
                }

                if (doc.Names.Contains("lastName"))
                {
                    add.LastName = doc["lastName"].IsBsonNull ? "" : doc["lastName"].ToString();
                }

                

                if (doc.Names.Contains("email"))
                {
                    add.Email = doc["email"].IsBsonNull ? "" : doc["email"].ToString();
                }
                if (doc.Names.Contains("phoneNumber"))
                {
                    add.PhoneNumber = doc["phoneNumber"].IsBsonNull ? "" : doc["phoneNumber"].ToString();
                }

                if (doc.Names.Contains("status"))
                {
                    add.Status = doc["status"].ToInt32();
                }
                return add;
            }
        }
    }
}