using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Klinger.ViewModels
{
    public class EntityViewModels
    {
        [Key]
        public Guid Id { get; set; }

        public EntityViewModels()
        {
            Id = Guid.NewGuid();
        }
    }
}
