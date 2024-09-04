using SmartERP.CommonTools.Services;
using SmartERP.Development.Application.Models;

namespace SmartERP.Development.Application.Avalonia.Services.Interfaces
{
    public interface IDevelopmentService : IBaseService
    {
        CustomModuleModel? GetCustomModuleById(long id);
    }
}
