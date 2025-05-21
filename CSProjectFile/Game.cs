using CSProjectFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class Game
{
    private bool _isRunning;
    private readonly Menu<string> _mainMenu;
    private List<Pet> pets = new();

    public Game()
    {
        _isRunning = false;
        var setup = new GameSetup();
        _mainMenu = setup.MainMenuSetup();
    }

    public async Task GameLoop()
    {
        await InitializeAsync();

        _isRunning = true;
        while (_isRunning)
        {
            string userChoice = GetUserInput();
            await ProcessUserChoice(userChoice);
        }

        Console.WriteLine("Thanks for playing!");
    }

    private async Task InitializeAsync()
    {
        Console.WriteLine("Game Started.");
        string path = GetSaveFilePath();

        if (File.Exists(path))
        {
            try
            {
                string json = await File.ReadAllTextAsync(path);
                if (string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Save file is empty. Starting with new pet list.");
                    pets = new List<Pet>();
                    return;
                }
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                };
                pets = JsonSerializer.Deserialize<List<Pet>>(json, options) ?? new List<Pet>();
                Console.WriteLine($"{pets.Count} pet(s) loaded from save file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load pets: {ex.Message}. Starting with empty pet list.");
                pets = new List<Pet>();
            }
        }
        else
        {
            pets = new List<Pet>();
            Console.WriteLine("You Need To Adopt A Pet First...");
        }
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PetGame", "pets.json");
    }

    private string GetUserInput()
    {
        return _mainMenu.ShowAndGetSelection();
    }

    private async Task ProcessUserChoice(string choice)
    {
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

            case "Adopt a Pet":
                await AdoptPetAsync();
                break;

            case "View Pets":
                await ViewPetsAsync();
                break;

            default:
                Console.WriteLine("In Progress...");
                Console.WriteLine("Something Gone Wrong....");
                Console.ReadKey();
                break;
        }

        await Task.Delay(10);
    }

    private async Task AdoptPetAsync()
    {
        Console.Clear();
        Console.WriteLine("Please choose a new pet type:");

        var petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
        for (int i = 0; i < petTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {petTypes[i]}");
        }

        Console.Write("Please Enter Your Choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= petTypes.Count)
        {
            PetType selectedType = petTypes[choice - 1];

            Console.Write("Give your pet a name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Pet name cannot be empty!");
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                return;
            }

            Pet newPet = new Pet(name, selectedType);
            pets.Add(newPet);

            await SavePetsAsync();

            Console.WriteLine($"{name} the {selectedType} has been adopted!");
        }
        else
        {
            Console.WriteLine("Error, Please Enter Real Choice One Of From The List.");
        }

        Console.WriteLine("Press any key to return to main menu");
        Console.ReadKey();
    }

    private async Task ViewPetsAsync()
    {
        Console.Clear();
        var petMenu = new Menu<Pet>(
            "Your Pets",
            pets,
            pet => $"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}, Alive: {(pet.IsAlive ? "Yes" : "No")}"
        );

        Pet selectedPet = petMenu.ShowAndGetSelection();

        if (selectedPet == null && pets.Count > 0)
        {
            Console.WriteLine("Returning to main menu...");
        }
        else if (pets.Count == 0)
        {
            Console.WriteLine("No pets adopted yet!");
        }
        else
        {
            Console.WriteLine($"Selected: {selectedPet.Name} ({selectedPet.Type})");
        }

        Console.WriteLine("Press any key to return to main menu");
        Console.ReadKey();
    }

    private async Task SavePetsAsync()
    {
        try
        {
            string path = GetSaveFilePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };
            string json = JsonSerializer.Serialize(pets, options);
            await File.WriteAllTextAsync(path, json);
            Console.WriteLine("Pets saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save pets: {ex.Message}");
        }
    }
}