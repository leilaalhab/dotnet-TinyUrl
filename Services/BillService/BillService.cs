using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.BillService
{
    public class BillService : IBillService
    {
         private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int getUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        public BillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetBillDto>>> GetBills()
        {
            var serviceResponse = new ServiceResponse<List<GetBillDto>>();
            var dbBills = await _context.Bills.Where(c => c.User!.Id == getUserId()).ToListAsync();
            serviceResponse.Data = dbBills.Select(c => _mapper.Map<GetBillDto>(c)).ToList();
            return serviceResponse;
        }
    }
}