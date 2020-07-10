using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDRFA_Client
{
    public class GDRFA_Control_Class
    {
        public static string run()
        {
            //Control_GetPersonDetails("8", "0000", "1", "test123");

            return Control_GetPersonDetailswithPhotoEx("1", "2", "201/2003/7072838", "test123");
        }

        #region GetSearchPerson
        public static string Control(int gender, string dob, string nationality, string passportnumber, string GDRFA_SecurityId)
        {

            string xmlrequest = string.Empty;
            string result = string.Empty;

            try
            {

                Console.WriteLine("Generating XML");
                xmlrequest = GetXML(gender, dob, nationality, passportnumber);
                if (xmlrequest != null)
                    if (xmlrequest.Length > 0)
                    {
                        Console.WriteLine("GetSearchPersons Called");
                        result = GetSearchPersons(xmlrequest, GDRFA_SecurityId);
                    }
                return result;
            }
            catch (Exception ex)
            {
                return "GetSearchPersons Control Exception: " + ex.Message;
            }
        }
        private static string GetXML(int gender, string dateOfBirth, string nationality, string passportNumber)
        {
            StringBuilder a = new StringBuilder();
            string transactionDate = DateTime.Now.ToString("yyyy-MM-dd");
            a.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            a.Append("<Request>");
            a.Append("<Header>");
            a.Append("<FusionInfo>");
            a.Append("<Process ID=\"111\" Name=\"AAA\" />");
            a.Append("<Activity ID=\"222\" Name= \"BBB\"></Activity>");
            a.Append("<TransactionID>191090915000001</TransactionID>");
            a.Append("</FusionInfo>");
            a.Append(" <DepartmentInfo>");
            a.Append("<Sender>");
            a.Append("<Id>191</Id>");
            a.Append("<Description>DHA eClaim</Description>");
            a.Append("</Sender>");
            a.Append("<Receiver>");
            a.Append("<Id>140</Id>");
            a.Append("<Description>DNRD</Description>");
            a.Append("</Receiver>");
            a.Append("<Transaction>");
            a.Append("<Type>1</Type>");
            a.Append("<ProcessType>SynSingle_PR</ProcessType>");
            a.Append("<Date>" + transactionDate + "</Date>");
            a.Append("<ReferenceNumber>2222</ReferenceNumber>");
            a.Append("</Transaction>");
            a.Append("</DepartmentInfo>");
            a.Append("</Header>");
            a.Append("<Body>");
            a.Append("<ClientProcess>");
            a.Append("<Id>EMCS-220</Id>   ");
            a.Append("<Description>Search Persons</Description>");
            a.Append("</ClientProcess>");
            a.Append("<ApplicantInfo>");
            a.Append("<Applicant>");
            a.Append("<PassportNo>" + passportNumber + "</PassportNo>");
            a.Append(" <DateOfBirth>" + dateOfBirth + "</DateOfBirth>");
            a.Append("<Gender>" + gender.ToString() + "</Gender>");
            a.Append("  <Nationality>" + nationality + "</Nationality>      ");
            a.Append("</Applicant>");
            a.Append("</ApplicantInfo>      ");
            a.Append("</Body>");
            a.Append("</Request>");
            return a.ToString();
        }
        private static string GetSearchPersons(string xmlrequest, string securityId)
        {
            string response = string.Empty;

            try
            {
                GDRFA_GetSearchPersons.GetSearchPersonsSoapClient GSP = new GDRFA_GetSearchPersons.GetSearchPersonsSoapClient();

                response = GSP.Execute(securityId, xmlrequest);
                return response;
            }
            catch (Exception ex)
            {
                return "GetSearchPersons Exception: " + ex.Message;
            }
        }
        #endregion

        #region GetPersonDetails
        public static string Control_GetPersonDetails(string SearchType,string SearchNo,string SearchNoFormat, string GDRFA_SecurityId)
        {
            string xmlrequest = string.Empty;
            string result = string.Empty;

            try
            {

                Console.WriteLine("Generating XML");
                xmlrequest = GenerateXML_GetPersonDetails(SearchType, SearchNo, SearchNoFormat);
                if (xmlrequest != null)
                    if (xmlrequest.Length > 0)
                    {
                        Console.WriteLine("GetPersonDetails Called");
                        result = Execute_GetPersonDetails(xmlrequest, GDRFA_SecurityId);
                    }
                return result;
            }
            catch (Exception ex)
            {
                return "GetPersonDetails Control Exception: " + ex.Message;
            }
        }
        private static string GenerateXML_GetPersonDetails(string SearchType, string SearchNo, string SearchNoFormat)
        {
            StringBuilder a = new StringBuilder();

            string transactionDate = DateTime.Now.ToString("yyyy-MM-dd");
            a.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            a.Append("<Request>");
            a.Append("<Header>");
            a.Append("<FusionInfo>");
            a.Append("<Process ID=\"111\" Name=\"AAA\" />");
            a.Append("<Activity ID=\"222\" Name= \"BBB\" />");
            a.Append("<TransactionID>191090915000001</TransactionID>");
            a.Append("</FusionInfo>");
            a.Append("<DepartmentInfo>");
            a.Append("<Sender>");
            a.Append("<Id>191</Id>");
            a.Append("<Description>DHA eClaim Insurance</Description>");
            a.Append("</Sender>");
            a.Append("<Receiver>");
            a.Append("<Id>140</Id>");
            a.Append("<Description>GDRFAD</Description>");
            a.Append("</Receiver>");
            a.Append("<Transaction>");
            a.Append("<Type>1</Type>");
            a.Append("<ProcessType>SynSingle_PR</ProcessType>");
            a.Append("<Date>" + transactionDate + "</Date>");
            a.Append("<ReferenceNumber></ReferenceNumber>");
            a.Append("</Transaction>");
            a.Append("</DepartmentInfo>");
            a.Append("</Header>");
            a.Append("<Body>");
            a.Append("<ClientProcess>");
            a.Append("<Id>EMCS-221</Id>   ");
            a.Append("<Description>Get Person Details</Description>");
            a.Append("</ClientProcess>");
            a.Append("<ApplicantInfo>");
            a.Append("<Applicant>");
            a.Append("<SearchType>" + SearchType + "</SearchType>");
            a.Append("<SearchNo>" + SearchNo + "</SearchNo>");
            a.Append("<SearchNoFormat>" + SearchNoFormat + "</SearchNoFormat>");
            a.Append("</Applicant>");
            a.Append("</ApplicantInfo>      ");
            a.Append("</Body>");
            a.Append("</Request>");

            return a.ToString();
        }
        private static string Execute_GetPersonDetails(string xmlrequest,string securityId)
        {
            string response = string.Empty;
            try
            {
                GetPersonDetails.GetPersonDetailsSoapClient GPD = new GetPersonDetails.GetPersonDetailsSoapClient();
                response = GPD.Execute(securityId, xmlrequest);
                return response;
            }
            catch (Exception ex)
            {
                return "GetPersonDetails Execution Exception: " + ex.Message;
            }
        }
        #endregion

        #region GetPersonDetailswithPhotoEx
        public static string Control_GetPersonDetailswithPhotoEx(string interfacetype, string PersonType, string PersonNumebr, string GDRFA_SecurityId)
        {
            string xmlrequest = string.Empty;
            string result = string.Empty;

            try
            {
                Console.WriteLine("GetPersonwithPhotoEx generating XML");
                xmlrequest = GenerateXML_GetPersonDetailswithPhotoEx(interfacetype, PersonType, PersonNumebr);
                if (xmlrequest != null)
                    if (xmlrequest.Length > 0)
                    {
                        Console.WriteLine("GetPersonwithPhotoEx executing..");
                        result = Execute_GetPersonDetailswithPhotoEx(xmlrequest, GDRFA_SecurityId);
                    }
                return result;
            }
            catch (Exception ex)
            {
                return "GetPersonwithPhotoEx Control Exception: " + ex.Message;
            }
        }
        private static string Execute_GetPersonDetailswithPhotoEx(string xmlrequest, string securityId)
        {
            string response = string.Empty;
            try
            {
                GetPersonInfoWithPhotoEx.GetPersonInfoWithPhotoExSoapClient GPIWPE = new GetPersonInfoWithPhotoEx.GetPersonInfoWithPhotoExSoapClient();
                response = GPIWPE.Execute(securityId, xmlrequest);
                return response;
            }
            catch (Exception ex)
            {
                return "GetPersonInfoWithPhotoEx Execution Exception: " + ex.Message;
            }
        }
        private static string GenerateXML_GetPersonDetailswithPhotoEx(string interfacetype, string PersonType, string PersonNumebr)
        {
            StringBuilder a = new StringBuilder();

            string transactionDate = DateTime.Now.ToString("yyyy-MM-dd");
            a.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            a.Append("<Request>");
            a.Append("<Header>");
            a.Append("<FusionInfo>");

            a.Append("<Process ID=\"111\" Name=\"AAA\" />");
            a.Append("<Activity ID=\"222\" Name= \"BBB\"></Activity>");

            a.Append("<TransactionID>191140119123456</TransactionID>");
            a.Append("</FusionInfo>");
            a.Append("<DepartmentInfo>");
            a.Append("<Sender>");
            a.Append("<Id>191</Id>");
            a.Append("<Description>DHA eClaim Insurance</Description>");
            a.Append("</Sender>");
            a.Append("<Receiver>");
            a.Append("<Id>140</Id>");

            a.Append("<Description>DNRD</Description>");
            //a.Append("<Description>GDRFAD</Description>");

            a.Append("</Receiver>");
            a.Append("<Transaction>");
            a.Append("<Type>1</Type>");

            a.Append("<ProcessType>SynSingle_PR</ProcessType>");
            a.Append("<Date>" + transactionDate + "</Date>");
            a.Append("<ReferenceNumber>2222</ReferenceNumber>");

            a.Append("</Transaction>");
            a.Append("</DepartmentInfo>");
            a.Append("</Header>");
            a.Append("<Body>");
            a.Append("<ClientProcess>");

            a.Append("<Id>EMCS-016</Id>");
            a.Append("<Description>Applicant Profile With Photo Request</Description>");

            a.Append("</ClientProcess>");
            a.Append("<ApplicantInfo>");
            a.Append("<Applicant>");

            a.Append("<InterfaceType>" + interfacetype + "</InterfaceType>");
            a.Append("<PersonType>" + PersonType + "</PersonType>");
            a.Append("<PersonNumber>" + PersonNumebr + "</PersonNumber>");
            a.Append("<EstablishmentNb></EstablishmentNb>");
            a.Append("<IncludePhoto>" + 0 + "</IncludePhoto>");

            a.Append("</Applicant>");
            a.Append("</ApplicantInfo>      ");
            a.Append("</Body>");
            a.Append("</Request>");

            return a.ToString();
        }
        #endregion 

        
    }
}