using EWallet.Data.Entities;
using EWallet.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EWallet.Service.Interfaces
{
    public interface IAppUserService
    {
        UserProfileViewModel GetUserProfile(string userName);

        Task<VerifyImageViewModel> GetVerifyImageInfo(string userId);

        VerifyAccountViewModel GetVerifyAccount(string userId);

        bool UpdateUrlImage(string userId, string urlImage);

        Task<bool> UpdateIdNumberAndFullName(string userId, string fullName, string idNumber);
    }
}
