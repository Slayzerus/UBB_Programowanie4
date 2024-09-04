using Mapster;
using SmartERP.CommonTools.Services;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Infrastructure.Repositories.Interfaces;

namespace SmartERP.Development.Application.Avalonia.Services
{
    public class DevelopmentService : BaseService, IDevelopmentService
    {
        private readonly IDevelopmentRepository _repository;

        public DevelopmentService(IDevelopmentRepository developmentRepository) : base(developmentRepository)
        {
            _repository = developmentRepository;
        }

        public CustomModuleModel? GetCustomModuleById(long id)
        {
            return _repository.GetCustomModuleById(id).Adapt<CustomModuleModel>();            
        }
    }
}
