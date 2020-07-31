using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net;

namespace BmbWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BmbService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BmbService.svc or BmbService.svc.cs at the Solution Explorer and start debugging.
    public class BmbService : IBmbService
    {

       private SqlConnection UsersConn = new SqlConnection(ConfigurationManager.ConnectionStrings["UsersConn"].ConnectionString); 
        

       public bool UserFound(string UserName, string Password)
        { 
            using (UsersConn)
            {
                var command = new SqlCommand("select UserName, Password from Users where UserName = '" + UserName.Replace("'", "''") + "' and Password = '" + Password.Replace("'", "''") + "'", UsersConn);
                UsersConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return true;
                    else
                        return false;
                }
            }
        }


        public List<User> AllUsers()
        {

             var list = new List<User>();

            using (UsersConn)
            {
                var command = new SqlCommand("select UserName, Password, EmailAddress from Users", UsersConn);
                UsersConn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();
                        user.Username = reader["UserName"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.EmailAddress = reader["EmailAddress"].ToString();
                        list.Add(user);
                    }
                    
                }
            }

            return list;
        }



        //public void InsertUser(string UserName, string Password)
        //{
        //    using (UsersConn)
        //    {
        //       var command = new SqlCommand("insert into Users values('" + UserName.Replace("'","''") + "' , '" + Password.Replace("'","''") + "')" , UsersConn);
        //       UsersConn.Open();
        //        command.ExecuteNonQuery();
        //    }  
        //}

        public void InsertUser(User user)
        {
            using (UsersConn)
            {
                UsersConn.Open();
                var command1 = new SqlCommand("select UserName from Users where UserName = '" + user.Username .Replace("'","''") + "'",UsersConn);
                if(command1.ExecuteScalar() != null)
                {
                    CustomError myError1 = new CustomError()
                    {
                        Message = "Username already taken. Please choose another username.",
                        ErrorCode = 1,
                    };
                    throw new WebFaultException<CustomError>( myError1, HttpStatusCode.Forbidden);
                } else
                {
                    var command = new SqlCommand("insert into Users values('" + user.Username.Replace("'", "''") + "' , '" + user.Password.Replace("'", "''") + "' , '" + user.EmailAddress.Replace("'","''") +  "')", UsersConn);
                    command.ExecuteNonQuery();
                }             
            }
        }



        public void InsertManyUsers(List<User> users)
        {
            using (UsersConn)
            {
                UsersConn.Open();
                foreach (User user in users) { 
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "insert into Users values('" + user.Username.Replace("'", "''") + "' , '" + user.Password.Replace("'", "''") + "' , '" + user.EmailAddress.Replace("'", "''") + "')";
                        command.Connection = UsersConn;
                        command.ExecuteNonQuery();
                    } 
                }
            }
        }


    }
}
