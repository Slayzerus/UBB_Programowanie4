using Microsoft.EntityFrameworkCore;
using SmartERP.CommonTools.Repositories;
using SmartERP.Development.Database;
using SmartERP.Development.Domain.Entities;
using SmartERP.Development.Infrastructure.Repositories.Interfaces;

namespace SmartERP.Development.Infrastructure.Repositories
{
    public class DevelopmentRepository : BaseRepository, IDevelopmentRepository 
    {
        private DevelopmentContext _context;

        public DevelopmentRepository(DevelopmentContext context) : base(context)
        {
            _context = context;
        }

        public CustomModule? GetCustomModuleById(long id)
        {
            return _context.Modules
                .Where(x => x.Id == id)
                .Include(x => x.Entities)
                .ThenInclude(x => x.Fields)
                .FirstOrDefault();
        }
    }
}
