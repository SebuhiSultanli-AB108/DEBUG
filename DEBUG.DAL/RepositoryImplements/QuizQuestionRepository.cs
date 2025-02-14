using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;

namespace DEBUG.DAL.RepositoryImplements;

public class QuizQuestionRepository : GenericRepository<QuizQuestion>, IQuizQuestionRepository
{
    public QuizQuestionRepository(AppDbContext _context) : base(_context) { }
}