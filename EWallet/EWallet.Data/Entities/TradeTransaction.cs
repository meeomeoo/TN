using EWallet.Data.Enums;
using EWallet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("TradeTransactions")]
    public class TradeTransaction : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        public int AdvertisementId { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal EAmount { get; set; }

        [MaxLength(256)]
        public string ReferenceMessage { get; set; }

        public TransactionStatusEnum Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string PaymentProof { get; set; }

        [ForeignKey("AdvertisementId")]
        public virtual Advertisement Advertisement { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

    }
}
