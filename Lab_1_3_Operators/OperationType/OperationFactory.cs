using System;

namespace Lab_1_3_Operators.OperationType
{
    internal static class OperationFactory
    {
        /// <summary>
        /// depending on the user input key (in the main menu)
        /// create an instance of selected operation provider 
        /// and return a reference to it 
        /// </summary>
        /// <param name="enterdKey"></param>
        /// <returns></returns>
        internal static IOperationType GetSelectedOperation(ConsoleKey enterdKey)
        {
            try
            {
                switch (enterdKey)
                {
                    case ConsoleKey.NumPad1:
                        return new PuzzleFarmer.PuzzleFarmerOperation();
                    case ConsoleKey.NumPad2:
                        return new SimpleCalculator.ModernCalculator();
                    case ConsoleKey.NumPad3:
                        return new Factorial.FactorialOperation();
                    case ConsoleKey.NumPad4:
                        return new GuessNumber.GuessNumberOperation();
                    default:
                        return default(IOperationType);
                }
            }
            catch
            {
                return default(IOperationType);
            }
        }
    }
}
