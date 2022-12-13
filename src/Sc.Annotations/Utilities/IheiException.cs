using System;

namespace Ihei
{
    public static class IheiException
    {
        public static void PritingExcption(Exception exc)
        {
            Console.WriteLine(
                $"[{DateTime.Now}] [{exc.Source}] SroRobotException: {exc.Message}\n\n" +
                $"{exc.StackTrace}\n\n" +
                $"End ————————————————————————————————————————"
            );
        }
    }
}
