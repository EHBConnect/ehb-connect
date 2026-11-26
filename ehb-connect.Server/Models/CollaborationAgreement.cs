using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.Models
{
    public class CollaborationAgreement
    {
        public int AgreementId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        [StringLength(500)]
        public string What { get; set; } = string.Empty;

        public DateTime AgreedDate { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactMethod { get; set; }

        public bool ConfirmedBySender { get; set; }
        public bool ConfirmedByReceiver { get; set; }

        public ContactRequest ContactRequest { get; set; }

    }
}

