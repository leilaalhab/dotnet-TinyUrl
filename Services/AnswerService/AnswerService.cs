using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.AnswerService
{
    public class AnswerService : IAnswerService
    {
         private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int getUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public AnswerService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetQandADto>> AddAnswer(AddAnswerDto newAnswer)
        {
            var response = new ServiceResponse<GetQandADto>();
            try{
                var question = await _context.QandAs
                .FirstOrDefaultAsync(c => c.Id == newAnswer.questionId);
            
                if (question is null)
                {
                    response.Success = false;
                    response.Message = "question not found.";
                    return response;
                }

                var answer = new Answer{
                    Text = newAnswer.Text,
                    userId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                    QandA = question
                };

                _context.Answers.Add(answer);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetQandADto>(question);
            }catch(Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<GetQandADto>> DeleteAnswer(int Id)
        {
             var serviceResponse = new ServiceResponse<GetQandADto>();

            try {
            
            var answer = await _context.Answers.FirstOrDefaultAsync(c => c.Id == Id);

            if (answer is null)
                throw new Exception($"answer with id {Id} not found");

            
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            serviceResponse.Data =_mapper.Map<GetQandADto>( await _context.QandAs.FirstOrDefaultAsync
            (c => c.Id == answer.QandAId));
            

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAnswerDto>> UpdateAnswer(UpdatedAnswerDto updatedAnswer)
        {
            var serviceResponse = new ServiceResponse<GetAnswerDto>();

            try {
            
            var answer = await _context.Answers
            .Include(c => c.QandA) // to access objects you might have to include them first
            .FirstOrDefaultAsync(c => c.Id == updatedAnswer.Id);

            if (answer is null)
                throw new Exception($"question with id {updatedAnswer.Id} not found");
            
            answer.Text = updatedAnswer.Text;

            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetAnswerDto>(answer);

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