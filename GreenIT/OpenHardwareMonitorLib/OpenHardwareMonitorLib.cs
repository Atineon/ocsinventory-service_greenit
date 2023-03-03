using OpenHardwareMonitor.Hardware;

namespace GreenIT
{
    public static class OpenHardwareMonitorLib
    {
        private static readonly UpdateVisitor _updateVisitor = new();
        private static readonly Computer _computer = new();
        public static List<string> GetCPUPower()
        {
            List<string> CPUPower = new();
            _computer.Open();
            _computer.CPUEnabled = true;
            _computer.Accept(_updateVisitor);
            for (int i = 0; i < _computer.Hardware.Length; i++)
            {
                if (_computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < _computer.Hardware[i].Sensors.Length; j++)
                    {
                        if (_computer.Hardware[i].Sensors[j].SensorType == SensorType.Power) CPUPower.Add("OpenHardwareMonitor: " + _computer.Hardware[i].Sensors[j].Name + ": " + _computer.Hardware[i].Sensors[j].Value.ToString() + " W");
                    }
                }
            }
            _computer.Close();
            return CPUPower;
        }
    }
}
