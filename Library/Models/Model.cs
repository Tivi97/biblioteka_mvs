//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web.SessionState;
//using Oracle.ManagedDataAccess.Client;


//namespace Library.Models
//{
//            public abstract class Model
//        {
//            protected readonly OracleConnection Connection;
//            protected readonly HttpSessionState Session;

//            protected Model()
//            {
//                Connection = new OracleConnection { ConnectionString = "DATA SOURCE=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));USER ID=CLUB;PASSWORD=club;" };
//                Session = HttpContext.Current.Session;
//            }

//            protected static string GetMd5Hash(MD5 md5Hash, string input)
//            {
//                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
//                var sBuilder = new StringBuilder();
//                foreach (var t in data)
//                {
//                    sBuilder.Append(t.ToString("x2"));
//                }
//                return sBuilder.ToString();
//            }
//        }
//    }
