
namespace Lab_1_4_Arrays.Core.Hardware
{
    internal class LaptopBuilder : Computer
    {
        #region Construcors 
        public LaptopBuilder()
        {
            SetCPU();
            SetRAM();
            SetHDD();
        }
        #endregion

        #region Methods
        internal override void SetCPU(string cpu = "2 cores, 1.7 HGz")
        {
            CPU = string.IsNullOrEmpty(cpu) ? "2 cores, 1.7 HGz" : cpu;
        }

        internal override void SetRAM(string ram = "4 GB")
        {
            RAM = string.IsNullOrEmpty(ram) ? "4 GB" : ram;
        }

        internal override void SetHDD(string hdd = "250 GB")
        {
            HDD = string.IsNullOrEmpty(hdd) ? "250 GB" : hdd;
        }

        public override string ToString()
        {
            return "Laptop"; 
        }
        #endregion
    }
}
