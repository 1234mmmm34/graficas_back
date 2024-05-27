using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UserManagement;

namespace CardManagement
{
    public partial class AddDevice
    {
        public static DeviceInfo struDeviceInfo;

        public static string ActionISAPI(string szUrl, string szRequest, string szMethod)
        {
            string szResponse = string.Empty;

            if (!szUrl.Substring(0, 4).Equals("http"))
            {
                szUrl = "http://" + AddDevice.struDeviceInfo.strDeviceIP + ":" + AddDevice.struDeviceInfo.strHttpPort + szUrl;
            }
            HttpClient clHttpClient = new HttpClient();
            byte[] byResponse = { 0 };
            int iRet = 0;
            string szContentType = string.Empty;

            switch (szMethod)
            {
                case "GET":
                    iRet = clHttpClient.HttpRequest(AddDevice.struDeviceInfo.strUsername, AddDevice.struDeviceInfo.strPassword, szUrl, szMethod, ref byResponse, ref szContentType);
                    break;
                case "PUT":
                    iRet = clHttpClient.HttpPut(AddDevice.struDeviceInfo.strUsername, AddDevice.struDeviceInfo.strPassword, szUrl, szMethod, szRequest, ref szResponse);
                    break;
                case "POST":
                    iRet = clHttpClient.HttpPut(AddDevice.struDeviceInfo.strUsername, AddDevice.struDeviceInfo.strPassword, szUrl, szMethod, szRequest, ref szResponse);
                    break;
                default:
                    break;
            }

            if (iRet == (int)CardManagement.HttpClient.HttpStatus.Http200)
            {
                if ((!szMethod.Equals("GET")) || (szContentType.IndexOf("application/xml") != -1))
                {
                    if (szResponse != string.Empty)
                    {
                        return szResponse;
                    }

                    if (szMethod.Equals("GET"))
                    {
                        szResponse = Encoding.Default.GetString(byResponse);
                        return szResponse;
                    }
                }
                else
                {
                    if (byResponse.Length != 0)
                    {
                        szResponse = Encoding.Default.GetString(byResponse);
                        return szResponse;
                    }
                }
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpOther)
            {
                string szCode = string.Empty;
                string szError = string.Empty;
                clHttpClient.ParserResponseStatus(szResponse, ref szCode, ref szError);
                Console.WriteLine("Pedido fallido, error:" + szCode + " Describe:" + szError + "\r\n");
                return string.Empty;
            }
            else if (iRet == (int)HttpClient.HttpStatus.HttpTimeOut)
            {
                Console.WriteLine(szMethod + " " + szUrl + "error! tiempo agotado");
                return string.Empty;
            }
            return szResponse;
        }

        //Add Hikvision device
        public static bool Login(string user, string password, string port, string ip)
        {
            //    string textBoxUserName = "admin", textBoxPassword = "Repara123",
            //    textBoxDeviceAddress = "187.216.118.73";
            //    string textBoxPort = "5551";

            struDeviceInfo = new DeviceInfo();
            struDeviceInfo.strUsername = user;
            struDeviceInfo.strPassword = password;
            struDeviceInfo.strDeviceIP = ip;
            struDeviceInfo.strHttpPort = port;

            bool num = false;
            if (Security.Login(struDeviceInfo))
            {
                // user check success
                num = true;
                struDeviceInfo.bIsLogin = true;
                Console.WriteLine("Hecho");
            }

            return num;

        }

        public static bool InsertUser(string id_usuario, string nombre, string fechaActual, string fechaProximoPago)
        {
            string szUrl = "/ISAPI/AccessControl/UserInfo/SetUp?format=json";
            string szRequest = "{\"UserInfo\":{\"employeeNo\":\"" + id_usuario +
            "\",\"name\":\"" + nombre +
            "\",\"userType\":\"normal\",\"Valid\":{\"enable\":true,\"beginTime\":\""+fechaActual+"\",\"endTime\":\""+fechaProximoPago+"\"},\"doorRight\": \"1\",\"RightPlan\":[{\"doorNo\":1,\"planTemplateNo\":\"1\"}]}}";
            string szMethod = "PUT";

            string szResponse = ActionISAPI(szUrl, szRequest, szMethod);
            bool res = false;
            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (1 == rs.statusCode)
                {
                    Console.WriteLine("Set UserInfo Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.errorMsg);
                    res = false;
                }
            }
            return res;
        }

