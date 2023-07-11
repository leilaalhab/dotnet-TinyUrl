using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class QandA
    {
        public int Id { get; set; }
        public String Question { get; set; } = "";
        
        public List<Answer>? Answers { get; set;}

        public User? User { get; set; }
        public int userId { get; set; }
    }
}