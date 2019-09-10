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
        // TODO: some people and items show up on certain times/days. when you're not too lazy, consider throwing in the ones you KNOW you're going to do.

        static MapLocations location = MapLocations.Home;

        private static string libraryBooksPath = "NPC Dialogues/libraryBooks.txt";

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
            // TODO: make some interactions with mari so your house isnt so barren. right now shes using the test interation heh
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nHome is home. It barely fits you and your company, but rent's cheap.\nPapers are scattered on tables and the floor. All your pegboards are filled with old cases.\nYou should clean up soon. Especially since Rat keeps coming over to trash the place even more.");
            Console.WriteLine("\nYou can sleep in your bed for a while, or you can talk to Marideth.\nFrom here you can walk to a few places relatively quickly, including a few of the busier parts of town.");
            Console.WriteLine("\nTRAVEL TO: [L]uxxian Great Library, [T]rain Station (Midday Station), [S]unrise Center, [B]right Sqaure");
            Console.WriteLine("ACTION: [R]est (Save), Talk to [M]arideth");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        Save();
                        okToGo = false;
                        break;
                    case 'l':
                        location = MapLocations.Library;
                        break;
                    case 't':
                        location = MapLocations.TrainB;
                        break;
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    case 'm':
                        TestCharacterInterrogation();
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Center()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nYou step into Sunrise Center, and immediately feel overwhelmed by all the vendors shouting.\nThis center is the most well known shopping district in Orrim, known for outfitting adventurers for their travels.\nToo bad you're not really of the hero type.");
            Console.WriteLine("\nAlong with the appeal of shopping, the center is also an easy way to get to the docks.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [L]uxxian Great Library, [S]hop, Light [A]lley, [D]ocks");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Library()
        {
            // TODO: Time to write b o o k s. The switch is done- just empty. 
            // It'd be reasonable to just use InterrogationObject in here rather than making full convos for them over in NPCDialogues.
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nEasily the biggest library in all of Orrim, here you can find texts on everything there is to know. Probably.\nSelene, the librarian, is sitting behind the desk. If you don't know where to start ask her for help.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [S]unrise Center");
            Console.WriteLine("ACTION: [R]ead book, [T]alk to Selene, Talk to [A]rthur");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'h':
                        location = MapLocations.Home;
                        break;
                    case 's':
                        location = MapLocations.Center;
                        break;
                    case 'r':
                        Console.WriteLine("\nThere are rows and rows of books to pick from. They are organized neatly into catagories.");
                        Console.WriteLine("\n   General Information:\n[0] (General Game Info)       ||  [1] The Citadel of Lux");
                        Console.WriteLine("\n   Biology:\n[2] Races of Orrim: Humans    ||  [3] Races of Orrim: Wildfolk  ||  [4] Races of Orrim: Chimerics" +
                                                    "\n[5] Races of Orrim: Terrians  ||  [6] Races of Orrim: Dragons");
                        Console.WriteLine("\n   Mythology:\n[7] Gods of Orrim: Fuor'Ofor  ||  [8] Gods of Orrim: Seh'novis");
                        Console.WriteLine("\nInput the number of a book to read it. Or enter \"9\" to exit selection.");
                        int.TryParse(Console.ReadLine(), out userInputInt);
                        while (userInputInt != 9)
                        {
                            switch (userInputInt)
                            {
                                case 0:
                                    break;
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                case 5:
                                    break;
                                case 6:
                                    break;
                                case 7:
                                    break;
                                case 8:
                                    break;
                                default:
                                    Console.WriteLine("Invalid Input!");
                                    break;
                            }
                        }
                        break;
                    case 't':
                        Console.WriteLine("debug message: converse with a duck");
                        okToGo = false;
                        break;
                    case 'a':
                        Console.WriteLine("debug message: h e");
                        okToGo = false;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Shop()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nTRAVEL TO: [S]unrise Center");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 's':
                        location = MapLocations.Center;
                        break;                   
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Alley()
        {
            // TODO: make some possible interactions for this area.
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nAn entirely ironic name, Light Alley is probably the darkest place in all of the citadel.\nA place for shifty creatures of all kind to do buissiness behind the back of the law.\nThis is the stomping grounds of one of your informants, Rat. Hes hardly a friend; more of a liability.");
            // Placeholder. If you have knowledge of the Masked Market, you can make your way there.
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
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                            Console.WriteLine("Invalid input!");
                            okToGo = false;
                        }
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Docks()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nLux's only port, and one of only two entrances to the citadel.\nSecurity here is as tight as all other parts of the Wall...\nBut this enforcement only breeds more reason to smuggle in contraband items.");
            Console.WriteLine("\nWalking one way along the water will bring you to The Train Station, while the opposite way is Kog's workshop.\nYou can also make your way down to the Sunrise Center from here- a notable shopping district.");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dusk Station), [S]unrise Center, [K]og's Workshop");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Square()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nBright Square certainly lives up to it's name.\nSpellstone Lanterns of all colors illuminate the area- flashing advertisements for the surrounding establishments.");
            Console.WriteLine("\nLux's destination for entertainment, the surrounding area is filled with theatres, taverns, and casinos.\nStreet preformers also dot the way. A friend of yours, Sprocket, preforms here regularly.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [P]en & Dragon Tavern, [F]eri's Palace");
            Console.WriteLine("ACTION: Talk to [S]procket");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                        // TODO: basically do a conversation instead
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Tavern()
        {
            //TODO: GIVE OUR BOY CERIS SOME DIALOGUE. I'm very glad he and Arthur both had time to be fit in...
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nA run-down bar with the walls showing it's age and a bartender always in a sour mood. It's your second home.\nThe place hasn't been doing so well- being so close to Feri's Palace, an (arguably) better bar.");
            Console.WriteLine("\nThe bar-keep, Ceris, immediately perks up upon seeing you, his favorite regular.\nChat with him if you have the time, or you can exit back to the square.");
            Console.WriteLine("\nTRAVEL TO: [B]right Square, [F]eri's Palace");
            Console.WriteLine("ACTION: Talk to [C]eris");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    case 'f':
                        location = MapLocations.Feri;
                        break;
                    case 'c':
                        Console.WriteLine("debug message: cat chat");
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Kog()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nTRAVEL TO: [D]ocks, [M]asked Market");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'd':
                        location = MapLocations.Docks;
                        break;
                    case 'm':
                        location = MapLocations.Market;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Feri()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nTRAVEL TO: [B]right Square, [P]en & Dragon Tavern");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'b':
                        location = MapLocations.Square;
                        break;
                    case 'p':
                        location = MapLocations.Tavern;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Rutherian()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dawn Station), [H]ideout");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 'h':
                        location = MapLocations.Hideout;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Seren()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nTRAVEL TO: [T]rain Station (Dawn Station), [H]ideout");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 't':
                        location = MapLocations.TrainC;
                        break;
                    case 'h':
                        location = MapLocations.Hideout;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void Market()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nA series of winding alleys all lead to this small square. Patrons and shopkeeps alike all have something on to conceal their identities.\nVendors display dubious items... you're really not sure if you should be buying anything.");
            Console.WriteLine("\nThere is a Beak-Mask here. With your face covered, you can probably get information from them.");
            Console.WriteLine("\nTRAVEL TO: [L]ight Alley, [K]og's Workshop");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'a':
                        location = MapLocations.Alley;
                        break;
                    case 'k':
                        location = MapLocations.Kog;
                        break;
                    case 'x':
                        OpenInventory();
                        okToGo = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        //This area wont follow the same rules as some others- since entering is via a story event and so is exiting. rework this- TODO
        public static void Hideout()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\n[R]utherian's Estate, [S]eren's Lab");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
                switch (userInputChar)
                {
                    case 'r':
                        location = MapLocations.Rutherian;
                        break;
                    case 's':
                        location = MapLocations.Seren;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainA()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Dusk Station. This is the train station by the Docks.\nTaking the train to Midday Station puts you within walking distance of your house.");
            Console.WriteLine("\nThe majority of trains here are cargo trains rather than passenger trains.\nGoods delivered via ship are boarded and taken to all parts of the citadel from here.");
            Console.WriteLine("\nTRAVEL TO: [D]ocks, [T]ake Train to Midday Station");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainB()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Midday Station. This is the train station closest to your house.\nTaking the train to Dusk Station will bring you to the docks, while the Dawn Station train takes you to the Inner Ring.");
            Console.WriteLine("\nTRAVEL TO: [H]ome, [T]ake Train to Dusk Station, [R]ide Train to Dawn Station");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                        Console.WriteLine("Invalid input!");
                        okToGo = false;
                        break;
                }
            }
        }

        public static void TrainC()
        {
            Console.WriteLine("=======================================================================================================================");
            Console.WriteLine("\nYou arrive at Dawn Station. This is the train station in the Inner Ring.\nTaking the train to Midday Station will bring you back close to your house.");
            Console.WriteLine("\nThe Inner Ring is home to the aristocrats of Lux, seperated from the Outter Ring by a giant wall.\nFor common folk, theres practically nothing to do here but sight-see and talk to the locals.");
            Console.WriteLine("\nTRAVEL TO: [R]utherian's Estate, [S]eren's Lab, [T]ake Train to Midday Station");
            Console.WriteLine("ACTION: ");
            Console.WriteLine("[X] Open Inventory");
            bool okToGo = false;
            while (!okToGo)
            {
                okToGo = true;
                userInputChar = Char.ToLower(Console.ReadKey(true).KeyChar);
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
                        Console.WriteLine("Invalid input!");
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
