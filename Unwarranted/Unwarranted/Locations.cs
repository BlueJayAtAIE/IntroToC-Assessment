using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnwarrantedTools.Tools;
using static UnwarrantedTools.NPCDialogues;


namespace UnwarrantedTools
{
    public static class Locations
    {
        static MapLocations location = MapLocations.Home;

        public static MapLocations GetLocation()
        {
            return location;
        }

        public static void SetLocation(MapLocations newLocation)
        {
            location = newLocation;
        }

        public static void Home()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nHome is home. It barely fits you and your company, but rent's cheap.\nPapers are scattered on tables and the floor. All your pegboards are filled with old cases.\nYou should clean up soon. Especially since Rat keeps coming over to trash the place even more.");
            Console.WriteLine("\nYou can sleep in your bed for a while, or you can talk to Marideth.\nFrom here you can walk to a few places relatively quickly, including a few of the busier parts of town.");
            Console.WriteLine("\nTRAVEL TO: [L]uxxian Great Library, [T]rain Station (Midday Station), [S]unrise Center, [B]right Sqaure");
            Console.Write("ACTION: [R]est (Save), Talk to [M]arideth");
            if (!keyItems[0].playerObtained) Console.Write(", [I]nspect Table");
            Console.WriteLine("\n[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        switch (rng.Next(4))
                        {
                            case 0:
                            default:
                                Console.WriteLine("\nYou sleep for a few restless hours. You wake up completely unprepared to face your day.");
                                break;
                            case 1:
                                Console.WriteLine("\nYou over-sleep and someone else finishes the case, stealing your glory. At least that happens in your dream.");
                                break;
                            case 2:
                                Console.WriteLine("\nYou have a dream about being anywhere else. It was okay.");
                                break;
                            case 3:
                                Console.WriteLine("\nYou actually got some quality sleep this time- and still only wasted 4 hours.");
                                break;
                        }
                        for (int i = 0; i < 8; i++)
                        {
                            TimeAdvance();
                        }
                        Save();
                        Console.WriteLine("(Your game has been saved.)");
                        TimeDisplay();
                        okToGo = false;
                        break;
                    case 'l':
                        MaridethWorry();
                        location = MapLocations.Library;
                        break;
                    case 't':
                        MaridethWorry();
                        location = MapLocations.TrainB;
                        break;
                    case 's':
                        MaridethWorry();
                        location = MapLocations.Center;
                        break;
                    case 'b':
                        MaridethWorry();
                        location = MapLocations.Square;
                        break;
                    case 'm':
                        MaridethInterrogation();
                        TimeAdvance();
                        break;
                    case 'i':
                        if (!keyItems[0].playerObtained)
                        {
                            ItemInspect(0);
                            TimeAdvance();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }

                void MaridethWorry()
                {
                    Console.Write("\n\n[");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Marideth");
                    Console.ResetColor();
                    Console.WriteLine("]:");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Oh- and Jack. Don't forget to equipt Spellstones before you get into a fight.\nIf you get hurt I don't get the money you owe me. Well- I probably will still- but I won't feel good about it.");
                    Console.ResetColor();
                    Console.WriteLine("[Any Key] Continue...");
                    Console.ReadKey();
                }
            }
        }

