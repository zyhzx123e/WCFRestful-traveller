﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for hypermedia
/// </summary>
[DataContract(Namespace = "http://tempuri.org/")]
public class hypermedia
{


    [DataMember(IsRequired = true)]
    public  string link { get; set; }

    [DataMember(IsRequired = true)]
    public string description { get; set; }


    public hypermedia(string _link, string _description)
    { 
        this.link = _link;
        this.description = _description;

    }

    public hypermedia()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<hypermedia> UnauthorizedHypermedia()
    {


        List<hypermedia> lstbal = new List<hypermedia>();

        hypermedia hyper = new hypermedia("You are not authorized to access the Hypermedia link Info!",
            "You are not authorized to access the Hypermedia description Info!");
        lstbal.Add(hyper);

        return lstbal;

    }

}