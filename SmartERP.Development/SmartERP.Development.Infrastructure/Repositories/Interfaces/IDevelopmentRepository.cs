using SmartERP.CommonTools.Repositories;
using SmartERP.Development.Domain.Entities;

namespace SmartERP.Development.Infrastructure.Repositories.Interfaces
{
    public interface IDevelopmentRepository : IBaseRepository
    {
        CustomModule? GetCustomModuleById(long id);
    }
}
