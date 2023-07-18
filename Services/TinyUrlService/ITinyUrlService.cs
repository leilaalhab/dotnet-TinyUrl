using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.TinyUrlService
{
    public interface ITinyUrlService
    {
        Task<ServiceResponse<List<GetTinyUrlDto>>> GetAllUrls();

        Task<ServiceResponse<GetTinyUrlDto>> GetUrlById(int Id);

        Task<ServiceResponse<List<GetTinyUrlDto>>> AddUrl(AddTinyUrlDto newurl);

        Task<ServiceResponse<List<GetTinyUrlDto>>> DeleteUrl(int Id);

        Task<ServiceResponse<GetTinyUrlDto>> SetExpiration(UpdatedTinyUrlDto updatedTinyUrl);

        Task<ServiceResponse<List<GetTinyUrlDto>>> CreateUrl(AddTinyUrlDto newUrl);

        int getUserId();
        
    }
}