using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;

namespace DEBUG.DAL.RepositoryImplements;

public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
{
    public AnswerRepository(AppDbContext _context) : base(_context) { }
}