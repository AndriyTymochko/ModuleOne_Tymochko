
namespace Lab_1_4_Arrays.Core.Hardware
{
    internal class ServerBuilder : Computer
    {
        #region Construcors 
        public ServerBuilder()
        {
            SetCPU();
            SetRAM();
            SetHDD();
        }
        #endregion

        #region Methods
        internal override void SetCPU(string cpu = "8 cores, 3 HGz")
        {
            CPU = string.IsNullOrEmpty(cpu) ? "8 cores, 3 HGz" : cpu;
        }

        internal override void SetRAM(string ram = "16 GB")
        {
            RAM = string.IsNullOrEmpty(ram) ? "16 GB" : ram;
        }

        internal override void SetHDD(string hdd = "2048 GB")
        {
            HDD = string.IsNullOrEmpty(hdd) ? "2048 GB" : hdd;
        }

        public override string ToString()
        {
            return "Server";
        }
        #endregion
    }
}
