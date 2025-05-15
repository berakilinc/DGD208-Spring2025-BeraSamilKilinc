using System;
using System.Threading.Tasks;

namespace CSProjectFile
{
    class Program
    {
        static async Task Main(string[]args) // using Async Task, because Game.cs has using async Task
        {
            var game = new Game();
            await game.GameLoop();
        }
    }
}