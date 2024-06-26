﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Utils
{
    internal class ColoredConsole
    {
        private static object lockObj = new object();

        public static void Write(string text)
        {
            lock (lockObj)
            {
                string[] segments = text.Split('`');

                ConsoleColor currentColor = Console.ForegroundColor;

                for (int i = 0; i < segments.Length; i++)
                {
                    string segment = segments[i];

                    if (string.IsNullOrWhiteSpace(segment))
                        continue;

                    if (Enum.TryParse(segment, out ConsoleColor color) && i % 2 != 0)
                    {
                        Console.ForegroundColor = color;
                    }
                    else
                    {
                        Console.Write(segment);
                    }
                }

                Console.ForegroundColor = currentColor;
            }
        }

        public static void WriteLine(string text)
        {
            Write(text + '\n');
        }
    }
}
