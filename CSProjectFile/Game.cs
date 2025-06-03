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
    private PetAsync _petAsync;

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
                var loadedPets = JsonSerializer.Deserialize<List<Pet>>(json, options) ?? new List<Pet>();
                pets.Clear();
                pets.AddRange(loadedPets);
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

        _petAsync = new PetAsync(pets, GetSaveFilePath);
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
                await _petAsync.AdoptPetAsync();
                break;

            case "View Pets":
                await _petAsync.ViewPetsAsync();
                break;

            case "Use Item":
                await _petAsync.UseItemAsync();
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