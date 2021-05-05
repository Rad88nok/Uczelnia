using APBD8.DTO.Response;
using APBD8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Services
{
    public interface IPrescriptionServise
    {
        public Task<GetPrescriptionInfoResponseDto> GetPrescription(MainDbContext context, string index);
    }
}
