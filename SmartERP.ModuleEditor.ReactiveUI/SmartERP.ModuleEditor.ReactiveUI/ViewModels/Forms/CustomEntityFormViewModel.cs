using ReactiveUI;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using System.Collections.ObjectModel;
using System.Reactive;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public class CustomEntityFormViewModel : ViewModelBase
    {
        private IDevelopmentService _developmentService;

        private CustomEntityModel _entity;
        public CustomEntityModel Entity
        {
            get => _entity;
            set => this.RaiseAndSetIfChanged(ref _entity, value);
        }

        public ObservableCollection<CustomEntityFieldModel> Fields { get; } = new ObservableCollection<CustomEntityFieldModel>();

        public ReactiveCommand<Unit, Unit> AddFieldCommand { get; }
        public ReactiveCommand<CustomEntityFieldModel, Unit> RemoveFieldCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public CustomEntityFormViewModel(long entityId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();

            if (entityId != 0)
            {
                Entity = _developmentService.GetById<CustomEntity, CustomEntityModel>(entityId) ?? new();
                foreach (var field in Entity.Fields)
                {
                    Fields.Add(field);
                }
            }
            else
            {
                Entity = new CustomEntityModel();
            }

            AddFieldCommand = ReactiveCommand.Create(AddField);
            RemoveFieldCommand = ReactiveCommand.Create<CustomEntityFieldModel>(RemoveField);
            SaveCommand = ReactiveCommand.Create(SaveEntity);
            CancelCommand = ReactiveCommand.Create(Cancel);
        }

        private void AddField()
        {
            var newField = new CustomEntityFieldModel { Entity = Entity, EntityId = Entity.Id };
            Fields.Add(newField);
        }

        private void RemoveField(CustomEntityFieldModel field)
        {
            if (Fields.Contains(field))
            {
                Fields.Remove(field);
            }
        }

        private void SaveEntity()
        {
            if (Entity.Id == 0)
            {
                _developmentService.Add<CustomEntity, CustomEntityModel>(Entity);
            }
            else
            {
                _developmentService.Update<CustomEntity, CustomEntityModel>(Entity);
            }
        }

        private void Cancel()
        {
            // Implementacja nawigacji lub inna logika zamknięcia formularza
        }
    }
}
