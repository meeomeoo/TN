using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EWallet.Data.Entities;
using EWallet.Service.ViewModels;
using EWallet.Utilities.Dtos;

namespace EWallet.Service.Interfaces
{
    public interface IAdvertisementService
    {
        /// <summary>
        /// Thêm mới quảng cáo
        /// JP - 06/07/2018
        /// </summary>
        /// <returns></returns>
        ServiceResponse AddNewAdvertisement(AdvertisementCreateRequestModel model);

        /// <summary>
        /// Lay 1 quang cao
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Advertisement GetById(int id);

        /// <summary>
        /// Lay theo dieu kien
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        List<Advertisement> GetMulty(Expression<Func<Advertisement, bool>> conditions);


        /// <summary>
        /// Update thông tin quảng cáo mua
        /// </summary>
        /// <returns></returns>
        bool UpdateAdvertisement(Advertisement model);
    }
}
