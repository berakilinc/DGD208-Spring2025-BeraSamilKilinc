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
        private Pet? selectedPet;

        private readonly Dictionary<string, string[]> categoryAnimations = new()
        {
            {"Food", new[]
            {
            @"  
              ,--./,-.
             / #      \
            |          |
             \        /  
              `._,._,'",
            @"  
             ,--./,-.
            /,-._.--~\
             __}  {
            \`-._,-`-,
             `._,._,'"
            }},
            {"Toy", new[]
            {
            @"
               ,##.                   ,==.
             ,#    #.                 \ o ',
            #        #     _     _     \    \
            #        #    (_)   (_)    /    ; 
             `#    #'                 /   .'  
               `##'                   ""==""",

            @"
               ,##.                ,==.
             ,#    #.              \ o ',
            #        #     _        \    \
            #        #    (_)       /    ; 
             `#    #'              /   .'  
               `##'               ""==""",

            @"
               ,##.          ,==.
             ,#    #.        \ o ',
            #        #        \    \
            #        #        /    ; 
             `#    #'        /   .'  
               `##'         ""==""",
            }},
            {"Sleep", new[]
            {
            @"
                __________________   __________________              
            .-/|                  \ /                  |\-.
            ||||                   |                   ||||
            ||||                   |       ~~*~~       ||||        _ _ _ - - - - _ _ _
            ||||    --==*==--      |                   ||||      /                     \
            ||||                   |                   ||||     |                       |
            ||||                   |                   ||||  -  |        z  z  z        |
            ||||                   |     --==*==--     ||||     |                       |
            ||||                   |                   ||||      \                     /
            ||||                   |                   ||||        - - - _ _ _ _ - - -
            ||||                   |                   ||||
            ||||                   |                   ||||
            ||||__________________ | __________________||||
            ||/===================\|/===================\||
            `--------------------~___~-------------------''",
            @"
                __________________   __________________              
            .-/|                  \ /                  |\-.
            ||||                   |                   ||||
            ||||                   |       ~~*~~       ||||        _ _ _ - - - - _ _ _
            ||||    --==*==--      |                   ||||      /                     \
            ||||                   |                   ||||     |                       |
            ||||                   |                   ||||  -  |     z  z  z  z  z     |
            ||||    *---**--_*=    |     --==*==--     ||||     |                       |
            ||||                   |                   ||||      \                     /
            ||||                   |                   ||||        - - - _ _ _ _ - - -
            ||||                   |    **?*_-===_*    ||||
            ||||                   |                   ||||
            ||||__________________ | __________________||||
            ||/===================\|/===================\||
            `--------------------~___~-------------------''",
            @"
                __________________   __________________              
            .-/|                  \ /                  |\-.
            ||||                   |                   ||||
            ||||                   |       ~~*~~       ||||        _ _ _ - - - - _ _ _
            ||||    --==*==--      |                   ||||      /                     \
            ||||                   |       ~***~       ||||     |                       |
            ||||    *---**--_*=    |                   ||||  -  |  z z z z z z z z z z  |
            ||||                   |     --==*==--     ||||     |                       |
            ||||    *---**--_*=    |                   ||||      \                     /
            ||||    =**?---*?==    |   *--..*---.---   ||||        - - - _ _ _ _ - - -
            ||||    **?---_--*=    |   ----...-***-*   ||||
            ||||                   |                   ||||
            ||||__________________ | __________________||||
            ||/===================\|/===================\||
            `--------------------~___~-------------------''",

            }}
        };

        private readonly Dictionary<PetType, string[]> idleAnimations = new()
        {
            {PetType.Dog, new[]
            {
            @"
                 _=,_
              o_/6 /#\
              \__ |##/
               ='|--\            
                 /   #'-.      ____
                 \#|_   _'-. /        
                  |/ \_( #  |""  
                  C/ ,--___/",

            @"   
                 _=,_
              o_/6 /#\
              \__ |##/
                 ='|--\            
                 /   #'-.      ___ 
                 \#|_   _'-. /     \ 
                  |/ \_( #  |""  
                  C/ ,--___/",

            @"   
                 _=,_
              o_/6 /#\
              \__ |##/
                '|--\            
                 /   #'-.      ___ 
                 \#|_   _'-. /     \ 
                  |/ \_( #  |""  
                  C/ ,--___/"
            }},
            {PetType.Cat, new[]
            {
            @"  
             ,_     _
             |\\_,-~/
             / _  _ |    ,--.
            (  @  @ )   / ,-'
             \  _T_/-._( (
             /         `. \
            |         _  \ |
             \ \ ,  /      |
              || |-_\__   /
             ((_/`(____,-'",

            @"  
             ,_     _
             |\\_,-~/
             / _  _ |    ,--,>_
            (  @  @ )   / ,--> \
             \  _T_/-._( (    \_)
             /         `. \
            |         _  \ |
             \ \ ,  /      |
              || |-_\__   /
             ((_/`(____,-'\\",

            @"  
             ,_     _
             |\\_,-~/
             / _  _ |        ____
            (  @  @ )      / ,-> \ 
             \  _T_/-._   ( (   \_) 
             /         `./  /
            |         _  \ |
             \ \ ,  /      |
              || |-_\__   /
             ((_/`(____,-'\\"
            }}
            ,
            {PetType.Bird, new[]
            {
            @"
              __________
             / ___  ___ \
            / / @ \/ @ \ \
            \ \___/\___/ /\
             \____\/____/||
             /     /\\\\\//
            |     |\\\\\\
             \      \\\\\\
               \______/\\\\
                _||_||_",

            @"
              __________
             / ___  ___ \
            / / _ \/ _ \ \
            \ \___/\___/ /\
             \____\/____/||
             /   \\ /\\\\\//
            |     |\\\\\\
             \      \\\\\\
               \______/\\\\
                _||_||_"
            }},
            {PetType.Fish, new[]
            {
            @"
                  _-_-_  
                 /_,...\
             ,.-'  ,    `-:..-')
            : o ):';       _  {
             `-._ `'__, .-'\`-.)      
                 `\\  \,.-'`",
            @"
                  _____  
                 /_,...\
             ,.-'  ,    `-:..-')
            : 0 ):';       _  {
             `-._ `'__, .-'\`-.)      
                 `\\  \,.-'`",
            @"
                  _-_-_-_ 
                 /_,...  \
             ,.-'  ,     `-:..-')
            : @ ):';        _  {
             `-._ `'__, .-'\`-.)      
                 `\\  \,.-'`",
            }},
            {PetType.Rabbit, new[]
            {
            @"
                         ,\
                         \\\,_
                          \` ,\
                     __,.-"" =__)
                   .""        )
                ,_/   ,    \/\\_
                \_|    )_-\ \_-`
                   `-----` `--`",
            @"
                         __
                         \\\,_
                          \` ,\
                     __,.-"" =__)
                   .""        )
                ,_/   ,    \/\\_
                \_|    )_-\ \_-`
                   `-----` `--`",
            @"
                         __
                         \,\),_
                          \` ,\
                     __,.-"" =__)
                   .""        )
                ,_/   ,    \/\\_
                \_|    )_-\ \_-`
                   `-----` `--`"
            }}
        };

        public PetAsync(List<Pet> pets, Func<string> getSaveFilePath)
        {
            _pets = pets;
            _getSaveFilePath = getSaveFilePath;
            selectedPet = null;
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
            Console.WriteLine("Please choose a pet type:");
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
                Console.WriteLine("Error, Please Enter Valid Choice From The List.");
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
            Pet? newSelectedPet = petMenu.ShowAndGetSelection();
            if (newSelectedPet == null && _pets.Count > 0)
            {
                Console.WriteLine("Returning to main menu...");
            }
            else if (_pets.Count == 0)
            {
                Console.WriteLine("No pets adopted yet!");
            }
            else
            {
                selectedPet = newSelectedPet;
                Console.WriteLine($"Selected: {selectedPet.Name} ({selectedPet.Type})");
            }
            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
        }

        public async Task UseItemAsync()
        {
            Console.Clear();
            if (_pets.Count == 0)
            {
                Console.WriteLine("No pets adopted yet!");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }
            if (selectedPet == null)
            {
                var petMenu = new Menu<Pet>(
                    "Select a Pet to Use an Item On",
                    _pets,
                    pet => $"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}, Alive: {(pet.IsAlive ? "Yes" : "No")}"
                );
                selectedPet = petMenu.ShowAndGetSelection();
            }
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
            var categories = new List<string> { "Food", "Toy", "Sleep", "Back to Main Menu" };
            var categoryMenu = new Menu<string>(
                "Select Item Category",
                categories,
                category => category
            );
            string selectedCategory = await ShowMenuWithAnimation(categoryMenu);
            if (selectedCategory == null || selectedCategory == "Back to Main Menu")
            {
                Console.WriteLine("Returning to main menu...");
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
            Item selectedItem = await ShowMenuWithAnimation(itemMenu);
            if (selectedItem == null)
            {
                Console.WriteLine("No item selected. Returning to main menu...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            if (categoryAnimations.ContainsKey(selectedCategory))
            {
                var animations = categoryAnimations[selectedCategory];
                int totalDurationMs = (int)(selectedItem.Duration * 1000);
                int frameDurationMs = 250;
                int frameIndex = 0;
                int elapsedTime = 0;
                while (elapsedTime < totalDurationMs)
                {
                    Console.Clear();
                    Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name} the {selectedPet.Type}...");
                    Console.WriteLine(animations[frameIndex]);
                    await Task.Delay(frameDurationMs);
                    elapsedTime += frameDurationMs;
                    frameIndex = (frameIndex + 1) % animations.Length;
                }
            }
            else
            {
                Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name} the {selectedPet.Type}...");
                await Task.Delay((int)(selectedItem.Duration * 1000));
            }
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
                    selectedPet.Sleep = Math.Clamp(selectedPet.Sleep - 10, 0, 100);
                    Console.WriteLine($"{selectedPet.Name}'s Fun is now {selectedPet.Fun}");
                    Console.WriteLine($"{selectedPet.Name}'s Sleep is now {selectedPet.Sleep}");
                    break;
            }
            await SavePetsAsync();
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private async Task<T> ShowMenuWithAnimation<T>(Menu<T> menu)
        {
            var cts = new CancellationTokenSource();
            var animationTask = Task.Run(async () =>
            {
                int frameIndex = 0;
                int hungerTick = 0;
                int funTick = 0;
                int sleepTick = 0;
                while (!cts.Token.IsCancellationRequested)
                {
                    Console.Clear();
                    Console.WriteLine(menu.Title);
                    Console.WriteLine();
                    for (int i = 0; i < menu.Items.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {menu.DisplayFunction(menu.Items[i])}");
                    }
                    Console.WriteLine("0. Go Back");
                    Console.WriteLine();
                    Console.WriteLine(idleAnimations[selectedPet.Type][frameIndex]);
                    Console.WriteLine($"Hunger: {selectedPet.Hunger}, Sleep: {selectedPet.Sleep}, Fun: {selectedPet.Fun}");
                    Console.WriteLine();
                    Console.Write("Enter selection: ");
                    frameIndex = (frameIndex + 1) % idleAnimations[selectedPet.Type].Length;
                    hungerTick++;
                    if (hungerTick >= 1)
                    {
                        selectedPet.Hunger = Math.Max(0, selectedPet.Hunger - 1);
                        hungerTick = 0;
                    }
                    funTick++;
                    if (funTick >= 2)
                    {
                        selectedPet.Fun = Math.Max(0, selectedPet.Fun - 1);
                        funTick = 0;
                    }
                    sleepTick++;
                    if (sleepTick >= 4)
                    {
                        selectedPet.Sleep = Math.Max(0, selectedPet.Sleep - 1);
                        sleepTick = 0;
                    }
                    await Task.Delay(500, cts.Token);
                }
            }, cts.Token);
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
            string input = Console.ReadLine();
            cts.Cancel();
            await animationTask.ContinueWith(_ => { }, TaskContinuationOptions.OnlyOnCanceled);
            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= menu.Items.Count)
            {
                return choice == 0 ? default : menu.Items[choice - 1];
            }
            return default;
        }
    }
}