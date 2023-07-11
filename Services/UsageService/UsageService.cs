using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.UsageService
{
    public class UsageService : IUsageService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UsageService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetTinyUrlDto>> AddUsage(AddUsageDto newUsage)
        {
            var response = new ServiceResponse<GetTinyUrlDto>();
            try{
                var thisTinyUrl = await _context.TinyUrls.Include(c => c.Usages)
                .FirstOrDefaultAsync(c => c.Id == newUsage.UrlId &&c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            
                if (thisTinyUrl is null)
                {
                    response.Success = false;
                    response.Message = "tinyUrl not found.";
                    return response;
                }

                if (thisTinyUrl.DateExpired <= DateTime.Now) {
                    response.Success = false;
                    response.Message = "tinyUrl has expired.";
                    return response;
                }



                var Usage = new Usage{
                    Region = newUsage.Region,
                    tinyUrl = thisTinyUrl,
                };

                switch (thisTinyUrl.User.subscriptionType)
                { 
                    case SubscriptionTypeClass.Premium: 
                        thisTinyUrl.UsagesCount = thisTinyUrl.UsagesCount + 1;
                        await _context.SaveChangesAsync();
                        break;
                    case SubscriptionTypeClass.Ultra: 
                        _context.Usages.Add(Usage);
                        thisTinyUrl.UsagesCount = thisTinyUrl.UsagesCount + 1;
                        thisTinyUrl.Usages!.Add(Usage!);
                        await _context.SaveChangesAsync();
                        break;
                }

                response.Data = _mapper.Map<GetTinyUrlDto>(thisTinyUrl);
            }catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}