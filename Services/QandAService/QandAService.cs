using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.QandAService
{
    public class QandAService : IQandAService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public int getUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        public QandAService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetQandADto>> AddQuestion(AddQandADto newQuestion)
        {
            var serviceResponse = new ServiceResponse<GetQandADto>();
            var question = _mapper.Map<QandA>(newQuestion);
            question.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == getUserId());

            _context.QandAs.Add(question);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetQandADto>(question);
            
            return serviceResponse;
        }

        

        public async Task<ServiceResponse<List<GetQandADto>>> DeleteQuestion(int Id)
        {
             var serviceResponse = new ServiceResponse<List<GetQandADto>>();

            try {
            
            var question = await _context.QandAs.FirstOrDefaultAsync(c => c.Id == Id && c.User!.Id == getUserId());

            if (question is null)
                throw new Exception($"question with id {Id} not found");

            
            _context.QandAs.Remove(question);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.QandAs
            .Where(c => c.User!.Id == getUserId())
            .Select(c => _mapper.Map<GetQandADto>(c)).ToListAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetQandADto>> UpdateQuestion(UpdatedQandADto updatedQuestion)
        {
            var serviceResponse = new ServiceResponse<GetQandADto>();

            try {
            
            var question = await _context.QandAs
            .Include(c => c.User) // to access objects you might have to include them first
            .FirstOrDefaultAsync(c => c.Id == updatedQuestion.Id);

            if (question is null || question.User!.Id != getUserId())
                throw new Exception($"question with id {updatedQuestion.Id} not found");
            
            question.Question = updatedQuestion.Question;

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetQandADto>(question);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetQandADto>>> GetQandAs()
        {
            var serviceResponse = new ServiceResponse<List<GetQandADto>>();
            var dbQandAs = await _context.QandAs.Include(c => c.Answers)
            .Where(c => c.User!.Id == getUserId()).ToListAsync();
            serviceResponse.Data = dbQandAs.Select(c => _mapper.Map<GetQandADto>(c)).ToList();
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<GetQandADto>> GetQandAbyId(int Id)
        {
            var serviceResponse = new ServiceResponse<GetQandADto>();
            var dbQandA =  await _context.QandAs
            .Include(c => c.Answers).
            FirstOrDefaultAsync(c => c.Id == Id ); // we are also sending the status code 200 ok along with our mock character
            serviceResponse.Data = _mapper.Map<GetQandADto>(dbQandA); // the values of character are being mapped to the Character DTO
            return serviceResponse;
        }
    }
}