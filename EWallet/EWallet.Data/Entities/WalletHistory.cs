using EWallet.Data.Enums;
using EWallet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("WalletHistories")]
    public class WalletHistory : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        public int WalletId { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal BeforeWallet { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal AfterWallet { get; set; }

        public WalletHistoryTypeEnum WalletHistoryType { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}