        public static void Center()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou step into Sunrise Center, and immediately feel overwhelmed by all the vendors shouting.\nThis center is the most well known shopping district in Orrim, known for outfitting adventurers for their travels.\nToo bad you're not really of the hero type.");
            Console.WriteLine("\nAlong with the appeal of shopping, the center is also an easy way to get to the docks.");
            if (timeDays == 3) Console.WriteLine("\nThere are posters everywhere for Feri's Birthday Extravaganza. Maybe you should check it out...");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [L]uxxian Great Library, [S]hop, Light [A]lley, [D]ocks");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 'l':
                        location = MapLocations.Library;
                        break;
                    case 's':
                        location = MapLocations.Shop;
                        break;
                    case 'a':
                        location = MapLocations.Alley;
                        break;
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Library()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nEasily the biggest library in all of Orrim, here you can find texts on everything there is to know. Probably.\nSelene, the librarian, is sitting behind the desk. If you don't know where to start ask her for help.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [S]unrise Center");
            Console.WriteLine("ACTION: [R]ead book, [T]alk to Selene, Talk to [A]rthur");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'r':
                        while (userInputInt != 9)
                        {
                        Console.Clear();
                        Console.WriteLine("There are rows and rows of books to pick from. They are organized neatly into catagories.");
                        Console.WriteLine("\n   General Information:\n[0] (General Game Info)       ||  [1] The Citadel of Lux");
                        Console.WriteLine("\n   Biology:\n[2] Races of Orrim: Humans    ||  [3] Races of Orrim: Wildfolk  ||  [4] Races of Orrim: Chimerics" +
                                                    "\n[5] Races of Orrim: Terrians  ||  [6] Races of Orrim: Dragons");
                        Console.WriteLine("\n   Mythology:\n[7] Gods of Orrim: Fuor'Ofor  ||  [8] Gods of Orrim: Seh'novis");
                        Console.WriteLine("\nOr enter [9] to exit selection.");
                            Console.Write("\nInput command> ");
                            int.TryParse(Console.ReadLine(), out userInputInt);
                            switch (userInputInt)
                            {
                                case 0:
                                    Interrogation(libraryBooksPath, 1, 9, false, "Library Book", 999);
                                    break;
                                case 1:
                                    Interrogation(libraryBooksPath, 12, 5, false, "Library Book", 999);
                                    break;
                                case 2:
                                    Interrogation(libraryBooksPath, 19, 4, false, "Library Book", 999);
                                    break;
                                case 3:
                                    Interrogation(libraryBooksPath, 25, 6, false, "Library Book", 999);
                                    break;
                                case 4:
                                    Interrogation(libraryBooksPath, 33, 8, false, "Library Book", 999);
                                    break;
                                case 5:
                                    Interrogation(libraryBooksPath, 43, 7, false, "Library Book", 999);
                                    break;
                                case 6:
                                    Interrogation(libraryBooksPath, 52, 6, false, "Library Book", 999);
                                    break;
                                case 7:
                                    Interrogation(libraryBooksPath, 60, 6, false, "Library Book", 999);
                                    break;
                                case 8:
                                    Interrogation(libraryBooksPath, 68, 6, false, "Library Book", 999);
                                    break;
                                case 9:
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid input!");
                                    break;
                            }
                        }
                        break;
                    case 't':
                        SeleneInterrogation();
                        TimeAdvance();
                        break;
                    case 'a':
                        ArthurInterrogation();
                        TimeAdvance();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Shop()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou enter one of the few shops in the center of use to you. The only thing they sell here are Spellstones.\nSpellstones are used to augment your attacks, by either changing their strength or giving them additonal effects.");
            Console.WriteLine("\nTRAVEL TO: [S]unrise Center");
            Console.WriteLine("ACTION: [L]ook over wares");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'l':
                        Console.WriteLine("\nYou look over all the Spellstones offered in the shop.\nEnter the number in brackets to purchase, or [9] to exit selection.\n");
                        Console.WriteLine($"\t[0] Smite Spellstone - Attack Rune. Deals a massive {((BattleItem)battleRunes[7]).attackDamage} damage. \t\t\t\tCost: 200 Muns");
                        Console.WriteLine($"\t[1] Drain Spellstone - Attack/Heal Rune. Deals {((BattleItem)battleRunes[6]).attackDamage} damage. Heals user 2 damage \t\tCost: 70 Muns");
                        Console.WriteLine($"\t[2] Shock Spellstone - Attack/Status Rune. Deals {((BattleItem)battleRunes[4]).attackDamage} damage. Inflicts {((BattleItem)battleRunes[4]).attackEffect}. \t\tCost: 60 Muns");
                        Console.WriteLine($"\t[3] Restore Spellstone - Heal Rune. Heals user for {((BattleItem)battleRunes[5]).attackDamage} damage. \t\t\t\tCost: 50 Muns");
                        Console.WriteLine($"\t[4] Flame Spellstone - Status Rune. Inflicts {((BattleItem)battleRunes[2]).attackEffect}, which deals {((BattleItem)battleRunes[2]).attackDamage} damage over 2 turns. \tCost: 40 Muns");
                        while (userInputInt != 9)
                        {
                            Console.WriteLine($"\nCurrent funds: {money} Muns");
                            Console.Write("Input command> ");
                            int.TryParse(Console.ReadLine(), out userInputInt);
                            switch (userInputInt)
                            {
                                case 0:
                                    BuyItem(7, 200, battleRunes[7].name);
                                    break;
                                case 1:
                                    BuyItem(6, 70, battleRunes[6].name);
                                    break;
                                case 2:
                                    BuyItem(4, 60, battleRunes[4].name);
                                    break;
                                case 3:
                                    BuyItem(5, 50, battleRunes[5].name);
                                    break;
                                case 4:
                                    BuyItem(2, 40, battleRunes[2].name);
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid input!");
                                    break;
                            }
                        }
                        TimeAdvance();
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Alley()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nAn entirely ironic name, Light Alley is probably the darkest place in all of the citadel.\nA place for shifty creatures of all kind to do buissiness behind the back of the law.\nThis is the stomping grounds of one of your informants, Rat. Hes hardly a friend; more of a liability.");
            if (!discoveredMarket)
            {
                Console.WriteLine("\nFrom here, you've only one direction to go- back to Sunrise Center.");
                Console.WriteLine("\nTRAVEL TO: [S]unrise Center");
            }
            if (discoveredMarket)
            {
                Console.WriteLine("\nFrom here, you can go back to Sunrise Center, or if you feel like walking into your death, stroll the Masked Market.");
                Console.WriteLine("\nTRAVEL TO: [S]unrise Center, [M]asked Market");
            }
            Console.Write("ACTION: Talk to [R]at");
            if (!stoleFreeMoney)
            {
                Console.Write(", Talk to [G]entleman");
            }
            Console.WriteLine("\n[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'm':
                        if (discoveredMarket)
                        {
                            location = MapLocations.Market;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'r':
                        RatInterrogation();
                        break;
                    case 'g':
                        if (!stoleFreeMoney)
                        {
                            Console.WriteLine("\n\nHeh heh... heya \"inspector\" Jackylln. You don't think I forgot huh?");
                            Console.WriteLine("That I forgot how you put away one of my buddies before retiring?- After your weak ass couldn't handle consequenses.");
                            Console.WriteLine("So lemme teach you a thing or two about karma!");
                            Console.WriteLine("[Any Key] Continue...");
                            Console.ReadKey(true);
                            Battle("Thug");
                            if (HP > 0)
                            {
                                // If the player dies, run this bit.
                                // This is part of a template for future battles.
                            }
                            HP = 25;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Docks()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nLux's only port, and one of only two entrances to the citadel.\nSecurity here is as tight as all other parts of the Wall...\nBut this enforcement only breeds more reason to smuggle in contraband items.");
            Console.WriteLine("\nWalking one way along the water will bring you to The Train Station, while the opposite way is Kog's workshop.\nYou can also make your way down to the Sunrise Center from here- a notable shopping district.");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dusk Station), [S]unrise Center, [K]og's Workshop");
            Console.WriteLine("ACTION: Talk to [W]orker");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainA;
                        break;
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    case 'w':
                        DocksInterrogation();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Square()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nBright Square certainly lives up to it's name.\nSpellstone Lanterns of all colors illuminate the area- flashing advertisements for the surrounding establishments.");
            Console.WriteLine("\nLux's destination for entertainment, the surrounding area is filled with theatres, taverns, and casinos.\nStreet preformers also dot the way. A friend of yours, Sprocket, preforms here regularly.");
            if (timeDays == 3) Console.WriteLine("\nThere are posters everywhere for Feri's Birthday Extravaganza. Maybe you should check it out...");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [P]en & Dragon Tavern, [F]eri's Palace");
            Console.WriteLine("ACTION: Talk to [S]procket, Talk to [A]rthur");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 'p':
                        location = MapLocations.Tavern;
                        break;
                    case 'f':
                        location = MapLocations.Feri;
                        break;
                    case 's':
                        SprocketInterrogation();
                        break;
                    case 'a':
                        ArthurInterrogation();
                        TimeAdvance();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Tavern()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nA run-down bar with the walls showing it's age and a bartender always in a sour mood. It's your second home.\nThe place hasn't been doing so well- being so close to Feri's Palace, an (arguably) better bar.");
            Console.WriteLine("\nThe bar-keep, Ceris, immediately perks up upon seeing you, his favorite regular.\nChat with him if you have the time, or you can exit back to the square.");
            Console.WriteLine("\nTRAVEL TO: [B]right Square, [F]eri's Palace");
            Console.WriteLine("ACTION: Talk to [C]eris");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    case 'f':
                        location = MapLocations.Feri;
                        break;
                    case 'c':
                        CerisInterrogation();
                        TimeAdvance();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Kog()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou step into Kog's Workshop and are greeted by all kinda of humming machines and strange looking contraptions.\nContainers of Etheris and piles of metal objects are strewn about haphazardly.\nYou are yelled at to not touch anything.");
            Console.WriteLine("\nA Terrian known as Kog owns this workshop- as evident from the title of the place.\nSeeing as the cardo shippment company is so close, he may know something about the missing Chimeric.");
            Console.WriteLine("\nTRAVEL TO: [D]ocks, [M]asked Market");
            Console.WriteLine("ACTION: Talk to [K]og");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 'm':
                        location = MapLocations.Market;
                        break;
                    case 'k':
                        KogInterrogation();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Feri()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nAs you enter past the bouncer all your senses are assulted at once- you haven't been in a casino this busy in forever.");
            Console.WriteLine("\nWhile so much is going on, its easy to get overwhelemed, but you're only here to see Feri. Don't get side-tracked.\nYou see an elaborately clothed Wildfolk, who you assume muct be Feri.");
            if (timeDays == 3) Console.WriteLine("\nThere are Beak-Masks running around everywhere! One espacially nicely dressed one is up on stage with Feri.");
            Console.WriteLine("\nTRAVEL TO: [B]right Square, [P]en & Dragon Tavern");
            if (timeDays < 3) Console.Write("ACTION: Talk to [F]eri");
            if (!keyItems[5].playerObtained && !(timeDays == 3)) Console.Write(", [I]nspect Area");
            if (timeDays == 3) Console.WriteLine("Battle the [S]pectre!");
            Console.WriteLine("\n[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    case 'p':
                        location = MapLocations.Tavern;
                        break;
                    case 'f':
                        if (timeDays < 3)
                        {
                            FeriInterrogation();
                            TimeAdvance();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'i':
                        if (!keyItems[5].playerObtained && !(timeDays == 3))
                        {
                            ItemInspect(5);
                            TimeAdvance();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 's':
                        if (timeDays == 3)
                        {
                            Console.WriteLine("\n\nThe mysterious figure starts to speak.");
                            Console.WriteLine("\"Hello Inspector... I see you've come to witness our ultimatum.\"");
                            Console.WriteLine("\"Taking Feri into our hands- such a popular figure to the public--\"");
                            Console.WriteLine("Feri takes the oppertunity of the Spectre monologuing to pull a dagger out of her boot and stab them.");
                            Console.WriteLine("\"AUGH!- REALLY? I can't even enjoy this? Brothers and Sisters- take this heathen away.\"");
                            Console.WriteLine("\"I'm going to deal with the inspector...\"");
                            Console.WriteLine("[Any Key] Continue...");
                            Console.ReadKey();
                            location = MapLocations.Home;
                            Battle("Spectre1");
                            if (HP <= 0)
                            {
                                HP = 25;
                                break;
                            }
                            Console.WriteLine("\"Tch... This... He's stronger than I anticipated...\"");
                            Console.WriteLine("\"Heh- it was nice to see you though Jack... I hope I never have to again! Hahahahaa\"");
                            Console.WriteLine("You're surrounded on all sides by goons. You black out, then you wake up at home.\nA day has past...");
                            timeDays = 4;
                            Console.WriteLine("[Any Key] Continue...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Rutherian()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nOne of the most elaborate buildings in Lux, Rutherian is a wealthy Chimeric who climbed his way to aristocracy.\nHe now invests money into smaller ventures- maybe if you present your case to him he'll support you.");
            Console.WriteLine("\nYou're guided from the outer gates to the main parlor, where several other people wait to try to stake a claim.\nAfter a bit of waiting, you're allowed to go in and speak.");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dawn Station)");
            Console.WriteLine("ACTION: Talk to [R]ubinia, [S]peak with Rutherian");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 'r':
                        RubiniaInterrogation();
                        break;
                    case 's':
                        RutherianInterrogation();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Seren()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou walk into the lab, but you're only allowed to enter the lobby before being stopped.\nA receptionist asks for the purpose of your visit. You flash your expired badge long enough for her to think it's real.");
            Console.WriteLine("\nThe receptionist presses a button, and moments later a Dragonian steps through a door.\nHe introduces himself as Seren, head scientist here at the lab.");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dawn Station)");
            Console.Write("ACTION: Talk to [S]eren");
            if (!keyItems[3].playerObtained) Console.Write(", [I]nspect Area");
            Console.Write("\n[X] Open Inventory");

            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 's':
                        SerenInterrogation();
                        break;
                    case 'i':
                        if (!keyItems[3].playerObtained)
                        {
                            ItemInspect(3);
                            TimeAdvance();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Market()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nA series of winding alleys all lead to this small square.\nPatrons and shopkeeps alike all have something on to conceal their identities.\nVendors display dubious items... you're really not sure if you should be buying anything.");
            Console.WriteLine("\nThere is a Beak-Mask here. With your face covered, you can probably get information from them.");
            Console.WriteLine("\nTRAVEL TO: [L]ight Alley, [K]og's Workshop");
            Console.WriteLine("ACTION: Talk to [B]eak-Mask");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'a':
                        location = MapLocations.Alley;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    case 'b':
                        BeakInterrogation();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        //This area wont follow the same rules as some others- since entering is via a story event and so is exiting. rework this- TODO
        public static void Hideout()
        {
            ConsoleColor textColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou see someone frantically towards you- it's Rutherian.");
            Console.ForegroundColor = textColor;
            Console.WriteLine("Jack... Please... I come to you not as a sponsor but as a friend...\nThey took Rubi... They took her and I don't know what to do...\nI know where they went- I can show you- just please hurry.");
            Console.ResetColor();
            Console.WriteLine("\nRutherian leads you to the Inner Ring, and then to a door there you had never seen before.\nThe two of you work together to pry it open.");
            Console.WriteLine("Below you lie the stairs to a twisted set of tunnels. You have no idea which way to even go...");
            Console.WriteLine("[E]ast, [W]est, [S]outh, [N]orth");
            int walking = 0;
            while (walking < 5)
            {
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'e':
                    case 'w':
                    case 's':
                    case 'n':
                        switch (rng.Next(1,4))
                        {
                            case 1:
                                Console.WriteLine("\n\nYou hear nothing but the sound of your own footsteps...");
                                break;
                            case 2:
                                Console.WriteLine("\n\nOdd bird statues addorn the hall, jewel eyes looking into your soul.");
                                break;
                            case 3:
                                Console.WriteLine("\n\nRutherian makes a small stuttering sound behind you. You look behind you- he's okay.");
                                break;
                            case 4:
                                Console.WriteLine("\n\nYou hear sounds from somewhere- they stop a ssoon as you try to make them out.");
                                break;
                        }
                        walking++;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        break;
                }
            }
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou get to the end of a hallway. Infront of you lies a strange labritory.\nTwisted machines and vials of dubious liquid are spread throughout the room...");
            Console.WriteLine("But most notable is a Chimeric strapped down to a table. It's the Chimeric from the missing poster.\nThey have something covering their mouth but ehy're still yelling to get your attention.");
            Console.WriteLine("You rip the restraints away from them so they can speak.");
            Console.WriteLine("\"What're you doing? You madman! You never should have come here-\"\n\"This is exactly what HE wanted...\"");
            Console.WriteLine("The Chimeric points an accusitory finger at Rutherian.\nFollowing just after that- Rubinia- unharmed- walks into the room of her own volition.");

            textColor = ConsoleColor.Red;
            Console.ForegroundColor = textColor;
            Console.WriteLine("\nOops! Um- Oh Ruthy! Jack! You've come to saaaave me!");
            textColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = textColor;
            Console.WriteLine("\nOh... Oh! Rubi! I'm SO glad we finally found you! And unscathed...\nHm- it's not as fun to keep the act up now that we've gone this far. Let's start over maybe? I was having so much fun...\nDon't look at me like that Inspector- we had SO many good times didn't we?\nWeren't you having fun thinking you were about to solve this case?");
            Console.WriteLine("The case of all the missing Chimerics? Of the messiah who was capturing them to make them perfect?\nI sure was- in fact it's the reason I decided to bring you here and out myself- because, and let's be real:\nWho would listen to you anyway?");
            Console.WriteLine("Poor little Jackllyn- a man who just crumbled and fell apart after losing his wife; doing his job.\nNo... Unlike the cases you could have had with the others, you have no physical proof on me.\nSo who's more trustworthy... a nobleman who's been supporting this city for years?\n...Or a lazy drunk who fled when the city needed him most?");
            Console.ResetColor();
            Console.WriteLine("\n[X] Open Inventory");
            Console.WriteLine("[Any Key] Continue to Battle...");

            while (true)
            {
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                if (userInputChar == 'x')
                {
                    OpenInventory();
                }
                else
                {
                    break;
                }
            }

            bool continueSequence = true;
            Battle("Rutherian");
            if (HP <= 0)
            {
                HP = 25;
                continueSequence = false;
            }

            if (continueSequence)
            {
                Console.Clear();
                Console.WriteLine("\n=======================================================================================================================");
                Console.ForegroundColor = textColor;
                Console.WriteLine("\nAuck, why don't you JUST GIVE UP? GIVE UP INSPECTOR.\nSURROUND HIM. I THOUGHT I'D END THIS MYSELF BUT I SEE NOW WHY THAT'S DIFFICULT.\nSQUIRM OUT OF THIS ONE YOU INSOLENT WORM.");
                Console.ResetColor();
                Console.WriteLine("\nYou are surrounded by Beak-Masks. The world goes black.\nYou wake up in the street near Rutherian's Estate.");
                Console.WriteLine("\nYou hear voices in the back of your mind...");

                // Encouragement gives you over Max HP
                textColor = ConsoleColor.Blue;
                Console.ForegroundColor = textColor;
                Console.WriteLine("\nJack... Jack You can do this.");
                textColor = ConsoleColor.Cyan;
                Console.ForegroundColor = textColor;
                Console.WriteLine("Jack, I know you've got this!!~");
                textColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = textColor;
                Console.WriteLine("Come on Jacky boy I believe in you...");
                textColor = ConsoleColor.Yellow;
                Console.ForegroundColor = textColor;
                Console.WriteLine("You've done so good this far inspector!! You can do a little more!");
                textColor = ConsoleColor.Green;
                Console.ForegroundColor = textColor;
                Console.WriteLine("We're all rootin' for ya.");
                textColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = textColor;
                Console.WriteLine("Montenegro... this is the future I saw for you. Fight. Defend your truths.");
                Console.ResetColor();
                HP = 35;
                
                Console.WriteLine("\nYou're ready to take on Rutherian again.");

                Console.WriteLine("\n[X] Open Inventory");
                Console.WriteLine("[Any Key] Continue to Battle...");

                while (true)
                {
                    userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                    if (userInputChar == 'x')
                    {
                        OpenInventory();
                    }
                    else
                    {
                        break;
                    }
                }

                Console.Clear();
                Console.WriteLine("\n=======================================================================================================================");
                textColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = textColor;
                Console.WriteLine("\nOh? You're back.\nInspector, I'm so sorry... last time I just exploded at you- that was very... unwarranted.\nYou have to understand though- I'm doing this not for personal gain, but the goodness of my heart.\nYou know Chimerics used to be the dominant race of Orrim? Before the humans hunted us like animals?\nI'm just making a way for us to fight back against them... X-Class Chimerics- they're our perfect and most pure form.");
                Console.WriteLine("Thanks to machines from Kog, and samples from Seren, our own labs were able to figure out a way to transform normal chimerics...\nInto their more beautiful and pure counterparts.\nAnd finding test subjects was so much easier after we bribed Feri to kick Chimerics out of her establishment!\nSo many of my poor kin on the streets... during the night... so dangerous! So profitable~\nAnd now I think it's about time I ascend myself- I'll allow myself to be one of the first few to taste perfection...");
                Console.WriteLine("AND I WILL LEAD ALL OTHERS TO IT.");
                Console.ResetColor();

                Console.WriteLine("\n[Any Key] Continue to Battle...");
                Console.ReadKey();

                Battle("RutherianX");
                if (HP <= 0)
                {
                    HP = 25;
                    continueSequence = false;
                }

                if (continueSequence)
                {
                    Console.Clear();
                    endingsObtained[3] = true;
                    Console.WriteLine("\n=======================================================================================================================");
                    textColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = textColor;
                    Console.WriteLine("\nNo... NO. HOW IS THIS EVEN POSSIBLE. I AM A GOD.\nYOU ARE WEAK. YOU ARE BELow me...");
                    Console.ResetColor();
                    Console.WriteLine("Rutherian collapses...");
                    Console.WriteLine("\n\nThe police come to the scene shortly. Rutherian is taken into custody, but the other Beak-Masks vanished.");
                    Console.WriteLine("The chief comes up to you and tells you someone needs to lead the search for them. You accept.");
                    Console.WriteLine("Rutherian is going to be locked up for a long time. But you still can't help but wonder...");
                    Console.WriteLine("\nWhat comes next?\n");
                    Console.WriteLine("-= THE END =-");
                    Save();
                    GameEnd();
                }
            }
        }

        public static void TrainA()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Dusk Station. This is the train station by the Docks.\nTaking the train to Midday Station puts you within walking distance of your house.");
            Console.WriteLine("\nThe majority of trains here are cargo trains rather than passenger trains.\nGoods delivered via ship are boarded and taken to all parts of the citadel from here.");
            Console.WriteLine("\nTRAVEL TO: [D]ocks, [T]ake Train to Midday Station");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainB()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Midday Station. This is the train station closest to your house.\nTaking the train to Dusk Station will bring you to the docks, while the Dawn Station train takes you to the Inner Ring.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [T]ake Train to Dusk Station, [R]ide Train to Dawn Station");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 't':
                        location = MapLocations.TrainA;
                        break;
                    case 'r':
                        location = MapLocations.TrainC;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainC()
        {
            Console.WriteLine("\n=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Dawn Station. This is the train station in the Inner Ring.\nTaking the train to Midday Station will bring you back close to your house.");
            Console.WriteLine("\nThe Inner Ring is home to the aristocrats of Lux, seperated from the Outer Ring by a giant wall.\nFor common folk, theres practically nothing to do here but sight-see and talk to the locals.");
            Console.WriteLine("\nTRAVEL TO: [R]utherian's Estate, [S]eren's Lab, [T]ake Train to Midday Station");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                Console.Write("\nInput command> ");
                userInputChar = Char.ToLower(Console.ReadKey().KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        location = MapLocations.Rutherian;
                        break;
                    case 's':
                        location = MapLocations.Seren;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input!");
                        okToGo = false;
                        break;
                }
            }
        }
    }
}

//Map connections:
//TrainA -------- Docks ------------------------ Kog
//  |               |                           / 
//  | Library -- Center/Shop -- Alley -- [Market] 
//  |             /                               
//TrainB --- Home -------------- Square/Sproket
//  |                                 |  \
//  |                            Tavern - Feri
//  |
//TrainC ---- Rutherian/Seren ---- [Hideout]
