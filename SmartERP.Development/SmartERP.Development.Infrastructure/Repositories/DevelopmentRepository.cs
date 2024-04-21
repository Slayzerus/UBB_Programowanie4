using SmartERP.CommonTools.Repositories;
using SmartERP.Development.Database;
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
    }
}
