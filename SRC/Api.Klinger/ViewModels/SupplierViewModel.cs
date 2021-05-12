using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Klinger.ViewModels
{
    public class SupplierViewModel : EntityViewModels
    {

        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Document { get; set; }
        public int TypeSupplier { get; set; }
        public AddressViewModel Address { get; set; }
        public bool Active { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }

       
    }
}
