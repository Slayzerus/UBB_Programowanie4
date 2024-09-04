using ReactiveUI;
using SmartERP.Development.Application.Models;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

public class CustomEntityFormViewModel : ViewModelBase
{
    #region Fields

    private CustomEntityModel _selectedEntity = new();
    private CustomEntityFieldModel? _selectedField;
    private string _entityFormTitle;
    private CustomModuleFormViewModel _parentForm;
    private bool _isVisible;

    #endregion Fields

    public CustomEntityFormViewModel(CustomModuleFormViewModel parentForm)
    {
        AddFieldCommand = ReactiveCommand.Create(AddField_Click);
        EditFieldCommand = ReactiveCommand.Create<CustomEntityFieldModel>(EditField_Click);
        DeleteFieldCommand = ReactiveCommand.Create<CustomEntityFieldModel>(DeleteField_Click);
        SaveEntityCommand = ReactiveCommand.Create(SaveEntity_Click, this.WhenAnyValue(x => x.CanSaveEntity));
        CancelEntityCommand = ReactiveCommand.Create(CancelEntity_Click);
        
        _selectedEntity = new CustomEntityModel();
        _entityFormTitle = "New entity";
        _parentForm = parentForm;
    }

    #region Properties

    public bool IsVisible
    {
        get => _isVisible;
        set => this.RaiseAndSetIfChanged(ref _isVisible, value);
    }

    public string EntityFormTitle
    {
        get => _entityFormTitle;
        set => this.RaiseAndSetIfChanged(ref _entityFormTitle, value);
    }

    public CustomEntityModel SelectedEntity
    {
        get => _selectedEntity;
        set
        {
            Fields = new ObservableCollection<CustomEntityFieldModel>(value.Fields);
            this.RaiseAndSetIfChanged(ref _selectedEntity, value);
            this.RaisePropertyChanged(nameof(CanSaveEntity)); // Powiadomienie o zmianie
        }
    }

    public CustomEntityFieldModel? SelectedField
    {
        get => _selectedField;
        set => this.RaiseAndSetIfChanged(ref _selectedField, value);
    }

    public ObservableCollection<CustomEntityFieldModel> Fields { get; set; } = new();

    public bool CanSaveEntity => true;

    #endregion Properties

    #region Commands

    public ReactiveCommand<Unit, Unit> AddFieldCommand { get; }
    public ReactiveCommand<CustomEntityFieldModel, Unit> EditFieldCommand { get; }
    public ReactiveCommand<CustomEntityFieldModel, Unit> DeleteFieldCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveEntityCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelEntityCommand { get; }

    #endregion Commands

    #region Events

    private void AddField_Click()
    {
        var newField = new CustomEntityFieldModel { Entity = SelectedEntity };
        Fields.Add(newField);
        this.RaisePropertyChanged(nameof(Fields));
    }

    private void EditField_Click(CustomEntityFieldModel field)
    {
        SelectedField = field;
    }

    private void DeleteField_Click(CustomEntityFieldModel field)
    {
        SelectedEntity.Fields.Remove(field);
        this.RaisePropertyChanged(nameof(CanSaveEntity));
    }

    private void SaveEntity_Click()
    {
        SelectedEntity.Fields = Fields.ToList();
        _parentForm.SelectedEntity = SelectedEntity;
        _parentForm.SaveEntity_Click();
        IsVisible = false;
    }

    private void CancelEntity_Click()
    {
        IsVisible = false;
        _parentForm.CancelEntity_Click();
    }

    #endregion Events

    private void Clear()
    {
        _parentForm.SelectedEntity = new();
    }
}
