using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{

    [DataContract(Namespace = "http://tempuri.org/")]
    public class strReturn
    {

        [DataMember(IsRequired = true)]
        public string str { get; set; }
        [DataMember(IsRequired = true)]
        public List<hypermedia> hyperList{ get; set; }


        public strReturn() { }

        public strReturn(string s, List<hypermedia> hL)
        {
            this.str = s;
            this.hyperList = hL;
        }

    }
}