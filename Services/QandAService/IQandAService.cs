using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.QandAService
{
    public interface IQandAService
    {
        Task<ServiceResponse<GetQandADto>> AddQuestion(AddQandADto newQuestion);

        Task<ServiceResponse<GetQandADto>> UpdateQuestion(UpdatedQandADto updatedQuestion);

        Task<ServiceResponse<List<GetQandADto>>> DeleteQuestion(int Id);

        Task<ServiceResponse<List<GetQandADto>>> GetQandAs();

        Task<ServiceResponse<GetQandADto>> GetQandAbyId(int Id);

        int getUserId();
    }
}