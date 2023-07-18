using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace dotnet_rpg.Services.TinyUrlService
{
    public class TinyUrlService : ITinyUrlService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TinyUrlService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public int getUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetTinyUrlDto>>> CreateUrl(AddTinyUrlDto newUrl) {
            var serviceResponse = new ServiceResponse<List<GetTinyUrlDto>>();
            var url = _mapper.Map<TinyUrl>(newUrl);
            url.User = null;

            _context.TinyUrls.Add(url);
            await _context.SaveChangesAsync(); // saves to the database and generates new id for the character
            serviceResponse.Data = await _context.TinyUrls
                .Where(c => c.User!.Id == getUserId())
            .Select(c => _mapper.Map<GetTinyUrlDto>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTinyUrlDto>>> AddUrl(AddTinyUrlDto newUrl)
        {
            var serviceResponse = new ServiceResponse<List<GetTinyUrlDto>>();
            var url = _mapper.Map<TinyUrl>(newUrl);
            url.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == getUserId());

            _context.TinyUrls.Add(url);
            await _context.SaveChangesAsync(); // saves to the database and generates new id for the character
            serviceResponse.Data = await _context.TinyUrls
                .Where(c => c.User!.Id == getUserId())
            .Select(c => _mapper.Map<GetTinyUrlDto>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTinyUrlDto>>> GetAllUrls()
        {
            var serviceResponse = new ServiceResponse<List<GetTinyUrlDto>>();
            var dbTinyUrls = await _context.TinyUrls.Include(c => c.Usages)
            .Where(c => c.User!.Id == getUserId()).ToListAsync(); // accessing the characters in our database
            serviceResponse.Data = dbTinyUrls.Select(c => _mapper.Map<GetTinyUrlDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTinyUrlDto>> GetUrlById(int Id)
        {
             var serviceResponse = new ServiceResponse<GetTinyUrlDto>();
            var dbTinyUrl =  await _context.TinyUrls.Include(c => c.Usages)
            .FirstOrDefaultAsync(c => c.Id == Id && c.User!.Id == getUserId()); // we are also sending the status code 200 ok along with our mock character
            serviceResponse.Data = _mapper.Map<GetTinyUrlDto>(dbTinyUrl); // the values of character are being mapped to the Character DTO
            return serviceResponse;
            // first or default might return null so we must handle this by allowing it to be nullable or use a nullforgiving operator
        }




        public async Task<ServiceResponse<List<GetTinyUrlDto>>> DeleteUrl(int Id)
        {
            var serviceResponse = new ServiceResponse<List<GetTinyUrlDto>>();

            try {
            
            var tinyUrl = await _context.TinyUrls.FirstOrDefaultAsync(c => c.Id == Id && c.User!.Id == getUserId());

            if (tinyUrl is null)
                throw new Exception($"url with id {Id} not found");

            
            _context.TinyUrls.Remove(tinyUrl);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.TinyUrls
            .Where(c => c.User!.Id == getUserId())
            .Select(c => _mapper.Map<GetTinyUrlDto>(c)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTinyUrlDto>> SetExpiration(UpdatedTinyUrlDto updatedtinyUrl){

            var serviceResponse = new ServiceResponse<GetTinyUrlDto>();

            try {
            
            var tinyUrl = await _context.TinyUrls
            .Include(c => c.User) // to access objects you might have to include them first
            .FirstOrDefaultAsync(c => c.Id == updatedtinyUrl.Id);

            if (tinyUrl is null || tinyUrl.User!.Id != getUserId())
                throw new Exception($"character with id {updatedtinyUrl.Id} not found");

        
            tinyUrl.DateExpired = updatedtinyUrl.DateExpired;
            await _context.SaveChangesAsync();


            serviceResponse.Data = _mapper.Map<GetTinyUrlDto>(tinyUrl);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

       
    }
}