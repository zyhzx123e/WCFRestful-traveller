using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{
    [DataContract(Namespace = "http://tempuri.org/")]
    public class adminReturn
    {


        [DataMember(IsRequired = true)]
        public List<admin> adminList { get; set; }



        [DataMember(IsRequired = true)]
        public List<hypermedia> hyperList { get; set; }



        public adminReturn() { }

        public adminReturn(List<admin> aL, List<hypermedia> hL)
        {
            this.adminList = aL;
            this.hyperList = hL;
        }


    }
}