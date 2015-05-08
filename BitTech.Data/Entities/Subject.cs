﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTech.Data.Entities
{
   public  class Subject
    {
       public int Id { get; set; }
       public string Name { get; set; }

       public Subject()
       {
           Courses = new List<Course>();
       }


       public ICollection<Course> Courses { get; set; }

       
     

    }
}
