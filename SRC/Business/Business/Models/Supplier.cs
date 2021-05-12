using Business.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
    [Table("TB_Supplier")]
    public class Supplier : Entity
    {     
        public string Name { get; set; }
                
        public string Document { get; set; }
        public TypeSupplier TypeSupplier { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; }

        public IEnumerable<Product> Products { get; set; }


    }
}
