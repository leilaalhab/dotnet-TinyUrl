using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Bill
{
    public class GetBillDto
    {
         public int Id { get; set; }
         
        public long CardNumber { get; set; }

        public DateTime DatePurchased { get; set; } = DateTime.Now;

        public float charged {get; set;}

        public SubscriptionTypeClass item {get; set;}
    }
}