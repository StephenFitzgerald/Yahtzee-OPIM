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
        static Random random = new Random();
        static List<int> diceRoll = new List<int>();
        static int dice;

        // Method that creates the initial dice roll.
        static void RollDice()
        {
            for (int i = 0; i < 5; i++)
            {
                dice = random.Next(1, 7);
                diceRoll.Add(dice);
            }
        }

        // Main method.
        static void Main(string[] args)
        {
            // Roll the dice.
            RollDice();

            // Display the results of the dice roll.
            for (int i = 1; i < diceRoll.Count + 1; i++)
            {
                Console.WriteLine("Dice #" + i + ": " + diceRoll[i-1].ToString());
            }

            // Display the sum of the dice roll.
            Console.WriteLine("Sum: " + diceRoll.Sum());

            // Count the number of occurrences in each dice roll.
            foreach (var grp in diceRoll.GroupBy(i => i))
            {
                Console.WriteLine("{0} : {1}", grp.Key, grp.Count());
            }

            // Ask the user which dice they would like to keep.
            bool isValid = true;

            // Do this until the user enters a valid input (either "Y" or "N").
            do
            {
                Console.WriteLine("Do you want to reroll? Y/N");
                string s = Console.ReadLine();

                // If the user enters YES...
                if (s.ToLower() == "y")
                {
                    Console.WriteLine("You put yes.");
                    isValid = false;

                    // TODO
                    // Code that prompts user where to enter score.
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
                    Console.WriteLine("You put neither yes nor no.");
                }
            }
            while (isValid);

            // Ask the user which dice he or she wants to keep.
            Console.WriteLine("Which dice #s do you want to keep? Separate using spaces (ex. 2 3 5)");
            isValid = true;

            do
            {
                string s = Console.ReadLine();

                // Check for any invalid input. Ex. inputting any symbols or letters.
                try
                {
                    // Split the inputted string into each individual dice value.
                    string[] diceKept = s.Split(null);
                    isValid = false;
                }

                // If there are any errors...
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (isValid);

            Console.ReadLine();
        }
    }
}
