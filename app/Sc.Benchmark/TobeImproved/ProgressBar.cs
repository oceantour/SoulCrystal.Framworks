﻿using System;

namespace Siro.Benchmark.Other
{
    /// <summary>
    /// 进度条类型
    /// </summary>
    public enum ProgressBarType
    {
        /// <summary>
        /// 字符
        /// </summary>
        Character,
        /// <summary>
        /// 彩色
        /// </summary>
        Multicolor
    }

    /// <summary> 控制台进度条 </summary>
    /// <example>
    /// var t = new ProgressBar(); t.Dispaly(i);
    /// </example>
    public class ProgressBar
    {
        /// <summary>
        /// 光标的列位置。将从 0 开始从左到右对列进行编号。
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// 光标的行位置。从上到下，从 0 开始为行编号。
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// 进度条宽度。
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 进度条当前值。
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 进度条类型
        /// </summary>
        public ProgressBarType ProgressBarType { get; set; }


        private ConsoleColor colorBack;
        private ConsoleColor colorFore;


        public ProgressBar() : this(Console.CursorLeft, Console.CursorTop)
        {

        }

        public ProgressBar(int left, int top, int width = 100, ProgressBarType progressBarType = ProgressBarType.Multicolor)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.ProgressBarType = progressBarType;

            // 清空显示区域；
            Console.SetCursorPosition(Left, Top);
            for (int i = left; ++i < Console.WindowWidth;) { Console.Write(" "); }

            if (this.ProgressBarType == ProgressBarType.Multicolor)
            {
                // 绘制进度条背景； 
                colorBack = Console.BackgroundColor;
                Console.SetCursorPosition(Left, Top);
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                for (int i = 0; ++i <= width;) { Console.Write(" "); }
                Console.BackgroundColor = colorBack;

                // 更新进度百分比,原理同上.                 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                Console.Write("0%");
                Console.ForegroundColor = colorFore;
            }
            else
            {
                // 绘制进度条背景；    
                Console.SetCursorPosition(left, top);
                Console.Write("[");
                Console.SetCursorPosition(left + width - 1, top);
                Console.Write("]");
            }
        }

        public int Dispaly(int value, string msg = null)
        {
            if (this.Value != value)
            {
                this.Value = value;

                if (this.ProgressBarType == ProgressBarType.Multicolor)
                {
                    // 保存背景色与前景色；
                    colorBack = Console.BackgroundColor;
                    colorFore = Console.ForegroundColor;
                    // 绘制进度条进度                
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(this.Left, this.Top);
                    Console.Write(new string(' ', (int)Math.Round(this.Value / (100.0 / this.Width))));
                    Console.BackgroundColor = colorBack;

                    // 更新进度百分比,原理同上.                 
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                    if (string.IsNullOrWhiteSpace(msg)) { Console.Write("{0}%", this.Value); } else { Console.Write(msg); }
                    Console.ForegroundColor = colorFore;
                }
                else
                {
                    // 绘制进度条进度      
                    Console.SetCursorPosition(this.Left + 1, this.Top);
                    Console.Write(new string('*', (int)Math.Round(this.Value / (100.0 / (this.Width - 2)))));
                    // 显示百分比   
                    Console.SetCursorPosition(this.Left + this.Width + 1, this.Top);
                    if (string.IsNullOrWhiteSpace(msg)) { Console.Write("{0}%", this.Value); } else { Console.Write(msg); }
                }
            }
            return value;
        }
    }
}