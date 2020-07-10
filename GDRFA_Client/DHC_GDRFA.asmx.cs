using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace GDRFA_Client
{
    /// <summary>
    /// Summary description for DHC_GDRFA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DHC_GDRFA : System.Web.Services.WebService
    {

        //[WebMethod]
        //public string Maindu()
        //{
        //    return GDRFA_Control_Class.run();
        //}

        [WebMethod]
        public string GetSearchPerson(int gender, string dob, string nationality, string passportnumber)
        {
            return GDRFA_Control_Class.Control(gender, dob, nationality, passportnumber, System.Configuration.ConfigurationManager.AppSettings["GDRFA_SecurityId"]);
        }

        [WebMethod]
        public string GetPersonDetails(string SearchType, string SearchNo, string SearchNoFormat)
        {
            return GDRFA_Control_Class.Control_GetPersonDetails(SearchType, SearchNo, SearchNoFormat, System.Configuration.ConfigurationManager.AppSettings["GDRFA_SecurityId"]);
        }

        [WebMethod]
        public string GetPersonDetailsWithPhotoEx(string interfacetype, string PersonType, string PersonNumber)
        {
            return GDRFA_Control_Class.Control_GetPersonDetailswithPhotoEx(interfacetype, PersonType, PersonNumber, System.Configuration.ConfigurationManager.AppSettings["GDRFA_SecurityId"]);
        }
    }
}
