Hello! This is the Intro To C# Assessment of Julia Pickett.

The general flow of the program works like this: ==============================-

> Program.cs: Contains the start screen and main game loop.
> Tools.cs: Has all the important game functions (interacting with objects, inventory management, battling, save file management).
> NPCDialogues.cs: Holds the methods for each respective NPC, using the methods in Tools.cs and the corresponding files in the "NPC Dialogues" folder to allow for displaying of the proper text.
> Locations.cs: Has all the "location" methods to be called in Program.cs.
> Item.cs: exists to serve as a base class for KeyItem.cs and BattleItem.cs, which both hold information about aquireable items.
> Opponent.cs: Holds a constructor for any facable enemy in-game.

Requirements and how they're fulfilled: ===============================-
> A modular approach to implementing logic: Tools.cs and Locations.cs holding methods for Program.cs, rather than them all being in Program.cs.
> Class inheritance: KeyItem and BattleItem being derived from Item.
> Two arrays of user defined data types: battleRunes and keyItems arrays of Item in Tools.cs
> At least two classes containing four or more instance variables: Opponent.cs, as well as the Items.
> At least one instance of an overloaded constructor: The Interrogation method, which has an overload for interaction with people, and one for interaction with items.
> A class implementing user-defined object aggregation: The EntryRemove method in Tools.cs.
> Polymorphism: Any instances of changing variables on items from the battleRunes and keyItems arrays.
> Reading from and writing to a text file: Save file management, NPC Dialogue.
> Use of debugging tools within an IDE to debug and resolve project errors: Given.

Below is a copy of the "Client Side" README file: ============================-
UNWARRANTED Version 0.4

[You are a washed up detective. Now, a case comes along that could put you back in the spotlight. Since you no longer have real affiliation with the city's police force, you can do whatever it takes to get it done. Once you've collected the evidence, you can go to the police and hope they'll hear your claims...]

UNWARRANTED is a fantasy, Console-based Detective-lite RPG where humans live alongside other fantasy races. There is a mix of magic and machine, but despite the wonderous sound of the world, things are rather bleak. Your primary means of advancing are by talking to various citizens and recording what they say to gain more information. There are multiple suspects and multiple endings. One ending might require far more work than the others...

CONTROLS:
Enter the character in brackets [] to execute the respective command.
Example: [T]alk, so you would type "t".
Alt Example: [3] Read 3rd book, so you would type "3" and then hit "enter/return".

Number prompts require a press of the enter key to confirm ifs the number you want, letter inputs automatically enter.

Saving the game can only happen at your home or automatically after picking up items. Clsoing the game without saving first will cause last progress!