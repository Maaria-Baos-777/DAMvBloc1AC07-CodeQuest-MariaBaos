using System;
using System.Text;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        //Menu
        const string MenuTitle = "===== MAIN MENU - CODEQUEST =====";
        const string MenuOption1 = "1. Train your wizard";
        const string MenuOption2 = "2. Check the dungeon";
        const string MenuOption3 = "3. Loot the mine";
        const string MenuOptionExit = "0. Exit game";
        const string MenuPrompt = "Choose an option (1-3) - (0) to exit: ";

        const string ErrorNotNumber = "Error: you must type a number, not text.";
        const string ErrorTooBig = "Error: that number is too big.";
        const string ErrorRange = "Error: the option must be between 0 and 3.";
        const string ErrorNoInput = "Error: no input received.";

        //Chapter1
        const string NameMage = "Enter you mage name: ";
        const string DaysText = "⌛ Day {0} -> {1}, you have now meditated for {2} hours, and your power is currently {3} points!";
        const string LevelUp = "⭐ -> {0} has level up to level {1}";

        const string MessageRepeatExamination = "You are retaking it in the second examination session";
        const string MessageConfuseWandWithSpoon = "You still confuse the wand with spoon";
        const string MessageInvocatorMagicBreezes = "You are a Summoner of Magical Breezes";
        const string MessageSummonDragons = "Wow! You can summon dragons without burning down the laboratory!";
        const string MessageReachedRankMaster = "You have reached the rank of Master of the Arcane!";

        const string RankRaodenElantri = "You obtain rank Raoden the Elantri";
        const string RankZynBuguejat = "You obtain rank Zyn the Buguejat";
        const string RankArkaNullPointer = "You obtain rank Arka Nullpointer";
        const string RankElarionBrases = "You obtain rank Elarion of the Brases";
        const string RankItbWizard = "You obtain rank ITB-Wizard the grey";

        //Chapter2
        const string MessageLevelTwo = "Your wizard {0} must obtain the treasure hidden within the dungeon of the dragon. Where every door is protected by a digital access code";
        const string MessageDoors = "They are 3 doors, each one have a code random between 1 and 5. You have 3 try for each door";
        const string MessageDoorAndTry = "You are in door {0}, you have {1} attemps left";
        const string MessageFailDoor = "The dragon 🐲 has detected your presence and has kicked you from the server!";
        const string MessageUnlokDoor = "The dragon 🐲 respects you. You have unlocked the next level!";
        const string MessageUnlockLastLevel = "You have unlocked the final level. You can keep the dragon's 🐲 treasure 💎!";
        const string MessageNumber = "Enter the door number: ";

        //Chapter3

        // Valor centinella: -1 NO és una opció vàlida.
        // El necessitem perquè el compilador no garanteix que 'option' s'assigni
        // dins del try (pot saltar una excepció abans d'arribar a l'assignació).

        Random random = new Random();
        int option = -1, days = 5, hours, dailyPower = 0, totalPower = 0, level = 1, levelBefore, numberUserDoor, doorCode, attemps = 3, doors = 1;
        bool isValid, doorFound = false, doorFail = false;
        string userMageName = "";

        do
        {
            isValid = false; // A cada volta suposem que l'entrada NO és vàlida

            Console.WriteLine(MenuTitle);
            Console.WriteLine(MenuOption1);
            Console.WriteLine(MenuOption2);
            Console.WriteLine(MenuOption3);
            Console.WriteLine(MenuOptionExit);
            Console.Write(MenuPrompt);

            try
            {
                // int.Parse pot llançar: FormatException, OverflowException
                // o ArgumentNullException (si ReadLine retorna null amb Ctrl+Z / Ctrl+D)
                option = int.Parse(Console.ReadLine());

                // Validació de rang: si no es compleix, LLANÇEM nosaltres l'excepció
                if (option < 0 || option > 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(option), ErrorRange);
                }

                // Si arribem aquí no ha saltat cap excepció: entrada vàlida
                isValid = true;
                Console.WriteLine($"You chose: {option}");
            }
            catch (FormatException)              // Text que no és un número: "abc", "3.5", ""
            {
                Console.WriteLine(ErrorNotNumber);
            }
            catch (OverflowException)            // Número que no cap en un int: "99999999999"
            {
                Console.WriteLine(ErrorTooBig);
            }
            catch (ArgumentNullException)        // ReadLine ha retornat null
            {
                Console.WriteLine(ErrorNoInput);
            }
            catch (ArgumentOutOfRangeException)  // La que hem llançat nosaltres pel rang
            {
                Console.WriteLine(ErrorRange);
            }
            finally
            {
                // S'executa SEMPRE: hi hagi error o no
                Console.WriteLine("----------------------------");
            }

            // Repetim si l'entrada ha estat invàlida O si no han demanat sortir.
            // Comprovem primer !isValid perquè, amb un error, 'option' pot
            // valer 0 d'una volta anterior.

            switch(option)
            {
                case 1:
                    Console.Write(NameMage);
                    userMageName = Console.ReadLine();
                    for(int i = 1; i <= days; i++) //days
                    {
                        hours = random.Next(1, 25);
                        dailyPower = random.Next(1, 11);
                        totalPower += dailyPower;
                        levelBefore = level;
                        level = 1 + (totalPower / 10);
                        Console.WriteLine(DaysText, i, userMageName, hours, totalPower);
                        if(level != levelBefore)
                        {
                            Console.WriteLine(LevelUp, userMageName, level);
                        } 
                    }
                    if(totalPower == 20)
                    {
                        Console.WriteLine(MessageRepeatExamination);
                        Console.WriteLine(RankRaodenElantri);
                    }
                    else if(totalPower >= 20 && totalPower < 30)
                    {
                        Console.WriteLine(MessageConfuseWandWithSpoon);
                        Console.WriteLine(RankZynBuguejat);
                    }
                    else if(totalPower >= 30 && totalPower < 35)
                    {
                        Console.WriteLine(MessageInvocatorMagicBreezes);
                        Console.WriteLine(RankArkaNullPointer);
                    }
                    else if(totalPower >= 35 && totalPower < 40)
                    {
                        Console.WriteLine(MessageSummonDragons);
                        Console.WriteLine(RankElarionBrases);
                    }
                    else
                    {
                        Console.WriteLine(MessageReachedRankMaster);
                        Console.WriteLine(RankItbWizard);
                    }
                    break;
                case 2:
                    Console.WriteLine(MessageLevelTwo, userMageName);
                    Console.WriteLine(MessageDoors);
                    while(doors <= 3 && !doorFail)
                    {
                        doorFound = false;
                        while (attemps > 0 && !doorFound)
                        {
                            doorCode = random.Next(1, 6);
                            Console.WriteLine(MessageDoorAndTry, doors, attemps);
                            Console.Write(MessageNumber);
                            numberUserDoor = Convert.ToInt32(Console.ReadLine());
                            if (numberUserDoor == doorCode)
                            {
                                doorFound = true;
                                Console.WriteLine(MessageUnlokDoor);
                            }
                            else
                            {
                                attemps--;
                            }
                            
                        }
                        if (!doorFound)
                        {
                            Console.WriteLine(MessageFailDoor);
                            doorFail = true;
                        }
                        else
                        {
                            doors++;
                        }
                    }
                    if (!doorFail && doors > 3)
                    {
                        Console.WriteLine(MessageUnlockLastLevel);
                    }
                    break;
            }


        } while (!isValid || option != 0);
    }
}