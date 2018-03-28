using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    class Program
    {
        // Variables that can be used globally.
        static Random random = new Random();                // Random number generator.
        static List<int> diceRoll = new List<int>();        // A list of the dice rolls from a round.
        static List<String> scorecard = new List<string>(); // A list of scores for the game.
        static int dice;                                    // Value of a Dice
        static int numberOfDice = 5;                        // Can be changed if the rules of Yahtzee change the number of dice.
        static int currentRound;                            // The current round of the game.
        static int numberOfRounds = 13;                     // Number of rounds in a game of Yahtzee.
        static int currentRoll = 1;                         // The current roll of the round.
        static int numberOfRolls = 3;                       // Number of rolls allowed during a round.

        // Method that creates the initial dice roll. Only executed once.
        static void RollDice()
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                dice = random.Next(1, 7);
                diceRoll.Add(dice);
            }
        }

        // Method that rerolls the dice.
        static void RerollDice()
        {
            for (int i = 0; i < diceRoll.Count(); i++)
            {
                // If the dice was not kept, reroll it.
                if (diceRoll[i] == 0)
                    diceRoll[i] = random.Next(1, 7);
            }

            // Increment the current roll.
            currentRoll++;
        }

        // Method that enters the score.
        static void EnterScore()
        {
            
        }

        // Main method.
        static void Main(string[] args)
        {
            // Roll the dice.
            RollDice();

            // If the game is not over...
            for (currentRound = 0; currentRound < numberOfRounds; numberOfRounds++)
            {
                // Display the number of the dice rolls.
                string roll = "Roll #" + currentRoll;

                // If it is the last roll, display a different message.
                if (currentRoll == numberOfRolls - 1)
                    Console.WriteLine(roll);
                else
                    Console.WriteLine(roll + " (last roll!)");

                // Display the results of the dice roll.
                for (int i = 1; i < diceRoll.Count + 1; i++)
                {
                    Console.WriteLine("Dice #" + i + ": " + diceRoll[i - 1].ToString());
                }

                // Display the sum of the dice roll.
                Console.WriteLine("Sum: " + diceRoll.Sum());

                // Count the number of occurrences in each dice roll.
                foreach (var v in diceRoll.GroupBy(i => i))
                {
                    Console.WriteLine("{0} : {1}", v.Key, v.Count());
                }

                // Ask the user which dice they would like to keep.
                bool isValid = true;

                // Do this until the user enters a valid input (either "Y" or "N").
                do
                {
                    // If the user still has the option to reroll...
                    if (currentRoll < numberOfRolls)
                    {
                        // Asks the user if he or she would like to use a reroll.
                        Console.WriteLine("Do you want to reroll? Y/N");

                        // User-inputted string.
                        string s = Console.ReadLine();

                        // If the user enters YES...
                        if (s.ToLower() == "y")
                        {
                            Console.WriteLine("You put yes.");
                            isValid = false;
                        }

                        // If the user enters NO...
                        else if (s.ToLower() == "n")
                        {
                            Console.WriteLine("You put no.");
                            isValid = false;

                            // TODO
                            // Code asking reroll.
                        }

                        // If the user enters an invalid input...
                        else
                        {
                            Console.WriteLine("Invalid input. Please type Y or N.");
                            continue;
                        }
                    }
                }
                while (isValid);

                // Do this until the user enters a valid input.
                do
                {
                    // Ask the user which dice he or she wants to keep.
                    Console.WriteLine("Which dice #s do you want to keep? Separate using spaces (ex. 2 3 5)");
                    isValid = true;

                    // User-inputted string.
                    string s = Console.ReadLine();

                    // Check for any invalid input. Ex. inputting any symbols or letters.
                    try
                    {
                        // Split the inputted string into each individual dice value.
                        List<int> diceKept = s.Split(null).Select(Int32.Parse).ToList();

                        // Sort the list so numbers go from smallest to largest.
                        diceKept.Sort();

                        // Checks to see if the user entered the same number twice.
                        bool isUnique = diceKept.Distinct().Count() == diceKept.Count();

                        if (!isUnique)
                        {
                            Console.WriteLine("Invalid input. Dice number was repeated.");
                            continue;
                        }

                        // Checks to see if a number less than 1 or greater than 6 was inputted.
                        foreach (int i in diceKept)
                        {
                            if (i < 1 || i > 6)
                            {
                                Console.WriteLine("Invalid input. Please choose a number between 1 and 6.");
                                continue;
                            }
                        }

                        // Temporary variable. Used for iterating.
                        int j = 0;

                        for (int i = 0; i < diceRoll.Count; i++)
                        {
                            // If the index of the dice roll is NOT equal to the dice the user wants to keep...
                            if (i != (diceKept[j] - 1))
                            {
                                // Reset the dice roll to 0.
                                diceRoll[i] = 0;
                            }

                            // Check the next dice as long as it's not the last element in the list.
                            else if (j != diceKept.Count() - 1)
                                j++;
                        }

                        Console.WriteLine(diceRoll[0].ToString() + diceRoll[1].ToString() + diceRoll[2].ToString() + diceRoll[3].ToString() + diceRoll[4].ToString());

                        isValid = false;
                    }

                    // If there are any errors...
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input." + e.Message);
                        continue;
                    }
                }
                while (isValid);

                // If the user has not reached the maximum number of allowed rolls, then reroll.
                if (currentRoll < numberOfRolls)
                    RerollDice();

                Console.ReadLine();
            }
        }
    }
}