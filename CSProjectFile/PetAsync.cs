using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CSProjectFile
{
    internal class PetAsync
    {
        private readonly List<Pet> _pets;
        private readonly Func<string> _getSaveFilePath;

        public PetAsync(List<Pet> pets, Func<string> getSaveFilePath)
        {
            _pets = pets;
            _getSaveFilePath = getSaveFilePath;
        }

        public async Task SavePetsAsync()
        {
            try
            {
                string path = _getSaveFilePath();
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                };
                string json = JsonSerializer.Serialize(_pets, options);
                await File.WriteAllTextAsync(path, json);
                Console.WriteLine("Pets saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save pets: {ex.Message}");
            }
        }

        public async Task AdoptPetAsync()
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
                _pets.Add(newPet);

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

        public async Task ViewPetsAsync()
        {
            Console.Clear();
            var petMenu = new Menu<Pet>(
                "Your Pets",
                _pets,
                pet => $"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}, Alive: {(pet.IsAlive ? "Yes" : "No")}"
            );

            Pet selectedPet = petMenu.ShowAndGetSelection();

            if (selectedPet == null && _pets.Count > 0)
            {
                Console.WriteLine("Returning to main menu...");
            }
            else if (_pets.Count == 0)
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
        public async Task UseItemAsync()
        {
            Console.Clear();
            var petMenu = new Menu<Pet>(
                "Select a Pet to Use an Item On",
                _pets,
                pet => $"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}, Alive: {(pet.IsAlive ? "Yes" : "No")}"
            );
            Pet selectedPet = petMenu.ShowAndGetSelection();

            if (selectedPet == null)
            {
                Console.WriteLine("No pet selected. Returning to main menu...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            if (!selectedPet.IsAlive)
            {
                Console.WriteLine($"{selectedPet.Name} is not alive and cannot use items!");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            var categories = new List<string> { "Food", "Toy", "Sleep" };
            var categoryMenu = new Menu<string>(
                "Select Item Category",
                categories,
                category => category
            );
            string selectedCategory = categoryMenu.ShowAndGetSelection();

            if (selectedCategory == null)
            {
                Console.WriteLine("No category selected. Returning to main menu...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var compatibleItems = ItemDatabase.AllItems
                .Where(item =>
                    (selectedCategory == "Food" && item.Type == ItemType.Food) ||
                    (selectedCategory == "Toy" && item.Type == ItemType.Toy && item.AffectedStat == PetStat.Fun) ||
                    (selectedCategory == "Sleep" && item.Type == ItemType.Toy && item.AffectedStat == PetStat.Sleep))
                .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
                .ToList();

            if (compatibleItems.Count == 0)
            {
                Console.WriteLine($"No {selectedCategory} items available for {selectedPet.Name} the {selectedPet.Type}!");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            var itemMenu = new Menu<Item>(
                $"Select a {selectedCategory} for {selectedPet.Name}",
                compatibleItems,
                item => $"{item.Name} (Affects: {item.AffectedStat}, Amount: {item.EffectAmount}, Duration: {item.Duration}s)"
            );
            Item selectedItem = itemMenu.ShowAndGetSelection();

            if (selectedItem == null)
            {
                Console.WriteLine("No item selected. Returning to main menu...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name} the {selectedPet.Type}...");
            await Task.Delay((int)(selectedItem.Duration * 1000));

            switch (selectedItem.AffectedStat)
            {
                case PetStat.Hunger:
                    selectedPet.Hunger = Math.Clamp(selectedPet.Hunger + selectedItem.EffectAmount, 0, 100);
                    Console.WriteLine($"{selectedPet.Name}'s Hunger is now {selectedPet.Hunger}");
                    break;
                case PetStat.Sleep:
                    selectedPet.Sleep = Math.Clamp(selectedPet.Sleep + selectedItem.EffectAmount, 0, 100);
                    Console.WriteLine($"{selectedPet.Name}'s Sleep is now {selectedPet.Sleep}");

                    if (selectedPet.Sleep == 100)
                    {
                        Console.WriteLine($"{selectedPet.Name} is so rested that they fall asleep!");
                        selectedPet.Hunger = Math.Clamp(selectedPet.Hunger - 30, 0, 100);
                        Console.WriteLine($"{selectedPet.Name} feels hungry after sleeping! Hunger is now {selectedPet.Hunger}");
                    }
                    break;
                case PetStat.Fun:
                    selectedPet.Fun = Math.Clamp(selectedPet.Fun + selectedItem.EffectAmount, 0, 100);
                    Console.WriteLine($"{selectedPet.Name}'s Fun is now {selectedPet.Fun}");
                    break;
            }

            await SavePetsAsync();

            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
    }
}
