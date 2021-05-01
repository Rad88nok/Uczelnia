using System;

namespace LinqTutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, 1, 1, 10, 4, 5, 4, 5, 1 };
            var t = LinqTasks.Task14();
            //Console.WriteLine(t);
            foreach (var e in t)
            {
                Console.WriteLine(e);
            }
        }
    }
}
