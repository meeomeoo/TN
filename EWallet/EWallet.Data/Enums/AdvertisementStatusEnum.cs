using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Data.Enums
{
    /// <summary>
    /// Lưu trạng thái của 1 quảng cáo
    /// </summary>
    public enum AdvertisementStatusEnum
    {
        Available = 1, // Sẵn sàng để giao dịch
        Pending = 2, // Tạm dừng
        LockedForTransaction = 3, // Đang bị khóa để thực hiện giao dịch
        Closed = 4, // Đã close
        Delete = 5 // Đã close
    }
}
