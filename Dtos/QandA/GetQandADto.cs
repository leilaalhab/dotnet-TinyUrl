using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.QandA
{
    public class GetQandADto
    {
        public int questionId { get; set; }
        public String Question { get; set; } = "";
        
        public List<GetAnswerDto>? Answers { get; set;}

    }
}