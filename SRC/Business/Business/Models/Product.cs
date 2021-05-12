using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Business.Models
{
    [Table("TB_Product")]
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public DateTime InsertDate { get; set; }
        public bool Active { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }

       

    }
}
