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

        public string DatabaseContextsNamespace => $"{DatabaseNamespace}.Contexts";

        public string DatabaseContextName => $"{Name}Context";

        #region Infrastructure

        public string InfrastructureNamespace => $"{ModuleFullName}.Infrastructure";

        public string InfrastructureRepositoriesNamespace => $"{InfrastructureNamespace}.Repositories";

        public string InfrastructureRepositoryName => $"{Name}Repository";

        public string InfrastructureIRepositoryName => $"I{InfrastructureRepositoryName}";

        public string InfrastructureIRepositoryNamespace => $"{DomainNamespace}.Repositories";

        #endregion Infrastructure

        #region Application

        public string ApplicationNamespace => $"{ModuleFullName}.Application";

        public string ApplicationServicesNamespace => $"{ApplicationNamespace}.Services";
        public string ApplicationInterfacesNamespace => $"{ApplicationNamespace}.Interfaces";

        public string ApplicationServiceName => $"{Name}Service";

        public string ApplicationIServiceName => $"I{ApplicationServiceName}";

        #endregion Application

        #region CommonTools

        public string NuGetCommonToolsName => "SmartERP.CommonTools";

        public string GenericRepositoryName => "BaseRepository";

        public string GenericIRepositoryName => $"I{GenericRepositoryName}";

        public string GenericRepositoryNamespace => "SmartERP.CommonTools.Repositories";

        public string GenericServiceName => "BaseService";

        public string GenericIServiceName => $"I{GenericServiceName}";

        public string GenericServiceNamespace => "SmartERP.CommonTools.Services";

        #endregion CommonTools

        public Dictionary<CustomEntityModel, string> DbSetNames { get; set; } = new();
    }
}
