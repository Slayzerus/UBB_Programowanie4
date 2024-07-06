using System.Text;

namespace NiceToDev.CommandLine
{
    public class CmdClient
    {
        private bool _powerShell = false;

        private bool _showWindow = false;

        private List<string> queue = new();

        public CmdClient(bool showWindow = false, bool powerShell = false)
        {
            _powerShell = powerShell;
            _showWindow = showWindow;
        }                

        public void AddCommand(string command)
        {
            queue.Add(command);
        }

        public void ClearCommands()
        {
            queue.Clear();
        }

        public string Execute(bool clearQueue = true, string command = "")
        {
            if (!string.IsNullOrEmpty(command))
            {
                queue.Add(command);
            }

            var process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = _powerShell ? "powershell.exe" : "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = _showWindow
                }
            };

            StringBuilder output = new();
            process.Start();

            foreach (var item in queue)
            {
                process.StandardInput.WriteLine(item);                
                output.AppendLine(item);
            }            
            process.StandardInput.Flush();
            process.StandardInput.Close();

            output.AppendLine(process.StandardOutput.ReadToEnd());            
            process.WaitForExit();

            if (clearQueue)
            {
                ClearCommands();
            }

            return output.ToString();
        }
    }
}
