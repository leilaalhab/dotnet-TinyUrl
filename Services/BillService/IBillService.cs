using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.BillService
{
    public interface IBillService
    {
        Task<ServiceResponse<List<GetBillDto>>> GetBills();
        int getUserId();
    }
}