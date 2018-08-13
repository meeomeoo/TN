using EWallet.Data.EF.Interfaces;
using EWallet.Data.Entities;
using EWallet.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EWallet.Service.Implements
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IRepository<Announcement, int> _announcementService;
        private readonly IRepository<AnnouncementUser, int> _announcementUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _log;

        public AnnouncementService(IRepository<Announcement, int> announcementService,
            IRepository<AnnouncementUser, int> announcementUserService,
            IUnitOfWork unitOfWork,
            ILogger<AnnouncementService> log)
        {
            _announcementService = announcementService;
            _announcementUserService = announcementUserService;
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Delete(Announcement announcement)
        {
            try
            {
                _log.LogInformation("Delete Announcement");
                var announcementUsers = announcement.AnnouncementUsers.ToList();
                if (announcementUsers != null)
                {
                    _announcementUserService.RemoveMultiple(announcementUsers);
                }
                _announcementService.Remove(announcement);
                _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                _log.LogError($"Delete Announcement error. {ex.ToString()}");
            }
            _log.LogInformation($"Delete Announcement success");
        }

        public List<Announcement> GetAllByConditions(Expression<Func<Announcement, bool>> conditions)
        {
            return _announcementService.FindAll(conditions).ToList();
        }

        public void Send(string userId, string title, string content, string[] receiversUser)
        {
            try
            {
                _log.LogInformation("Send Announcement - START");
                var announcement = new Announcement()
                {
                    SenderId = userId,
                    Title = title,
                    Content = content,
                    CreatedDate = DateTime.Now,
                    Status = Data.Enums.StatusEnum.Active
                };
                _log.LogInformation("Add Announcement");
                //Add Announcement
                _announcementService.Add(announcement);

                _log.LogInformation("Add AnnouncementUser");
                //Add AnnounmentUser
                foreach (var user in receiversUser)
                {
                    _announcementUserService.Add(new AnnouncementUser()
                    {
                        AnouncementId = announcement.Id,
                        UserId = user,
                        HasRead = false
                    });
                }

                //Save data
                _unitOfWork.Commit();
                _log.LogInformation("Send Announcement Success");
            }
            catch(Exception ex)
            {
                _log.LogError($"Send Announcement Error. {ex.ToString()}");
            }
        }

        public void Update(Announcement announcement)
        {
            _announcementService.Update(announcement);
        }
    }
}
