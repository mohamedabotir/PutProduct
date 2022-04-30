using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Repository;
using PutProduct.abstracts.Services;
using PutProduct.Data;
using PutProduct.Hubs;
using PutProduct.Model;

namespace PutProduct.Cores.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityService _user;
        private readonly IHubContext<NotificationHub, IHub> _hubContext;
        public ProductRepository(IHubContext<NotificationHub, IHub> hubContext, ApplicationDbContext context,IMapper mapper,IIdentityService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
            _hubContext = hubContext;
        }
        public async Task<int> CreateProduct(ProductModel product, string? userId)
        {
            var prod = _mapper.Map<ProductModel, Product>(product);

            _context.Products?.Add(prod);
            await _context.SaveChangesAsync();
            return prod.Id;
        }

        public async Task<int> DeleteProduct(string? userId, int productId)
        {
            var product = _context.Products?.
                Where(x => x.UserId == userId && x.Id == productId).SingleOrDefault();
            if (product == null)
                return 0;
            _context.Products?.Remove(product);
           await _context.SaveChangesAsync();
           return product.Id;
        }

        public async Task<int> ModifyProduct(ProductModel product, string? userId)
        {
            var models = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (models.UserId != userId)
            {
                return 0;
            }

            models.CategoryId = product.CategoryId;
            models.Description = product.Description;
            models.ImageUrl = product.ImageUrl;
            models.Name = product.Name;
            models.Price = product.Price;
            models.Quantity= product.Quantity;
            var model = _mapper.Map<ProductModel,Product>(product);
             
            var modify = _context.Products?.Update(models);
              _context.SaveChanges();
          return modify.Entity.Id;
        }

        public async Task<Product> RetrieveProduct(ProductModel product, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductModel>?> RetrieveMyProducts(string? userId)
        {
            var result = _context.Products?.Where(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<Product>,IEnumerable <ProductModel >>(result);
        }

        public async Task<IEnumerable<ProductModel>?> RetrieveAllProducts()
        {

            var result = _context.Products.Include(x=>x.User).Where(e => e.DeletedBy == null).AsEnumerable();
            return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductModel>>(result);
        }

        public async Task<ProductModel>? RetrieveSpecificProduct(int id)
        {
            var product = _context.Products.Include(x=>x.User).SingleOrDefault(x=>x.Id==id);
            var result = _mapper.Map<Product, ProductModel>(product);
            result.UserName = product.User.UserName;
            return result;
        }

        public async Task<bool> AddOrder(OrderModel model)
        {
            decimal totalPrice=0;
            string message = null;
            bool fail=false;


           
          

            var productcontext = _mapper.Map<ICollection<ProductModel>, ICollection<Product>>(model.OrderProducts);

            await _context.Database.BeginTransactionAsync();
            var products = model.OrderProducts;
            foreach (var item in products)
            {
                var data = _context.Products.FirstOrDefault(i => i.Id == item.Id);
                if (data == null)
                {
                    fail = true;
                    message = $"Product:{item.Name} not found";
                    return !fail;
                }

                if (item.qty < data.Quantity || item.qty == data.Quantity)
                {
                    totalPrice += item.qty * data.Price;


                }
                else
                {
                    fail = true;
                    message = $"Product:{item.Name} not Sufficient";
                    return !fail;
                }


            }
            if (fail)
            {
                return !fail;
            }

            var ProductOrder = _context.OrderProducts;
            var orders = _context.Orders;
            var discount = _context.Discounts.FirstOrDefault(x => x.Name == model.discountCode);
            var Order = new Order();
            
            if (discount == null)
            {
                Order = new Order()
                {
                    totalPrice = totalPrice,
                    UserId = _user.GetUserId(),
                    OrderTime = DateTime.Now
                    
                }; 
            }
            else
            {
                Order = new Order()
                {
                    DiscountId = discount.Id,
                    totalPrice = discount == null ? totalPrice : totalPrice * (decimal)(discount.DiscountValue / 100),
                    UserId = _user.GetUserId(),
                    OrderTime = DateTime.UtcNow
                }; 
            }
               
            

            _context.Orders.Add(Order);
            _context.SaveChanges();
              
            var Product = new Product();
            foreach (var item in products)
            {
                Product = _context.Products.FirstOrDefault(x => x.Id == item.Id);
                Product.Quantity -=item.qty;
                var user = await _context.User.FirstOrDefaultAsync(e => e.Id == Product.UserId);
                user.Balance += Product.Price;
                
                _context.Products.Update(Product);
                
                ProductOrder.Add(new OrderProducts() {OrderId = Order.Id, ProductId = item.Id, qty = item.qty});
                await _context.SaveChangesAsync();

            }

           await _context.Database.CommitTransactionAsync();
            if (!fail)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public IEnumerable<Order> GetAllOrders()
        {
            var userId = _user.GetUserId();
            var orders = _context.Orders.Where(e => e.UserId == userId).Include(e=>e.OrderProducts).ThenInclude(e=>e.Product).AsEnumerable();
            return orders;
        }

        public async Task<CommentModel> Comment(CommentModel comment)
        {
            var userName = _user.GetUserName();

            var commentMap = _mapper.Map<CommentModel, Comment>(comment);
            commentMap.UserId = _user.GetUserId();
            commentMap.CommentDateTime = DateTime.Now;
           await _context.Database.BeginTransactionAsync();
            var commentData = await _context.Comments.AddAsync(commentMap);
         await _context.SaveChangesAsync();
         var productOwner = _context.Products.FirstOrDefault(e => e.Id == comment.ProductId);
        await Notify(new NotificationModel(){Message = "You have Received Comment from "+userName,ReceiverId = productOwner.UserId});
        await _context.Database.CommitTransactionAsync();
        var result = _context.Comments.Include(e => e.User).FirstOrDefault(e => e.Id == commentData.Entity.Id);
         return _mapper.Map<Comment,CommentModel>(result);

        }

        public async Task<IEnumerable<CommentModel>> GetProductComment(int ProductId)
        {
            var result =   _context.Comments.Where(e => e.ProductId == ProductId && e.DeletedBy == null).Include(e=>e.User).AsEnumerable();

            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(result);

        }

        public async Task<CommentModel> GetComment(int commentId)
        {
            var comment = await _context.Comments.Include(e=>e.Product).FirstOrDefaultAsync(c => c.Id == commentId);
            var isOwn = comment.UserId == _user.GetUserId();
            if (!isOwn)
                return null;
            if (comment == null)
                return null;
            return _mapper.Map<Comment, CommentModel>(comment);
        }

        public async Task<bool> UpdateComment(UpdateCommentModel comment)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(e => e.Id == comment.Id);
            var isOwn = result.UserId == _user.GetUserId();
            if (!isOwn)
                return false;
            if (result == null)
            {
                return false;
            }

            result.Message = comment.Message;
            result.CommentDateTime = DateTime.Now;

            _context.Comments.Update(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(e => e.Id == id);
            var userId = _user.GetUserId();
            var isOwn = comment.UserId == userId;
            if (!isOwn)
                return false;
            if (comment == null)
                return false;

            _context.Comments.Remove(comment);
           await _context.SaveChangesAsync();
           return true;
        }

        private async Task Notify(NotificationModel model)
        {
            _context.Notifications.Add(new Notification() {ReceiverId = model.ReceiverId, Message = model.Message});
           await _context.SaveChangesAsync();
           await _hubContext.Clients.All.BroadcastNotification(model);
        }
    }
}
