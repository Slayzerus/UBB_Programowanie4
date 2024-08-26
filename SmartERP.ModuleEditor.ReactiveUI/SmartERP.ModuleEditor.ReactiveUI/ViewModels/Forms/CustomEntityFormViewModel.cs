using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public class CustomEntityFormViewModel : INotifyPropertyChanged
    {
        private IDevelopmentService _developmentService;
        private CustomEntityFieldModel? _selectedField = null;
        private CustomEntityModel _selectedEntity = new();

        public CustomEntityModel SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                _selectedEntity = value;
                OnPropertyChanged();
            }
        }

        public CustomEntityFieldModel? SelectedField
        {
            get => _selectedField;
            set
            {
                _selectedField = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CustomEntityFormViewModel(long enityId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();
            if (enityId != 0)
            {
                CustomEntityModel? entity = _developmentService.Get<CustomEntity, CustomEntityModel>(x => x.Id == enityId);
                if (entity != null)
                {
                    SelectedEntity = entity;
                    if (entity.Fields.Count > 0)
                    {
                        SelectedField = entity.Fields[0];
                    }
                }
            }
        }

        // Dodawanie nowego pola
        public void AddField_Click()
        {
            SelectedField = new CustomEntityFieldModel() { Entity = SelectedEntity };
            SelectedEntity.Fields.Add(SelectedField);
        }

        // Edytowanie istniejącego pola
        public void EditField_Click(CustomEntityFieldModel field)
        {
            SelectedField = field;
        }

        // Usuwanie pola
        public void DeleteField_Click(CustomEntityFieldModel field)
        {
            SelectedEntity.Fields.Remove(field);
        }

        // Zapisanie encji (w tym także pól)
        public void SaveEntity_Click()
        {
            // Logika zapisu encji
        }

        // Anulowanie edycji encji
        public void CancelEntity_Click()
        {
            // Logika anulowania edycji
        }
    }
}
