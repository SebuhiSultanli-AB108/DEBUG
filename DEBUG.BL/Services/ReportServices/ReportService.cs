using AutoMapper;
using DEBUG.BL.DTOs.ReportItemDTOs;
using DEBUG.BL.Exceptions.Common.Common;
using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;

namespace DEBUG.BL.Services.ReportServices;

public class ReportService(IReportItemRepository _repository, IMapper _mapper) : IReportService
{
    public async Task<int> CreateAsync(ReportItemCreateDTO dto, User user)
    {
        ReportItem report = _mapper.Map<ReportItem>(dto);
        report.ReporterUserId = user.Id;
        await _repository.CreateAsync(report);
        await _repository.SaveChangesAsync();
        return report.Id;
    }
    public async Task<IEnumerable<ReportItemGetDTO>> GetAllAsync()
    {
        IEnumerable<ReportItem> reports = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReportItemGetDTO>>(reports);
    }
    public async Task<ReportItemGetDTO> GetByIdAsync(int id)
    {
        ReportItem? report = await _repository.GetByIdAsync(id);
        if (report == null) throw new NotFoundException<ReportItem>();
        return _mapper.Map<ReportItemGetDTO>(report);
    }
    public async Task<IEnumerable<ReportItemGetDTO>> GetByReportedUserIdAsync(string id)
    {
        IEnumerable<ReportItem> reports = await _repository.GetWhereAsync(x => x.ReportedUserId == id);
        return _mapper.Map<IEnumerable<ReportItemGetDTO>>(reports);
    }
    public async Task HardDeleteAsync(int id)
    {
        ReportItem? target = await _repository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<ReportItem>();
        await _repository.HardDeleteAsync(target);
        await _repository.SaveChangesAsync();
    }
}