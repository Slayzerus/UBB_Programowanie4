using SmartERP.Development.Application.Models;
using System.Collections.Generic;

namespace SmartERP.ModuleEditor.ReactiveUI.Models
{
    public class CustomModuleConfig : CustomModuleModel
    {
        public string ModuleFullName => $"SmartERP.Module.{Name.Replace(" ", "").Replace(".", "")}";

        public string RootPath { get; set; } = string.Empty;

        public string ModulePath => $"{RootPath}{ModuleFullName}";

        public string LibraryPath => $"{ModulePath}\\{ModuleFullName}";

        public string ApiPath => $"{ModulePath}\\{ModuleFullName}.API";
        public string VuePath => $"{ModulePath}\\{ModuleFullName}.VueJS";

        public string LibraryNamespace => $"{ModuleFullName}";

        public string DomainNamespace => $"{ModuleFullName}.Domain";

        public string DomainEntitiesNamespace => $"{DomainNamespace}.Entities";

        public string DatabaseNamespace => $"{ModuleFullName}.Database";

        public string InfrastructureNamespace => $"{ModuleFullName}.Infrastructure";

        public string InfrastructureRepositoriesNamespace => $"{InfrastructureNamespace}.Repositories";
        public string ApplicationNamespace => $"{ModuleFullName}.Application";

        public string ApplicationServicesNamespace => $"{ApplicationNamespace}.Services";

        public string NuGetCommonToolsName => "SmartERP.CommonTools";

        public string GenericRepositoryName => "IBaseRepository";

        public string RepositoryName => $"{Name}Repository";

        public string IRepositoryName => $"I{RepositoryName}";

        public Dictionary<CustomEntityModel, string> DbSetNames { get; set; } = new();
    }
}
