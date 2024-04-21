using ReactiveUI;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.Development.Application.Models;
using SmartERP.Development.Domain.Entities;
using SmartERP.ModuleEditor.ReactiveUI.Static;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms
{
    public class CustomEntityFormViewModel : ViewModelBase
    {
        private IDevelopmentService _developmentService;

        public CustomEntityModel Entity { get; set; }

        public CustomEntityFormViewModel(long entityId = 0)
        {
            _developmentService = DependencyResolver.Instance.Get<IDevelopmentService>();

            if (entityId != 0)
            {
                Entity = _developmentService.GetById<CustomEntity, CustomEntityModel>(entityId) ?? new();
            }
            else
            {
                Entity = new();
            }
        }
    }
}
