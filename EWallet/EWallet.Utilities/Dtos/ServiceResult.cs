using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using EWallet.Utilities.Extensions;

namespace EWallet.Utilities.Dtos
{
    /// <summary>
    /// Lưu kết quả trả về của các method ở TransactionService
    /// </summary>
    public class ServiceResult
    {
        public bool IsOK; // true: thực hiện thành công, false: thực hiện thất bại
        public string ErrorMessage; // chi tiết lỗi khi thực hiện thất bại
        public object Data; // Data tra ve neu co
    }

    public class ServiceResponse
    {
        public StatusCode Code { get; set; } = StatusCode.Fail;
        public string Message { get; set; } = StatusCode.Fail.Text();
        public object Data { get; set; } // Data tra ve neu co
    }

    public enum StatusCode
    {
        [Description("System error")]
        SystemError = -1,

        [Description("Success")]
        Success = 1,

        [Description("Fail")]
        Fail = 2
    }
}
