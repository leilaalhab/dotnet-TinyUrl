using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Answer
{
    public class AddAnswerDto
    {
        public String Text { get; set; } = "";
        public int questionId { get; set; }
    }
}