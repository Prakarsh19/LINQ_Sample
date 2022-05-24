namespace Task.Helpers
{
    using System;
    using System.Threading;

    namespace ResizerApp.Helpers
    {
        internal static class MessageHelper
        {
            private static readonly object ConsoleWriterLock = new object();

            public static void ErrorMessage(Exception e, string text)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + text + "\nMessage: {0}", e.Message);

                if (!String.IsNullOrEmpty(e.StackTrace))
                {
                    Console.WriteLine("StackTrace: {0}", e.StackTrace);
                }
                Console.ResetColor();
            }

            public static void InfoMessage(string text, ConsoleColor bgColor = ConsoleColor.Black,
                ConsoleColor fgColor = ConsoleColor.Cyan)
            {
                lock (ConsoleWriterLock)
                {
                    Console.BackgroundColor = bgColor;
                    Console.ForegroundColor = fgColor;
                    Console.WriteLine(text);
                    Console.ResetColor();
                }
            }

            public static void SuccessMessage(string additionalText = "", ConsoleColor bgColor = ConsoleColor.Green,
                ConsoleColor fgColor = ConsoleColor.Black)
            {
                lock (ConsoleWriterLock)
                {
                    Console.BackgroundColor = bgColor;
                    Console.ForegroundColor = fgColor;
                    Console.WriteLine($"{additionalText}: Success!");
                    Console.ResetColor();
                }
            }
        }
    }
}
