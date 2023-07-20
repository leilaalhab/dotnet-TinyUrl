using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
         private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public int getUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public SubscriptionService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetUserDto>> GetSubscriptionType()
        {
             var response = new ServiceResponse<GetUserDto>();
             
            try{
                var user = await _context.Users.FirstOrDefaultAsync(c =>  c.Id == getUserId());

                response.Data = _mapper.Map<GetUserDto>(user);
            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateSubscription(UpdatedSubscriptionDto updatedSubscriptionDto)
        {
            var response = new ServiceResponse<GetUserDto>();
            User? user = await  _context.Users.FirstOrDefaultAsync(c =>  c.Id == getUserId());
            var bill = new Bill{User = await _context.Users.FirstOrDefaultAsync(u => u.Id == getUserId()), userId = getUserId(), CardNumber = updatedSubscriptionDto.CardNumber};
            try{
                switch (updatedSubscriptionDto.SubscriptionType)
                {
                    case SubscriptionTypeClass.Normal:
                        {
                            user!.subscriptionType = SubscriptionTypeClass.Normal;
                            bill.charged = 0;
                            bill.item = SubscriptionTypeClass.Normal;
                            break;
                        }

                    case SubscriptionTypeClass.Premium:
                        {
                            user!.subscriptionType = SubscriptionTypeClass.Premium;
                            bill.charged = 50;
                            bill.item = SubscriptionTypeClass.Premium;
                            break;
                        }

                    case SubscriptionTypeClass.Ultra:
                        {
                            user!.subscriptionType = SubscriptionTypeClass.Ultra;
                            bill.charged = 100;
                            bill.item = SubscriptionTypeClass.Ultra;
                            break;
                        }
                }

                
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUserDto>(user);
            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }
    }
}