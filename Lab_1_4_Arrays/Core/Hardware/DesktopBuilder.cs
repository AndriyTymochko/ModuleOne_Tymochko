
namespace Lab_1_4_Arrays.Core.Hardware
{
    internal class DesktopBuilder : Computer
    {
        #region Construcors 
        public DesktopBuilder()
        {
            SetCPU();
            SetRAM();
            SetHDD();
        }
        #endregion

        #region Methods
        internal override void SetCPU(string cpu = "4 cores, 2.5 HGz")
        {
            CPU = string.IsNullOrEmpty(cpu) ? "4 cores, 2.5 HGz" : cpu;
        }

        internal override void SetRAM(string ram = "6 GB")
        {
            RAM = string.IsNullOrEmpty(ram) ? "6 GB" : ram;
        }

        internal override void SetHDD(string hdd = "500 GB")
        {
            HDD = string.IsNullOrEmpty(hdd) ? "500 GB" : hdd;
        }

        public override string ToString()
        {
            return "Desktop";
        }
        #endregion
    }
}
