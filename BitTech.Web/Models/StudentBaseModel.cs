﻿using BitTech.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitTech.Web.Models
{
    public class StudentBaseModel
    {
        
        
            public int Id { get; set; }
            public string Url { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public int EnrollmentsCount { get; set; }

        

    }
}