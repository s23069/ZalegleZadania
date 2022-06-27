
using APBD8_DK.Models.DTOs;
using System.Threading.Tasks;

namespace APBD8_DK.Services
{
    public interface IPrescriptionDbService
    {
        Task<PrescriptionDTO> GetPrescription(int id);

    }
}
