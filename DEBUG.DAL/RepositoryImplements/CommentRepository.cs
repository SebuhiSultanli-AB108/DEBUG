using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;

namespace DEBUG.DAL.RepositoryImplements;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext _context) : base(_context) { }
}