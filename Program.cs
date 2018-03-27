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

            // Count the number of occurrences in each dice roll.
            foreach (var grp in diceRoll.GroupBy(i => i))
            {
                Console.WriteLine("{0} : {1}", grp.Key, grp.Count());
            }

            // Ask the user which dice they would like to keep.
            Console.WriteLine("Do you want to reroll? Y/N");

            if (Console.ReadLine().ToLower() == "y")
            {
                Console.WriteLine("You put yes.");
            }
            else if (Console.ReadLine().ToLower() == "n")
            {
                Console.WriteLine("You put no.");
            }
            else
            {
                Console.WriteLine("You put neither yes nor no.");
            }

            Console.ReadLine();
        }
    }
}
