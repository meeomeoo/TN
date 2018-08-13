using EWallet.Data.Enums;
using EWallet.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Data.Entities
{
    [Table("Wallets")]
    public class Wallet : IDateTracking, ISwitchable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }

        public int CurrencyId { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Balance { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public StatusEnum Status { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<WalletHistory> WalletHistories { get; set; }
    }
}
