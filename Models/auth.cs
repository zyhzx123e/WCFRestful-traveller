using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wcfapi.Models
{
    public class auth
    {
        public string auth_user_type { get; set; }
        public string auth_key { get; set; }

        public static string conStr = dbconnection.conStr;

        public auth(string auth_user_type, string auth_key)
        {
            this.auth_user_type = auth_user_type;
            this.auth_key = auth_key;
        }

        public auth() {

        }

        public int addKey()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("insert into traveller_auth(auth_user_type,auth_key) values('" + auth_user_type + "','" + auth_key + "');", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }

        public int selectKeys(string key)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("select * from traveller_auth where auth_key='" + key + "'", sqlcon);
                int count;
                sqlcon.Open();
                SqlDataReader dr = sqlcmd.ExecuteReader();
                if (dr.Read())
                {
                    count = 1;
                    
                }
                else {
                    count = 0;
                }
                sqlcon.Close();
                return count;

            }
        }





        public int deleteKey(string key)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("delete from traveller_auth where auth_key='" + auth_key + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;

            }
        }


        public int updateKey()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand sqlcmd = new SqlCommand("update traveller_auth set auth_user_type='" + auth_user_type + "' where auth_key='" + auth_key + "'", sqlcon);

                sqlcon.Open();
                int count = sqlcmd.ExecuteNonQuery();
                sqlcon.Close();
                return count;


            }
        }


    }
}