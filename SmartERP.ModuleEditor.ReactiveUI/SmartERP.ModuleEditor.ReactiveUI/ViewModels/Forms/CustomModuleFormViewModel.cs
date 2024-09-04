using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public partial class CustomModuleFormViewModel : ViewModelBase
    {
        #region Fields

        private IDevelopmentService _developmentService;
        private bool _isEntityFormVisible;

        #endregion Fields

        #region Properties

        public CustomModuleModel Module { get; set; } = new CustomModuleModel();

        public CustomEntityModel SelectedEntity { get; set; } = new CustomEntityModel();

        public CustomEntityFormViewModel SelectedEntityViewModel { get; set; }

        public ObservableCollection<CustomEntityModel> Entities { get; set; } = new();

        public bool CanGenerate => Module.Id != 0;

        #endregion Properties

        public CustomModuleFormViewModel(long moduleId = 0)
        {
            SelectedEntityViewModel = new CustomEntityFormViewModel(this);

            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();
            if (moduleId != 0)
            {
                CustomModuleModel? module = _developmentService.GetCustomModuleById(moduleId);

                if (module != null)
                {
                    Module = module;
                    Entities = new ObservableCollection<CustomEntityModel>(Module.Entities);
                }
            }
        }

        #region Events

        public void SaveButton_Click()
        {
            if (Module.Id == 0)
            {
                Module = _developmentService.Add<CustomModule, CustomModuleModel>(Module);
            }
            else
            {
                _developmentService.Update<CustomModule, CustomModuleModel>(Module);
            }

            foreach (CustomEntityModel entity in Entities)
            {
                entity.ModuleId = Module.Id;

                /*foreach (CustomEntityFieldModel field in entity.Fields)
                {
                    field.Entity = null;
                }*/

                if (entity.Id == 0)
                {
                    entity.Id = _developmentService.Add<CustomEntity, CustomEntityModel>(entity).Id;
                }
                else
                {
                    _developmentService.Update<CustomEntity, CustomEntityModel>(entity);
                }
            }

            this.RaisePropertyChanged(nameof(CanGenerate));
        }

        public void GenerateButton_Click()
        {
            ModuleGenerator moduleGenerator = new ModuleGenerator();
            moduleGenerator.GenerateModule(Module);
        }

        public void CloseButton_Click()
        {
            AppWindows.MainWindowViewModel.Navigate(PageType.ModuleList);
        }

        public void AddEntity_Click()
        {
            SelectedEntity = new CustomEntityModel();
            SelectedEntityViewModel.SelectedEntity = SelectedEntity;
            SelectedEntityViewModel.IsVisible = true;
        }

        public void EditEntity_Click(CustomEntityModel entity)
        {
            SelectedEntity = entity;
            SelectedEntityViewModel.SelectedEntity = SelectedEntity;
            SelectedEntityViewModel.IsVisible = true;
        }

        public void SaveEntity_Click()
        {
            if (!Module.Entities.Contains(SelectedEntity))
            {
                Entities.Add(SelectedEntity);
            }
            SelectedEntity = new CustomEntityModel();
        }

        public void CancelEntity_Click()
        {
            SelectedEntity = new CustomEntityModel();
        }

        #endregion Events
    }
}
