using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace wcfapi.Models
{

    public class dbconnection
    {


        public static string conStr = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        public dbconnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }


    }
}