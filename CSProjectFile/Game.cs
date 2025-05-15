using CSProjectFile;

public class Game
{
    private bool _isRunning;
    private readonly Menu<string> _mainMenu;

    public Game()
    {
        _isRunning = false;
        var setup = new GameSetup();
        _mainMenu = setup.MainMenuSetup();
    }
    
    public async Task GameLoop()
    {
        // Initialize the game
        Initialize();
        
        // Main game loop
        _isRunning = true;
        while (_isRunning)
        {
            // Display menu and get player input
            string userChoice = GetUserInput();
            
            // Process the player's choice
            await ProcessUserChoice(userChoice);
        }
        
        Console.WriteLine("Thanks for playing!");
    }
    
    private void Initialize()
    {
        // Use this to initialize the game
        Console.WriteLine("Game Started.");
    }
    
    private string GetUserInput()
    {
        // Use this to display appropriate menu and get user inputs
        return _mainMenu.ShowAndGetSelection();
    }
    
    private async Task ProcessUserChoice(string choice)
    {
        // Use this to process any choice user makes
        // Set _isRunning = false to exit the game
        switch (choice)
        {
            case "Exit":
                Console.WriteLine("- - -");
                Console.WriteLine("Exiting The Game.");
                Console.WriteLine("Saying goodbye to your pet... This might take a while.");
                Console.WriteLine("- - -");
                await Task.Delay(4000);
                _isRunning = false;
                break;

            case "Credit":
                Console.WriteLine("- - - - - - - - - - - - - - - - - - -");
                Console.WriteLine("-        This Game Was Made By      -");
                Console.WriteLine("-    Full Name: Bera Þamil Kýlýnç   -");
                Console.WriteLine("-         Number: 2305041045        -");
                Console.WriteLine("- - - - - - - - - - - - - - - - - - -");
                Console.ReadKey();
                break;

            default:
                Console.WriteLine("In Progress...");
                Console.WriteLine("Something Gone Wrong....");
                Console.ReadKey();
                break;
        }

        await Task.Delay(10);
    }
}