/// <summary>
/// A generic menu system that can display a list of items and get user selection.
/// </summary>
/// <typeparam name="T">The type of items in the menu</typeparam>
public class Menu<T>
{
    private readonly List<T> _items;
    private readonly string _title;
    private readonly Func<T, string> _displaySelector;

    /// <summary>
    /// Creates a new menu with the specified items and display format.
    /// </summary>
    /// <param name="title">The title to display at the top of the menu</param>
    /// <param name="items">The list of items to display in the menu</param>
    /// <param name="displaySelector">A function that determines how each item is displayed</param>
    public Menu(string title, List<T> items, Func<T, string> displaySelector)
    {
        _title = title;
        _items = items ?? new List<T>();
        _displaySelector = displaySelector ?? (item => item?.ToString() ?? "");
    }

    /// <summary>
    /// Displays the menu and gets the user's selection.
    /// </summary>
    /// <returns>The selected item, or default(T) if the user chooses to go back</returns>
    public T ShowAndGetSelection()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine($"No items available in {_title}. Press any key to continue...");
            Console.ReadKey();
            return default;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== {_title} ===");
            Console.WriteLine();

            // Display menu items with numbers
            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_displaySelector(_items[i])}");
            }

            Console.WriteLine();
            Console.WriteLine("0. Go Back");
            Console.WriteLine();
            Console.Write("Enter selection: ");

            // Get user input
            string input = Console.ReadLine();
            
            // Try to parse the input
            if (int.TryParse(input, out int selection))
            {
                // Check for "Go Back" option
                if (selection == 0)
                    return default; // Return default value of T to indicate backing out
                
                // Check if selection is valid
                if (selection > 0 && selection <= _items.Count)
                {
                    return _items[selection - 1];
                }
            }
            
            Console.WriteLine("Invalid selection. Press any key to try again.");
            Console.ReadKey();
        }
    }
}