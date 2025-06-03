public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        // Foods
        new Item { 
            Name = "Kibble", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Dog }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 15,
            Duration = 2.5f 
        },
        new Item { 
            Name = "Premium Dog Food", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Dog }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 30,
            Duration = 3.0f  
        },
        new Item { 
            Name = "Cat Food", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Cat }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 15,
            Duration = 2.0f
        },
        new Item { 
            Name = "Tuna Treat", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Cat }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 25,
            Duration = 1.5f 
        },
        new Item { 
            Name = "Bird Seed", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Bird }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 10,
            Duration = 1.0f
        },
        new Item { 
            Name = "Fruit Mix", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Bird }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 20,
            Duration = 2.0f
        },
        new Item { 
            Name = "Fish Flakes", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Fish }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 10,
            Duration = 0.5f 
        },
        new Item { 
            Name = "Premium Fish Pellets", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Fish }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 20,
            Duration = 1.0f
        },
        new Item { 
            Name = "Carrots", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Rabbit }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 15,
            Duration = 3.0f 
        },
        new Item { 
            Name = "Leafy Greens", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Rabbit }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 25,
            Duration = 4.0f 
        },
        
        // Universal Foods
        new Item { 
            Name = "Vitamin Treat", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat, PetType.Rabbit }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 10,
            Duration = 1.0f  
        },
        new Item { 
            Name = "Gourmet Dinner", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 40,
            Duration = 5.0f  
        },
        
        // Toys
        new Item { 
            Name = "Tennis Ball", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Dog }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 20,
            Duration = 4.0f  
        },
        new Item { 
            Name = "Squeaky Toy", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Dog }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 2.5f
        },
        new Item { 
            Name = "String Toy", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Cat }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 20,
            Duration = 3.0f  
        },
        new Item { 
            Name = "Toy Mouse", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Cat }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 2.0f
        },
        new Item { 
            Name = "Swing", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Bird }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 3.0f 
        },
        new Item { 
            Name = "Bell", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Bird }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 10,
            Duration = 1.5f
        },
        new Item { 
            Name = "Bubbler", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Fish }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 10,
            Duration = 2.0f 
        },
        new Item { 
            Name = "Water Plant", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Fish }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 1.5f
        },
        new Item { 
            Name = "Chew Toy", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Rabbit }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 3.5f
        },
        new Item { 
            Name = "Tunnel", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Rabbit }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 20,
            Duration = 4.0f
        },
        
        // Universal Toys
        new Item { 
            Name = "Ball", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat, PetType.Rabbit }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 10,
            Duration = 2.0f
        },
        
        // Sleep Items
        new Item { 
            Name = "Comfy Bed", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 30,
            Duration = 6.0f
        },
        new Item { 
            Name = "Pet Blanket", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Dog, PetType.Cat, PetType.Rabbit }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 20,
            Duration = 4.0f
        },
        new Item { 
            Name = "Perch", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Bird }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 25,
            Duration = 3.0f
        },
        new Item { 
            Name = "Cave Decoration", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Fish }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 15,
            Duration = 2.0f
        },
        new Item { 
            Name = "Hideaway", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.Rabbit }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 25,
            Duration = 5.0f
        }
    };
}