using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.Static;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public partial class CustomModuleFormViewModel : ViewModelBase
    {
        private IDevelopmentService _developmentService;

        public CustomModuleModel Module { get; set; } = new CustomModuleModel();

        public CustomModuleFormViewModel(long entityId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();
            if (entityId != 0)
            {
                CustomModuleModel? module = _developmentService.Get<CustomModule, CustomModuleModel>(x => x.Id == entityId);
                if (module != null)
                {
                    Module = module;
                }                
            }
        }

        public void SaveButton_Click()
        {
            if (Module.Id == 0)
            {
                _developmentService.Add<CustomModule, CustomModuleModel>(Module);
            }
            else
            {
                _developmentService.Update<CustomModule, CustomModuleModel>(Module);
            }
        }

        public void GenerateButton_Click()
        {
            ModuleGenerator moduleGenerator = new ModuleGenerator();
            moduleGenerator.GenerateModule(CustomModuleModel.Example);
        }

        public void CloseButton_Click()
        {
            AppWindows.MainWindowViewModel.Navigate(PageType.ModuleList);
        }
        /*private readonly CustomModuleFormWindow _form;

        private readonly CustomModulesListViewModel _listViewModel;

        private CustomModuleModel _module = new CustomModuleModel();

        private string _nameValidationMessage = string.Empty;

        public CustomModuleFormViewModel(CustomModulesListViewModel listViewModel, CustomModuleFormWindow form)
        {
            _listViewModel = listViewModel;
            _form = form;
        }

        public void AddCustomEntity()
        {
            //Module.Entities.Add(new CustomEntityModel() { Module = Module });
        }

        public void RemoveCustomEntity(CustomEntityModel entity)
        {
            //Module.Entities.Remove(entity);
        }

        public void SaveModule()
        {
            if (Module.Id != 0)
            {
                _listViewModel.Modules.Add(Module);
            }
            _form.Close();
        }*/
    }
}
