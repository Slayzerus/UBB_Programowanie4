using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SmartERP.ModuleEditor.ReactiveUI.Extensions
{
    public static class ResourceHostExtensions
    {
        public static IServiceProvider GetServiceProvider(this IResourceHost control)
        {
            return (IServiceProvider)control.FindResource(typeof(IServiceProvider));
        }

        public static T CreateInstance<T>(this IResourceHost control)
        {
            return ActivatorUtilities.CreateInstance<T>(control.GetServiceProvider());
        }
    }
}
