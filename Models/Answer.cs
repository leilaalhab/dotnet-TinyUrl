using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public String Text { get; set; } = "";
        public int userId { get; set; }
    
        public QandA? QandA { get; set; }
        public int QandAId { get; set; }

    }
}