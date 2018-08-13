using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.Enums
{
    /// <summary>
    /// Lưu trạng thái của 1 giao dịch
    /// </summary>
    public enum TransactionStatusEnum
    {
        // đang trả tiền 
        // chờ trả tiền
        Paying,

        //GD bán: Người mua xác nhận đã thanh toán, chờ người bán xác nhận đã nhận tiền, 
        //GD mua: Người mua xác nhận đã thanh toán, chờ người bán xác nhận đã nhận tiền
        Pair,

        // Người mua xác nhận đã thanh toán, người bán xác nhận đã nhận tiền
        PairAccepted,

        //Giao dịch đang ở trạng thái tranh chấp, chờ admin xử lys
        Dispute,

        // Đã hoàn thành giao dịch
        Finished,

        // Giao dịch đã bị hủy bỏ
        Cancelled
    }
}
