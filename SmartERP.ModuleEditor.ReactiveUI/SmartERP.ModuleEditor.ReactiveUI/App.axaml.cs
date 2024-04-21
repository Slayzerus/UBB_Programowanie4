using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SmartERP.Development.Application.Avalonia;
using SmartERP.ModuleEditor.ReactiveUI.Static;
using SmartERP.ModuleEditor.ReactiveUI.ViewModels;
using SmartERP.ModuleEditor.ReactiveUI.Views;
using System;

namespace SmartERP.ModuleEditor.ReactiveUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = AppWindows.MainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = AppWindows.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
