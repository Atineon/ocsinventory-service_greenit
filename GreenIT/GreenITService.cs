using System.ComponentModel;

namespace GreenIT
{
    public class GreenITService
    {
        private readonly BackgroundWorker _worker;

        public GreenITService()
        {
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
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
            List<string> Sensors;
            string message;
            while (true)
            {
                Sensors = OpenHardwareMonitorLib.GetCPUPower();
                foreach (var sensorInfo in Sensors)
                {
                    message = DateTime.Now.ToString() + ": " + sensorInfo + "\r";
                    Console.WriteLine(message);
                    File.AppendAllText(@"C:\ProgramData\OCS Inventory NG\Agent\GreenIT.log", message);
                }
                Console.WriteLine("fichier actualisé");
                Thread.Sleep(5000);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Engine stopped");
            _worker.CancelAsync();
        }
    }
}