        public static bool InsertCardUser(string id_usuario, string codigo_acceso)
        {
            // var token = Guid.NewGuid().ToString();
            //    token.Substring(0, 3);
           // var randomNumber = new Random().Next(0, 1000);
        //    string token = randomNumber.ToString();

            string szUrl = "/ISAPI/AccessControl/CardInfo/SetUp?format=json";
            string szResponse = string.Empty;
            string szRequest = "{\"CardInfo\":{\"employeeNo\":\"" + id_usuario +
                "\",\"cardNo\":\"" + codigo_acceso +
                "\",\"cardType\":\"normalCard\"}}";
            string szMethod = "PUT";

            bool res = false;
            szResponse = ActionISAPI(szUrl, szRequest, szMethod);
            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (1 == rs.statusCode)
                {
                    Console.WriteLine("Add Card Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.errorMsg);
                    res = false;
                }
            }

            return res;
        }

        public static bool DeleteCardUser(string id_usuario)
        {
            string szUrl = "/ISAPI/AccessControl/CardInfo/Delete?format=json";
            string szResponse = string.Empty;
            string szRequest = "{\"CardInfoDelCond\":{\"CardNoList\":[{\"cardNo\":\"" + id_usuario + "\"}]}}";
            string szMethod = "PUT";

            szResponse = ActionISAPI(szUrl, szRequest, szMethod);

            bool res = false;

            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (rs.statusString.Equals("OK"))
                {
                    Console.WriteLine("Delete Card Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.statusString);
                }
            }
            return res;
        }


        public static bool DeleteAllUser(string id_usuario)
        {
            string szUrl = "/ISAPI/AccessControl/UserInfoDetail/Delete?format=json";
            string szResponse = string.Empty;
            string szRequest = "{\"UserInfoDetail\":{\"mode\":\"byEmployeeNo\",\"EmployeeNoList\":[{\"employeeNo\":\"" + id_usuario + "\"}]}}";
            string szMethod = "PUT";

            szResponse = ActionISAPI(szUrl, szRequest, szMethod);

            bool res = false;

            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (rs.statusString.Equals("OK"))
                {
                    Console.WriteLine("Delete Card Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.statusString);
                }
            }
            return res;
        }


        public static bool RestrictedUser(string id_usuario)
        {
            string szUrl = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
            string szResponse = string.Empty;
            string szRequest = "{\"UserInfo\":{\"employeeNo\":\"" + id_usuario + "\",\"Valid\":{\"enable\":false}}}";
            string szMethod = "PUT";

            szResponse = ActionISAPI(szUrl, szRequest, szMethod);

            bool res = false;

            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (rs.statusString.Equals("OK"))
                {
                    Console.WriteLine("Delete Card Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.statusString);
                }
            }
            return res;
        }


        public static bool EnableUser(string id_usuario)
        {
            string szUrl = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
            string szResponse = string.Empty;
            string szRequest = "{\"UserInfo\":{\"employeeNo\":\"" + id_usuario + "\",\"Valid\":{\"enable\":true}}}";
            string szMethod = "PUT";

            szResponse = ActionISAPI(szUrl, szRequest, szMethod);

            bool res = false;

            if (szResponse != string.Empty)
            {
                ResponseStatus rs = JsonConvert.DeserializeObject<ResponseStatus>(szResponse);
                if (rs.statusString.Equals("OK"))
                {
                    Console.WriteLine("Delete Card Succ!");
                    res = true;
                }
                else
                {
                    Console.WriteLine(rs.statusString);
                }
            }
            return res;
        }
    }
}


