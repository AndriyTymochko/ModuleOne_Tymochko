using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace Lab_1_3_Operators.OperationType.SimpleCalculator.Buffer
{
    /*
     * Copyright [2012] [Jeff R Baker]

     * Licensed under the Apache License, Version 2.0 (the "License");
     * you may not use this file except in compliance with the License.
     * You may obtain a copy of the License at    
     * 
     *          http://www.apache.org/licenses/LICENSE-2.0
     * 
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     * 
     * v 1.2.0
     */
    ///<summary>
    ///This class allows for a double buffer in Visual C# cmd promt. 
    ///The buffer is persistent between frames.
    ///</summary>
    internal class SimpleBuffer
    {
        protected int width;
        protected int height;
        protected int windowWidth;
        protected int windowHeight;
        protected SafeFileHandle h;
        protected CharInfo[] buf;
        protected SmallRect rect;

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            private short X;
            private short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct CharUnion
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CharInfo
        {
            [FieldOffset(0)]
            public CharUnion Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SmallRect
        {
            private short Left;
            private short Top;
            private short Right;
            private short Bottom;
            public void setDrawCord(short l, short t)
            {
                Left = l;
                Top = t;
            }
            public void setWindowSize(short r, short b)
            {
                Right = r;
                Bottom = b;
            }
        }

        /// <summary>
        /// Consctructor class for the buffer. Pass in the width and height you want the buffer to be.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public SimpleBuffer(int Width, int Height, int wWidth, int wHeight) // Create and fill in a multideminsional list with blank spaces.
        {
            if (Width > wWidth || Height > wHeight)
            {
                throw new System.ArgumentException("The buffer width and height can not be greater than the window width and height.");
            }
            h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            width = Width;
            height = Height;
            windowWidth = wWidth;
            windowHeight = wHeight;
            buf = new CharInfo[width * height];
            rect = new SmallRect();
            rect.setDrawCord(0, 0);
            rect.setWindowSize((short)windowWidth, (short)windowHeight);
            //Clear();
        }
        /// <summary>
        /// This method draws any text to the buffer with given color.
        /// To chance the color, pass in a value above 0. (0 being black text, 15 being white text).
        /// Put in the starting width and height you want the input string to be.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="attribute"></param>
        public virtual void Draw(string str, int Width, int Height, ConsoleColor cc = ConsoleColor.White) //Draws the image to the buffer
        {
            if (Width > windowWidth - 1 || Height > windowHeight - 1)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            if (str != null)
            {
                Char[] temp = str.ToCharArray();
                int tc = 0;
                foreach (Char le in temp)
                {
                    buf[(Width + tc) + (Height * width)].Char.AsciiChar = (byte)(int)le; //Height * width is to get to the correct spot (since this array is not two dimensions).
                    if (cc != 0)
                        buf[(Width + tc) + (Height * width)].Attributes = (short)cc;
                    tc++;
                }
            }


        }
        /// <summary>
        /// Prints the buffer to the screen.
        /// </summary>
        public virtual void Print() //Paint the image
        {
            if (!h.IsInvalid)
            {
                bool b = WriteConsoleOutput(h, buf, new Coord((short)width, (short)height), new Coord((short)0, (short)0), ref rect);
            }
        }
        /// <summary>
        /// Clears the buffer and resets all character values back to 32, and attribute values to 1.
        /// </summary>
        public virtual void Clear()
        {
            Console.Clear();
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].Attributes = 1;
                buf[i].Char.AsciiChar = 32;
            }
        }
    }
}
