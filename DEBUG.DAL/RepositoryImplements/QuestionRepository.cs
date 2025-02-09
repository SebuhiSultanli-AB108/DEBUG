using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;

namespace DEBUG.DAL.RepositoryImplements;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    public QuestionRepository(AppDbContext _context) : base(_context) { }
}