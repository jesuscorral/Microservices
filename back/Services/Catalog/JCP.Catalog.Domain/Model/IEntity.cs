using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCP.Catalog.Domain.Model
{
    public interface IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
