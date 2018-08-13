using EWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("VerifyAccounts")]
    public class VerifyAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [MaxLength(256)]
        public string IdNumber { get; set; }

        public bool IsProfileVerified { get; set; }

        //Image for Authen
        public string AuthentiCateImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ProfileVerifyStatus Status { get; set; }

    }
}
