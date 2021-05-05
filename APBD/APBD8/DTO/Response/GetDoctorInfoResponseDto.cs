using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.DTO.Response
{
    public class GetDoctorInfoResponseDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public IEnumerable<GetPatientInfoResponseDto> Patients { get; set; } = new List<GetPatientInfoResponseDto>();

    }
}
