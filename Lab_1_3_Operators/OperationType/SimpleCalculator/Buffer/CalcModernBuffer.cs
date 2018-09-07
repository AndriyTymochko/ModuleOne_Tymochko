using System;
using System.Linq;
using System.Text;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator.Buffer
{
    internal class CalcModernBuffer : SimpleBuffer
    {
        /// <summary>
        /// to store a local buffer data (result of operations, last math operation (+, -...) etc)
        /// </summary>
        protected LastOp OpProvider;
        /// <summary>
        /// default value in int user input string at calc UI
        /// </summary>
        protected const string DefaultValueForInputString = "0";
        
        /// <summary>
        /// default value of double buffer width
        /// </summary>
        private const int _width = 80;
        /// <summary>
        /// default value of double buffer height
        /// </summary>
        private const int _height = 30;

        /// <summary>
        /// to store the number of user input data row in calc UI
        /// </summary>
        protected readonly int RowInputData;
        /// <summary>
        /// to store the number of previous calc operations row in calc UI
        /// </summary>
        protected readonly int RowBufferOperation;

        /// <summary>
        /// to store a string with empty space in calc UI for all width
        /// </summary>
        protected readonly string EmptySpaceCalcUI;
        /// <summary>
        /// to store a string with undeline space in calc UI for all width
        /// </summary>
        protected readonly string UndelineSpaceCalcUI;

        /// <summary>
        /// to store the init posistion of cursor for user input in double buffer 
        /// </summary>
        private readonly byte _colInputDataInitPosition;

        /// <summary>
        /// to store the calc UI template 
        /// </summary>
        private readonly string _calcTemplateUI;
        /// <summary>
        /// to store the keyboard UI template
        /// </summary>
        private readonly string _keyboardTemplateUI;
        /// <summary>
        /// to store the input data and buffer of last operations UI template
        /// </summary>
        private readonly string _inputDataTemplateUI;

        #region Constructors
        public CalcModernBuffer() : this(_width, _height, _width, _height) { }
        public CalcModernBuffer(int width, int height, int wWidth, int wHeight) : base(width, height, wWidth, wHeight)
        {
            OpProvider = new LastOp();

            RowInputData = 2;
            RowBufferOperation = 1;

            _colInputDataInitPosition = 20;

            EmptySpaceCalcUI = new string(' ', 19);
            UndelineSpaceCalcUI = new string('_', 19);

            _inputDataTemplateUI = string.Format("|__________________0|{0}", "\n");

            _keyboardTemplateUI = string.Format(
                                        "|  7    8    9   /  |{0}"
                                      + "|  4    5    6   *  |{0}"
                                      + "|  1    2    3   -  |{0}"
                                      + "|  0    ,    e   +  |{0}", "\n");

            _calcTemplateUI = string.Format(
                                    " {2} {0}"
                                  + "|{1}|{0}"
                                  + "{3}"
                                  + "|-------------------|{0}"
                                  + "{4}"
                                  + " -------------------{0}{0}{0}{0}"
                                  + "Press 'c' to clear the calc or 'q' for exit to the main menu... {0}", "\n", EmptySpaceCalcUI, UndelineSpaceCalcUI, _inputDataTemplateUI, _keyboardTemplateUI);

            Console.CursorVisible = false;
            Console.SetWindowSize(_width, _height);
            Console.SetBufferSize(_width, _height);
        }
        #endregion


        #region Methods 

        #region Cleaning 
        /// <summary>
        /// Clear the buffer and reset all depended value to value by default
        /// </summary>
        public virtual void ClearBufferAndShowInitScreen()
        {
            Clear();
            WriteTextDividedByNewLine(_calcTemplateUI);
            Print();

            OpProvider = new LastOp();
            Console.SetCursorPosition(_colInputDataInitPosition, RowInputData);
        }
        #endregion

        /// <summary>
        /// Get the row from the double buffer
        /// </summary>
        /// <param name="rowPosition"></param>
        /// <returns></returns>
        public virtual StringBuilder GetRow(int rowPosition)
        {
            StringBuilder line = new StringBuilder();
            for (int i = (rowPosition * width); i < ((rowPosition * width + width)); i++)
            {
                if (rowPosition > windowHeight - 1)
                    throw new System.ArgumentOutOfRangeException();

                line.Append(buf[i].Char.UnicodeChar);
            }
            return line;
        }

        /// <summary>
        /// Write text to buffer that is splitted by "\n" (by new line)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="rowSeparator"></param>
        public virtual void WriteTextDividedByNewLine(string text, string rowSeparator = "\n")
        {
            int row = 0;
            string[] splitedByLine = text.Split(new string[] { rowSeparator }, StringSplitOptions.None);
            splitedByLine
                .ToList()
                .ForEach(line =>
                {
                    Draw(line, 0, row);
                    row++;
                });
        }

        #endregion
    }   
}
