using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wcfapi.Models
{

    [DataContract(Namespace = "http://tempuri.org/")]
    public class userReturn
    {


        [DataMember(IsRequired = true)]
        public List<User> uList { get; set; }



        [DataMember(IsRequired = true)]
        public List<hypermedia> hyperList { get; set; }



        public userReturn() { }

        public userReturn(List<User> uL,List<hypermedia> hL) {
            this.uList = uL;
            this.hyperList = hL;
        }


    }
}