using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("AnnouncementUsers")]
    public class AnnouncementUser 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AnouncementId { get; set; }

        public string UserId { get; set; }

        public bool? HasRead { get; set; }

        [ForeignKey("AnouncementId")]
        public virtual Announcement Announcement { set; get; }
    }
}
