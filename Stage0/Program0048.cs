using System;

namespace Stage0
{
    partial class Program
    {
        static void main(string[] srgs)
        {
            Welcome0048();
            Welcome2942();
            Console.ReadKey();
        }
        private static void Welcome0048()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
        static partial void Welcome2942();
    }
}