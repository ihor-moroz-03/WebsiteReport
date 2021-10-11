using System;

namespace WebsiteReport
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(new WebsiteWeekReport(System.IO.File.ReadAllLines(@"C:\Users\ihorm\source\repos\WebsiteReport\data.txt")).GetReport());
            Console.Read();
        }
    }
}
