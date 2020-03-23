using AutoMapper;
using Common.Layer;
using Domain.Dto.Layer;
using Persistence.Layer;
using Services.Layer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Layer
{
    public interface IStoreService
    {
        Task<DataCollection<StoreDto>> Paged(int page, int take, IEnumerable<QueryFilter> filters = null);
    }

    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<StoreDto>> Paged(int page, int take, IEnumerable<QueryFilter> filters = null)
        {
            var result = new DataCollection<StoreDto>();
            try
            {
                var query = _context.Stores.OrderBy(x => x.StoreId).AsQueryable();
                var data = await query.AsPagedAsync(page, take);
                result = Mapper.Map<DataCollection<StoreDto>>(data);
            }
            catch (Exception e)
            {
            }
            return result;
        }
    }
}