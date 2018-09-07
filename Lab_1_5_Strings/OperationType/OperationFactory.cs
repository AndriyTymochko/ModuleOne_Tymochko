using System;

namespace Lab_1_5_Strings.OperationType
{
    internal static class OperationFactory
    {
        #region Methods

        internal static IOperationType GetSelectedOperation(ConsoleKey enterdKey)
        {
            try
            {
                switch (enterdKey)
                {
                    case ConsoleKey.NumPad1:
                        return new OperationType.AbsoluteValue.AbsoluteValueOfDifference(1000);
                    case ConsoleKey.NumPad2:
                        return new OperationType.BinaryString.BinaryConverter(350);
                    case ConsoleKey.NumPad3:
                        return new OperationType.CodeAndSound.MorseCode();
                    default:
                        return default(IOperationType);
                }
            }
            catch (Exception ex)
            {
                ConsoleModernWriter.ConsoleIO.Console_ShowErrorDescription(ex);
                Environment.Exit(0);
            }
            return default(IOperationType);
        }
        #endregion
    }
}
