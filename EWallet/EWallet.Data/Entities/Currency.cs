using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Data.Entities
{
    [Table("Currencies")]
    public class Currency 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? CountryId { get; set; }

        [Required]
        [MaxLength(256)]
        public string CurrencyName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CurrencyCode { get; set; }

        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
