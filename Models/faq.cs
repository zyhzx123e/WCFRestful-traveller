using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class faq
    {
        [DataMember(IsRequired = true)]
        public string faq_id { get; set; }


        [DataMember(IsRequired = true)]
        public string faq_q { get; set; }

        [DataMember(IsRequired = true)]
        public string faq_a { get; set; }

        public faq() { }

        public faq(string faqid,string faqq,string faqa) {
            this.faq_id = faqid;
            this.faq_q = faqq;
            this.faq_a = faqa;
        }



        public static string conStr = dbconnection.conStr;
        public List<faq> SelectAllFaqs()
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("select * from traveller_faq", sqlcon);

                sqlcon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                List<faq> lstbal = new List<faq>();
                while (dr.Read())
                {
                    faq bal = new faq
                    {
                        faq_id = dr["faq_id"].ToString(),
                        faq_q = dr["faq_q"].ToString(),
                        faq_a = dr["faq_a"].ToString()
                    };

                    lstbal.Add(bal);
                }

                return lstbal;
            }
        }



        public List<faq> UnauthorizedFaq()
        {


            List<faq> lstbal = new List<faq>();

            faq f = new faq("You are not authorized to access the faq_id data!",
                "You are not authorized to access the faq_q data!",
                "You are not authorized to access the faq_a data!");
            lstbal.Add(f);

            return lstbal;

        }





    }
}