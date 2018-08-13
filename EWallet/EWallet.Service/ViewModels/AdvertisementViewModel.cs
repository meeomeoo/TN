using System;
using System.Collections.Generic;
using System.Text;
using EWallet.Data.Entities;
using EWallet.Data.Enums;

namespace EWallet.Service.ViewModels
{
    public class AdvertisementViewModel
    {
        public Advertisement AdvertisementUser { get; set; }
        public decimal BitcoinPrice { get; set; } = 0;
        public string CountryName { get; set; }
        public string Title { get; set; }
        public string TransactionName { get; set; }
        public string Message { get; set; }
    }

    public class AdvertisementCreateModel
    {
        public Advertisement AdvertisementUser { get; set; }

        public decimal UsdRate { get; set; } = 0;
        public decimal ReferenceExchange { get; set; } = 0;
        public double Fee { get; set; } = 0.01;
        public double MinAmount { get; set; } = 0.01;
        public double MaxAmount { get; set; } = 2;

        //public PaymentMethodEnum ListCurrency { get; set; }
        public List<Currency> ListCurrency { get; set; }
        public List<Bank> ListBank { get; set; }
        public List<Country> ListCountry { get; set; }
    }

    public class AdvertisementCreateRequestModel
    {
        public int Id { get; set; }

        public AdvertisementTypeEnum AdvertisementType { get; set; } //Buy or Sell

        public string UserId { get; set; }//UserId của người tạo quảng cáo

        public int ECurrencyId { get; set; } //Loại tiền điện tử: BTC, ETH...

        public int CurrencyId { get; set; } //Loại tiền tệ để đổi lấy tiền điện tử

        public decimal BitUSDPrice { get; set; } //Giá mua/bán(theo loại tiền tệ)/USD

        public decimal BitcoinPriceMaker { get; set; }

        public decimal BitcounPriceTaker { get; set; }

        public ReferenceExchangeEnum ReferenceExchange { get; set; }//Mã sàn giao dịch tham khảo giá tiền điện tử

        public decimal CoinPriceLimit { get; set; } //Giá tối thiểu(Bán)/tối đa(Mua) bạn chấp nhận cho 1 Bitcoin (tính theo VND)

        public int CountryId { get; set; } //Vị trí quốc gia hiển thị quảng cáo

        public decimal MinAmount { get; set; } //Số BTC tối thiểu trong một giao dịch.

        public decimal MaxAmount { get; set; } //Số BTC tối đa trong một giao dịch (Bán thì còn phụ thuộc số dư của ví BTC hiện tại).

        public PaymentMethodEnum PaymentMethod { get; set; }

        public bool RejectUserNotVeryfied { get; set; } //Chỉ chấp nhận người mua đã xác minh

        public int PaymentTime { get; set; } //Thời gian tối đa cho một giao dịch 15p or 30p

        public int BankId { get; set; } //Mã ngân hàng của người mua/bán

        public AdvertisementStatusEnum Status { get; set; } //Trạng thái của quảng cáo

        public string BankAccountNumber { get; set; }

        public string BankAccountName { get; set; }
    }

    public class AdvertisementCreateResponseModel
    {

    }
}
