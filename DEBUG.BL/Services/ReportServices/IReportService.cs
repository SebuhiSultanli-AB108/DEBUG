using DEBUG.BL.DTOs.ReportItemDTOs;

namespace DEBUG.BL.Services.ReportServices;

public interface IReportService
{
    Task<int> CreateAsync(ReportItemCreateDTO dto);
    Task HardDeleteAsync(int id);
    Task<IEnumerable<ReportItemGetDTO>> GetAllAsync();
    Task<IEnumerable<ReportItemGetDTO>> GetByReportedUserIdAsync(string id);
    Task<ReportItemGetDTO> GetByIdAsync(int id);
}
