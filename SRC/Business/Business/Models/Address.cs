﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
    [Table("TB_Address")]
    public class Address : Entity
    {
        public Guid SupplierId { get; set; }
                
        public string Street { get; set; }        
        public string Number { get; set; }                
        public string Complement { get; set; }        
        public string ZipCode { get; set; }        
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }               

    }
}
