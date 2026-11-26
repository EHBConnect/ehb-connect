using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace ehb_connect.Server.Models
{
    public class ContactRequest
    {
        // PK
        public int ContactRequestId { get; set; }

        // FK's with navigation properties

        // User who initiated the contact request
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;

        // User receiving the contact request
        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;

        // Optional project related to request (nullable)
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        // Initial message sent with the request
        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        // Status of the request, initial = pending
        public ContactRequestStatus Status { get; set; } = ContactRequestStatus.Pending;

        // Timestamp when the contact request was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional: timestamp when the request was responded to
        public DateTime? RespondedAt { get; set; }

        public ICollection<CollaborationAgreement> CollaborationAgreements { get; set; } = new List<CollaborationAgreement>();

    }
}
