
using NiceToDev.CommandLine;

CmdClient cmd = new();
cmd.AddCommand("cd c:\\Users\\slayz\\source\\repos\\SmartERP\\SmartERP.ModuleEditor.ReactiveUI\\SmartERP.ModuleEditor.ReactiveUI.Desktop\\bin\\Debug\\net8.0\\Modules\\SmartERP.Module.Example\\");
cmd.AddCommand("dotnet new sln -n SmartERP.Module.Example");
string result = cmd.Execute();
Console.WriteLine(result);