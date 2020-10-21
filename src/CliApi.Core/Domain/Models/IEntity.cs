using System;

namespace CliApi.Core.Domain.Models
{
   public interface IEntity
    {
        int Id {get;set;}

        DateTime CreatedDate { get; set; }

        DateTime LastModifiedDate { get; set; }
        
    }
}