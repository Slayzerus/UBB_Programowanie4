using ReactiveUI;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Lists
{
    public partial class CustomModulesListViewModel : ViewModelBase
    {
        private IDevelopmentService _developmentService;

        public CustomModulesListViewModel(long entityId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();
            Modules = new ObservableCollection<CustomModuleModel>(
                _developmentService.GetAll<CustomModule, CustomModuleModel>().ToList());

#if DEBUG
            if (Modules.Count == 0)
            {
                Modules.Add(CustomModuleModel.Example);
            }
#endif
        }

        //public ICommand AddModuleClickCommand { get; }

        public ObservableCollection<CustomModuleModel> Modules { get; set;  } = new() { CustomModuleModel.Example };

        public string NewModuleName { get; set; } = string.Empty;

        public bool IsAddModuleVisible { get; set; }

        //[RelayCommand]
        public void AddModule_Click()
        {
            AppWindows.MainWindowViewModel.Navigate(PageType.ModuleForm);
            //OpenModuleForm(new CustomModuleModel());
            //IsAddModuleVisible = !IsAddModuleVisible;
        }

        public void ConfirmAddModule_Click()
        {
            //Modules.Add(new CustomModuleModel() { Name = NewModuleName });
        }

        //public ICommand EditModuleCommand { get; }

        public void EditButton_Click(object idObj)
        {
            if (!(idObj is long))
            {
                return;
            }

            long id = (long)idObj;
            CustomModuleModel? model = Modules.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return;
            }

            AppWindows.MainWindowViewModel.Navigate(PageType.ModuleForm, id);
        }

        private void OpenModuleForm(CustomModuleModel model)
        {
            /*CustomModuleFormWindow customModuleFormWindow = new CustomModuleFormWindow();
            CustomModuleFormViewModel customModuleFormViewModel = new CustomModuleFormViewModel(this, customModuleFormWindow)
            {
                Module = model
            };
            customModuleFormWindow.DataContext = customModuleFormViewModel;
            customModuleFormWindow.Show();*/
        }
    }
}
