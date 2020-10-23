using System.ComponentModel.DataAnnotations;

namespace CliApi.Core.Domain.Models
{
    public class Command: Entity
    {
        public string HowTo { get; set; }
        public string Platform { get; set; }
        public string CommandLine { get; set; }
    }
}