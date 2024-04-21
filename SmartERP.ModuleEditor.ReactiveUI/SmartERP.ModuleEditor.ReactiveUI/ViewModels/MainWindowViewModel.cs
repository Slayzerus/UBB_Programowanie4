using ReactiveUI;
using SmartERP.Development.Application.Avalonia.Services.Interfaces;
using SmartERP.ModuleEditor.ReactiveUI.Enums;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels.Lists;
using System.Collections.Generic;
using System;

namespace SmartERP.ModuleEditor.ReactiveUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Navigate(PageType.ModuleList);
        }

        public Dictionary<PageType, Type> Pages { get; } = new()
        {
            { PageType.ModuleList, typeof(CustomModulesListViewModel) }
        };

        private ViewModelBase _currentPage = new CustomModulesListViewModel();

        public ViewModelBase CurrentPage
        {
            get { return _currentPage; }
            private set { this.RaiseAndSetIfChanged(ref _currentPage, value); }
        }

        public void Navigate(PageType page, long entityId = 0)
        {
            CurrentPage = (ViewModelBase)Activator.CreateInstance(Pages[page], entityId);
        }
    }
}
