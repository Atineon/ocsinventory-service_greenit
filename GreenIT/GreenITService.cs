using System.ComponentModel;
using System.Reflection;

namespace GreenIT
{
    public class GreenITService
    {
        private readonly BackgroundWorker _worker;
        private string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public GreenITService()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Engine;
        }

        public void Start()
        {
            Console.WriteLine("Stating Engine...");
            _worker.RunWorkerAsync();
        }

        public void Engine(object? sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Engine started");
            while (true)
            {
                File.AppendAllText(@"C:\ProgramData\OCS Inventory NG\Agent\GreenIT.log", GreenITPowerShell.Command(_path + @"\helloworld.ps1"));
                // Anti-CPUBreaker
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Engine stopped");
            _worker.CancelAsync();
        }
    }
}
