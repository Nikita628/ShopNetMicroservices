using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Infrastructure;
using Order.Core.Models;
using OrderDbModel = Order.Infrastructure.Database.Models.Order;
using OrderCoreModel = Order.Core.Models.Order;

namespace Order.Infrastructure.Database.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _dbContext;
        private readonly IMapper _mapper;

        public OrderRepository(OrderContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderCoreModel> AddAsync(OrderCoreModel model)
        {
            var dbModel = _mapper.Map<OrderDbModel>(model);

            _dbContext.Orders.Add(dbModel);

            await _dbContext.SaveChangesAsync();

            return await GetByIdAsync(dbModel.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Orders.FindAsync(id);

            if (entity != null)
                _dbContext.Orders.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<OrderCoreModel>> GetAllAsync()
        {
            var list = await _dbContext.Orders.ToListAsync();

            return _mapper.Map<List<OrderCoreModel>>(list);
        }

        public async Task<OrderCoreModel> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Orders.FindAsync(id);

            return _mapper.Map<OrderCoreModel>(entity);
        }

        public async Task<IEnumerable<OrderCoreModel>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();

            return _mapper.Map<List<OrderCoreModel>>(orderList);
        }

        public async Task<IReadOnlyList<OrderCoreModel>> Search(OrderSearchParam searchParam)
        {
            IQueryable<OrderCoreModel> query = _dbContext.Set<OrderCoreModel>();

            if (searchParam.Id != null)
            {
                query = query.Where(o => o.Id == (int)searchParam.Id.SearchValue);
            }

            if (searchParam.UserName != null)
            {
                query = query.Where(o => o.UserName.Contains((string)searchParam.UserName.SearchValue));
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(OrderCoreModel model)
        {
            var entity = await _dbContext.FindAsync<OrderDbModel>(model.Id);

            if (entity != null)
            {
                var dbModel = _mapper.Map<OrderDbModel>(model);
                entity.AddressLine = dbModel.AddressLine;
                entity.CardName = dbModel.CardName;
                entity.CardNumber = dbModel.CardNumber;
                entity.Country = dbModel.CardNumber;
                entity.CVV = dbModel.CVV;
                entity.EmailAddress = dbModel.EmailAddress;
                entity.Expiration = dbModel.Expiration;
                entity.FirstName = dbModel.FirstName;
                entity.LastModifiedBy = "nik";
                entity.LastModifiedDate = DateTime.UtcNow;
                entity.PaymentMethod = dbModel.PaymentMethod;
                entity.State = dbModel.State;
                entity.TotalPrice = dbModel.TotalPrice;
                entity.UserName = dbModel.UserName;
                entity.ZipCode = dbModel.ZipCode;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}