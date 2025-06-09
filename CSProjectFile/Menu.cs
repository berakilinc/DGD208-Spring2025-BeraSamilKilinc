using System;
using System.Collections.Generic;

namespace CSProjectFile
{
    public class Menu<T>
    {
        public string Title { get; }
        public List<T> Items { get; }
        public Func<T, string> DisplayFunction { get; }

        public Menu(string title, List<T> items, Func<T, string> displayFunction)
        {
            Title = title;
            Items = items;
            DisplayFunction = displayFunction;
        }

        public T ShowAndGetSelection()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Title);
                Console.WriteLine();
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {DisplayFunction(Items[i])}");
                }
                Console.WriteLine("0. Go Back");
                Console.WriteLine();
                Console.Write("Enter selection: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= Items.Count)
                {
                    return choice == 0 ? default : Items[choice - 1];
                }
                Console.WriteLine("Invalid selection! Press any key to try again...");
                Console.ReadKey();
            }
        }
    }
}