using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;

namespace DEBUG.DAL.RepositoryImplements;

public class ReportItemRepository : GenericRepository<ReportItem>, IReportItemRepository
{
    public ReportItemRepository(AppDbContext _context) : base(_context) { }
}