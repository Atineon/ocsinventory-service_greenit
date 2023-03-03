using System.Management.Automation;
using System.Text;

namespace GreenIT
{
    public static class GreenITPowerShell
    {
        private static PowerShell _console = PowerShell.Create();

        public static string Command(string script)
        {
            string errorMsg = string.Empty;
            string output = string.Empty;
            PSDataCollection<PSObject> outputCollection = new();
            StringBuilder stringBuilder = new();

            _console.AddCommand("Set-ExecutionPolicy").AddParameter("ExecutionPolicy", "RemoteSigned").AddParameter("scope", "LocalMachine").Invoke();

            _console.AddScript(script);
            _console.AddCommand("Out-String");

            _console.Streams.Error.DataAdded += (object sender, DataAddedEventArgs e) =>
            {
                errorMsg = ((PSDataCollection<ErrorRecord>)sender)[e.Index].ToString();
            };

            IAsyncResult result = _console.BeginInvoke<PSObject, PSObject>(null, outputCollection);
            _console.EndInvoke(result);

            foreach (var outputItem in outputCollection)
            {
                stringBuilder.AppendLine(outputItem.BaseObject.ToString());
            }

            _console.Commands.Clear();

            if (!string.IsNullOrEmpty(errorMsg)) return errorMsg;
            return stringBuilder.ToString().Trim();
        }
    }
}
