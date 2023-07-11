using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            CreateMap<AddTinyUrlDto, TinyUrl>();
            CreateMap<TinyUrl, GetTinyUrlDto>();
            CreateMap<Usage, GetUsageDto>();

            CreateMap<AddQandADto, QandA>();
            CreateMap<QandA, GetQandADto>();

            CreateMap<Answer, GetAnswerDto>();
            CreateMap<AddAnswerDto, Answer>();

            CreateMap<User, GetUserDto>();
            CreateMap<Bill, GetBillDto>();

        }
        

    }
}