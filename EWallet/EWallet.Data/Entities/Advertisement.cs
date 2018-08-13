using EWallet.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EWallet.Data.Entities
{
    [Table("Advertisements")]
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public AdvertisementTypeEnum AdvertisementType { get; set; } //Buy or Sell

        public string UserId { get; set; }//UserId của người tạo quảng cáo

        public int ECurrencyId { get; set; } //Loại tiền điện tử: BTC, ETH...

        public int CurrencyId { get; set; } //Loại tiền tệ để đổi lấy tiền điện tử

        [Column(TypeName = "decimal(18,8)")]
        public decimal BitUSDPrice { get; set; } //Giá mua/bán(theo loại tiền tệ)/USD

        [Column(TypeName = "decimal(18,8)")]
        public decimal BitcoinPriceMaker { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal BitcounPriceTaker { get; set; }

        public ReferenceExchangeEnum ReferenceExchange { get; set; }//Mã sàn giao dịch tham khảo giá tiền điện tử

        [Column(TypeName = "decimal(18,8)")]
        public decimal CoinPriceLimit { get; set; } //Giá tối thiểu(Bán)/tối đa(Mua) bạn chấp nhận cho 1 Bitcoin (tính theo VND)

        public int CountryId { get; set; } //Vị trí quốc gia hiển thị quảng cáo

        [Column(TypeName = "decimal(18,8)")]
        public decimal MinAmount { get; set; } //Số BTC tối thiểu trong một giao dịch.

        [Column(TypeName = "decimal(18,8)")]
        public decimal MaxAmount { get; set; } //Số BTC tối đa trong một giao dịch (Bán thì còn phụ thuộc số dư của ví BTC hiện tại).

        public PaymentMethodEnum PaymentMethod { get; set; }

        public bool RejectUserNotVeryfied { get; set; } //Chỉ chấp nhận người mua đã xác minh

        public int PaymentTime { get; set; } //Thời gian tối đa cho một giao dịch 15p or 30p

        public int BankId { get; set; } //Mã ngân hàng của người mua/bán

        public AdvertisementStatusEnum Status { get; set; } //Trạng thái của quảng cáo

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
