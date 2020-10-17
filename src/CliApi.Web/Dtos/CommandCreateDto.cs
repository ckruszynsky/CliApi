using System.ComponentModel.DataAnnotations;

namespace CliApi.Web.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string CommandLine { get; set; }
    }
}