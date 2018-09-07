
namespace Lab_1_3_Operators.OperationType.SimpleCalculator
{
    internal enum KeyType : byte
    {
        /// <summary>
        /// numeric key(1, 2, 3) and decimal separator ('.', ',')
        /// </summary>
        Numeric = 1,  
        /// <summary>
        /// calulation operation (+, -, *, /, pow)
        /// </summary>
        CalcOperation,
        /// <summary>
        /// delete last entered symbol
        /// </summary>
        DeleteLast,
        /// <summary>
        /// to clean the calc buffer and show calc init screen
        /// </summary>
        ClearBufferAndShowInitScreen,
        /// <summary>
        /// when enter key will be pressed but with only one operand
        /// for example: '5' -> '+' -> 'Enter' -> result '10' -> 'Enter' -> result '15' -> and so on
        /// </summary>
        ClearBufferAndShowResult,
        /// <summary>
        /// exit from caltulator
        /// </summary>
        Exit,
        /// <summary>
        /// define a not implemented event handler
        /// </summary>
        Na
    }

    internal enum MathOperation : byte
    {   
        /// <summary>
        /// math opeartion '*'
        /// </summary>
        Multiplication = 1,
        /// <summary>
        /// math opeartion '/'
        /// </summary>
        Divide,
        /// <summary>
        /// math opeartion '+'
        /// </summary>
        Addition,
        /// <summary>
        /// math opeartion '-'
        /// </summary>
        Subtraction,
        /// <summary>
        /// math opeartion '^' or math.pow(a,b)
        /// </summary>
        Exponentiation
    }
}
