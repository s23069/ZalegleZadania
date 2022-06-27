
using APBD8_DK.Models.DTOs;
using System.Threading.Tasks;

namespace APBD8_DK.Services
{
    public interface IDoctorDbService
    {
        Task<DoctorDTO> GetDoctor(int id);
        Task AddDoctor(DoctorDTO doctor);
         Task UpdateDoctor(int id, DoctorDTO doctor);
         Task DeleteDoctor(int id);
    }
}
