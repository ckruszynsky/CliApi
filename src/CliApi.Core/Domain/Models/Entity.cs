
using System;
using System.ComponentModel.DataAnnotations;

namespace CliApi.Core.Domain.Models
{
    public class Entity : IEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

    }
}