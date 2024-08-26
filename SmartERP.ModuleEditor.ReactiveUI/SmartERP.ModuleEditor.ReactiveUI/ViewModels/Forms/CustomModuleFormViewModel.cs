using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.Static;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public partial class CustomModuleFormViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IDevelopmentService _developmentService;

        public CustomModuleModel Module { get; set; } = new CustomModuleModel();

        // Implementacja INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanGenerate => Module.Id != 0;

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
    }
}
