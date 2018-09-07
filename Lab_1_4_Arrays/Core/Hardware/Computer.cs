
namespace Lab_1_4_Arrays.Core.Hardware
{
    internal abstract class Computer
    {
        #region Fields
        internal string CPU;
        internal string RAM;
        internal string HDD;
        #endregion

        #region Methods
        internal abstract void SetCPU(string cpu);
        internal abstract void SetRAM(string ram);
        internal abstract void SetHDD(string hdd);


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "All computers";
        }
        #endregion

        #region Operators
        public static bool operator ==(Computer c1, Computer c2)
        {
            return c1.CPU == c2.CPU && c1.RAM == c2.RAM & c1.HDD == c2.HDD;
        }

        public static bool operator !=(Computer c1, Computer c2)
        {
            return c1.CPU != c2.CPU || c1.RAM != c2.RAM || c1.HDD != c2.HDD;
        }
        #endregion
    }
}
