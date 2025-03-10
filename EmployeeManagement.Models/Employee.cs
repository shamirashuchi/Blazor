﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int id { get; set; }
        
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public  string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public int DepartmentId { get; set; }

        public string   PhotoPath { get; set; }

       
        public Department Department { get; set; }

    }
}
