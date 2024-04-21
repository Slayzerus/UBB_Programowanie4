using ReactiveUI;
using SmartERP.Development.Application.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Lists
{
    public partial class CustomModulesListViewModel : ViewModelBase
    {
        public CustomModulesListViewModel()
        {
        }



        //public ICommand AddModuleClickCommand { get; }

        public ObservableCollection<CustomModuleModel> Modules { get; set;  } = new() { CustomModuleModel.Example };

        public string NewModuleName { get; set; } = string.Empty;

        public bool IsAddModuleVisible { get; set; }

        //[RelayCommand]
        public void AddModule_Click()
        {
            OpenModuleForm(new CustomModuleModel());
            //IsAddModuleVisible = !IsAddModuleVisible;
        }

        public void ConfirmAddModule_Click()
        {
            //Modules.Add(new CustomModuleModel() { Name = NewModuleName });
        }

        //public ICommand EditModuleCommand { get; }

        public void EditModule_Click(object idObj)
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

            OpenModuleForm(model);
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
