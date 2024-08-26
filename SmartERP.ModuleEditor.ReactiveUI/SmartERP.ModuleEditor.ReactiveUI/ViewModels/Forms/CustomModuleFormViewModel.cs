using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using ReactiveUI;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public partial class CustomModuleFormViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDevelopmentService _developmentService;
        private bool _isEntityFormVisible;

        public CustomModuleModel Module { get; set; } = new CustomModuleModel();

        public CustomEntityModel SelectedEntity { get; set; } = new CustomEntityModel();

        // Implementacja INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Właściwość kontrolująca widoczność formularza
        public bool IsEntityFormVisible
        {
            get => _isEntityFormVisible;
            set
            {
                _isEntityFormVisible = value;
                OnPropertyChanged();
            }
        }

        public bool CanGenerate => Module.Id != 0;

        public CustomModuleFormViewModel(long moduleId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();
            if (moduleId != 0)
            {
                CustomModuleModel? module = _developmentService.Get<CustomModule, CustomModuleModel>(x => x.Id == moduleId);
                if (module != null)
                {
                    Module = module;
                }
            }

            // Ukryj formularz podczas uruchamiania
            IsEntityFormVisible = false;
        }

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

            // Powiadomienie o zmianie wartości CanGenerate
            OnPropertyChanged(nameof(CanGenerate));
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

        public void AddEntity_Click()
        {
            // Otwórz formularz dla nowego entity
            SelectedEntity = new CustomEntityModel();
            IsEntityFormVisible = true;
        }

        public void EditEntity_Click(CustomEntityModel entity)
        {
            // Otwórz formularz dla edytowanego entity
            SelectedEntity = entity;
            IsEntityFormVisible = true;
        }

        public void SaveEntity_Click()
        {
            // Zapisz entity do listy i ukryj formularz
            if (!Module.Entities.Contains(SelectedEntity))
            {
                Module.Entities.Add(SelectedEntity);
            }
            IsEntityFormVisible = false;
        }

        public void CancelEntity_Click()
        {
            // Ukryj formularz bez zapisu
            IsEntityFormVisible = false;
        }
    }
}
