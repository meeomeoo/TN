using System;
using System.Collections.Generic;
using System.Text;

namespace EWallet.Utilities.Constants
{
    public static class AppConfigConstants
    {
        public const string USER_IS_AUTHENTICATED = "Đã xác minh";
        public const string USER_IS_NOTAUTHENTICATED = "Chưa xác minh";
        public const string USER_TRADETYPE_NEWBUYER = "Người mua mới";
        public const string USER_TRADETYPE_NEWSELLER = "Người bán chậm";

        public const string USER_VEIRFY_PHONE_CONTENT = "Mã xác thực số điện thoại của bạn là: {0}";
        public const int USER_PHONE_VERIFY_TIMEOUT = 15;
        public const int USER_ACCOUNT_VERIFY_IMAGE_NUMBER = 4;

    }
}
