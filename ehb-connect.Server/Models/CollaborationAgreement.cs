using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ehb_connect.Server.Models
{
    public class CollaborationAgreement
    {
        [Key]
        public int AgreementId { get; set; }

        [Required]
        [ForeignKey("ContactRequest")]
        public int RequestId { get; set; }

        [Required]
        [StringLength(500)]
        public string What { get; set; } = string.Empty;

        public DateTime AgreedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(100)]
        public string ContactMethod { get; set; } = string.Empty;

        public bool ConfirmedBySender { get; set; }
        public bool ConfirmedByReceiver { get; set; }

        public ContactRequest ContactRequest { get; set; } = null!;
    }
}
