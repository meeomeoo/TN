using EWallet.Data.Enums;
using EWallet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("Annoucements")]
    public class Announcement : ISwitchable, IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Content { get; set; }

        public string SenderId { get; set; }

        public StatusEnum Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<AnnouncementUser> AnnouncementUsers { get; set; }

        [ForeignKey("SenderId")]
        public virtual AppUser Sender { get; set; }
    }
}
