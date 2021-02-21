using EIHTestPortal.Helpers;
using EIHTestPortal.Models;
using EIHTestPortal.WSGatewayHelper.APIModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace EIHTestPortal.WSGatewayHelper
{
    public class WSClient
    {
        public static string BaseAddress;

        Logger _logger = new Logger();

        public string GetToken()
        {
            try
            {
                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var user = new User() { UserId = "test", Password = "test" };

                    string myContent = new JavaScriptSerializer().Serialize(user);


                    var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                    var task = Task.Run(async () => await tclient.PostAsync("api/user/authenticate", stringContent));

                    var resp = task.Result;

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        var token = resp.Content.ReadAsStringAsync().Result;
                        dynamic dynObj = JsonConvert.DeserializeObject(token);
                        var token_str = dynObj["token"];
                        return token_str;
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return "";
        }


        //Write functions to make a call to api gateway

        public string GetAllContacts()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
                return "";
            try
            {
                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    {
                        tclient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                        {

                            var task1 = Task.Run(async () => await tclient.GetAsync("api/contacts/contacts"));

                            var resp1 = task1.Result;

                            if (resp1.StatusCode == HttpStatusCode.OK)
                            {
                                var result = resp1.Content.ReadAsStringAsync().Result;

                                return result;

                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return "";
        }


        public string GetContactById(string id)
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
                return "";
            try
            {
                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    {
                        tclient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                        {

                            var task1 = Task.Run(async () => await tclient.GetAsync("api/contacts/contactsById?id=" + id));

                            var resp1 = task1.Result;

                            if (resp1.StatusCode == HttpStatusCode.OK)
                            {
                                var result = resp1.Content.ReadAsStringAsync().Result;

                                return result;

                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return "";
        }

        public ErrorModel AddContact(Contact contact)
        {
            ErrorModel resultObj = new ErrorModel();
            
            var token = GetToken();

            if (string.IsNullOrEmpty(token))
            {
                resultObj.Code = HttpStatusCode.InternalServerError;
                return resultObj;
            }

            try
            {
                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    {
                        tclient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                        {

                            string myContent = new JavaScriptSerializer().Serialize(contact);


                            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                            var task1 = Task.Run(async () => await tclient.PostAsync("api/contacts/Add", stringContent));

                            var resp1 = task1.Result;
                            resultObj.Code = resp1.StatusCode;
                            if (resp1.StatusCode == HttpStatusCode.OK)
                            {
                                var result = resp1.Content.ReadAsStringAsync().Result;
                                resultObj.ErrorMessage = result;

                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return resultObj;
        }

        public ErrorModel UpdateContact(Contact contact)
        {
            ErrorModel resultObj = new ErrorModel();

            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogException("In Updatecontact,token is empty");
                resultObj.Code = HttpStatusCode.InternalServerError;
                return resultObj;
            }
            try
            {
                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    {
                        tclient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                        {

                            string myContent = new JavaScriptSerializer().Serialize(contact);


                            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");

                            var task1 = Task.Run(async () => await tclient.PostAsync("api/contacts/Update", stringContent));

                            var resp1 = task1.Result;

                            resultObj.Code = resp1.StatusCode;

                            if (resp1.StatusCode == HttpStatusCode.OK)
                            {
                                var result = resp1.Content.ReadAsStringAsync().Result;
                                resultObj.ErrorMessage = result;
                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return resultObj;
        }

        public ErrorModel DeleteContact(Contact contact)
        {
            ErrorModel resultObj = new ErrorModel();

            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogException("In DeleteContact,token is empty");
                resultObj.Code = HttpStatusCode.InternalServerError;
                return resultObj;

            }
            try
            {

                using (var tclient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    tclient.BaseAddress = new Uri(BaseAddress);

                    tclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    {
                        tclient.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                        {

                            string myContent = new JavaScriptSerializer().Serialize(contact);


                            var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");


                            var new_req = new HttpRequestMessage
                            {
                                Method = HttpMethod.Post,
                                RequestUri = new Uri(BaseAddress + "api/contacts/Delete"),
                                Content = stringContent
                            };

                            var resp1 = tclient.SendAsync(new_req).Result;

                            resultObj.Code = resp1.StatusCode;

                            if (resp1.StatusCode == HttpStatusCode.OK)
                            {
                                var result = resp1.Content.ReadAsStringAsync().Result;
                                resultObj.ErrorMessage = result;

                            }

                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogException(e.ToString());
            }
            return resultObj;
        }
    }
}