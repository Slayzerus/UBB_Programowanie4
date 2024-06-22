using SmartERP.Development.Application.Models;

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
    }
}
