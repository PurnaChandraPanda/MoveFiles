using MoveFiles.Component;
using System;
using System.Threading.Tasks;

namespace MoveFiles.Main
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (await Manager.Validate(args))
            {
                await Manager.CopyFiles();
            }

            Console.ReadKey();
        }
    }
}
