using EWallet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("BankUsers")]
    public class BankUser : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BankId { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        [MaxLength(50)]
        public string BankAccountNumber { get; set; }

        [MaxLength(256)]
        public string BankAccountName { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
