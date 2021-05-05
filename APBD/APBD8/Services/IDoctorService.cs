using APBD8.Models;
using APBD8.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD8.DTO.Request;

namespace APBD8.Services
{
    interface IDoctorService
    {
        public Task<IEnumerable<GetDoctorInfoResponseDto>> GetDoctors(MainDbContext context);
        public Task PostDoctor(MainDbContext context, GetDoctorInfoRequestDto doctorInput);
        public Task PutDoctor(MainDbContext context, GetDoctorInfoRequestDto doctorInput, string index);
        public Task DeleteDoctor(MainDbContext context, string index);
    }
}
