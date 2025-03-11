using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.Core.Entities;

namespace DEBUG.BL.Services.ReportServices;

public interface IReportService
{
    Task<int> CreateAsync(ReportItemCreateDTO dto, User user);
    Task HardDeleteAsync(int id);
    Task<IEnumerable<ReportItemGetDTO>> GetAllAsync(short pageNo, short take);
    Task<IEnumerable<ReportItemGetDTO>> GetByReportedUserIdAsync(short pageNo, short take, string id);
    Task<ReportItemGetDTO> GetByIdAsync(int id);
}
