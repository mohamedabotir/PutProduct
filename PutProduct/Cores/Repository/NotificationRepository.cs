using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Repository;
using PutProduct.abstracts.Services;
using PutProduct.Data;
using PutProduct.Model;

namespace PutProduct.Cores.Repository
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private  readonly IIdentityService _identityService;

        public NotificationRepository(ApplicationDbContext context,IMapper mapper,IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _identityService = identityService;
        }
        public  async Task<IEnumerable<NotificationModel>> GetNotifications()
        {
            var userId = _identityService.GetUserId();
            var notifications = _context.Notifications
                .Where(e => e.ReceiverId == userId && e.SenderId !=userId)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationModel>>(notifications);
        }

        public async Task<bool> MarkItAsRead(IEnumerable<NotificationModel> model)
        {
            var map = _mapper.Map<IEnumerable<NotificationModel>, IEnumerable<Notification>>(model);
            var NotReaded = map.Where(e => e.isRead == false);

            if (!NotReaded.Any(e=>e.isRead==false))
            {
                return false;
            } 
          
            _context.Notifications.UpdateRange(NotReaded);
            _context.SaveChanges();
            
            
            
            return true;

        }

        public async Task<int> GetUnReadedNotificationCount()
        {
            var userId = _identityService.GetUserId();
            var count = await _context.Notifications.Where(e => e.isRead == false && e.ReceiverId == userId && e.SenderId!=userId).CountAsync();
            return count;
             
        }
    }
}
