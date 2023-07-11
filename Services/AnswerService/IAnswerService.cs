using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.AnswerService
{
    public interface IAnswerService
    {
        Task<ServiceResponse<GetQandADto>> AddAnswer(AddAnswerDto newAnswer);

        Task<ServiceResponse<GetAnswerDto>> UpdateAnswer(UpdatedAnswerDto updatedAnswer);

        Task<ServiceResponse<GetQandADto>> DeleteAnswer(int Id);
    }
}