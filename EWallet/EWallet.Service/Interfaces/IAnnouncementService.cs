using EWallet.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Interfaces
{
    public interface IAnnouncementService
    {
        void Send(string userId, string title, string content, string[] receiversUser);

        List<Announcement> GetAllByConditions(Expression<Func<Announcement, bool>> conditions);

        void Update(Announcement announcement);

        void Delete(Announcement announcement);
    }
}
