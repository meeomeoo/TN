using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("Banks")]
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(255)]
        public string BankDescription { get; set; }

        public int Sequence { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual ICollection<BankUser> BankUsers { get; set; }

    }
}
