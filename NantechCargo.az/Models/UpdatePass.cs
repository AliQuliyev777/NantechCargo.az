using System.ComponentModel.DataAnnotations;

namespace NantechCargo.az.Models
{
    public class UpdatePass
    {
        [Required (ErrorMessage = "Bos olmaz")]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string RePassword { get; set; }
    }
}
