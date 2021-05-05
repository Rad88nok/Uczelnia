using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.DTO.Response
{
    public class GetPrescriptionInfoResponseDto
    {
        public DateTime date { get; set; }
        public DateTime dueDate { get; set; }
        public string patient { get; set; }
        public string doctor { get; set; }
        public IEnumerable<GetMedicamentInfoResponseDto> Medicaments { get; set; } = new HashSet<GetMedicamentInfoResponseDto>();

    }
}
