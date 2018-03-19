# Yahtzee-OPIM

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //RANDOM # GENERATOR
            Random random = new Random();

            //generate dice 1 value
            int dice1 = random.Next(1, 7); // creates a number between 1 and 6
            Console.Write("\nValue of Dice 1: " + dice1);
            //generate dice 2 value
            int dice2 = random.Next(1, 7); // creates a number between 1 and 6
            Console.Write("\nValue of Dice 2: " + dice2);
            //generate dice 3 value           
            int dice3 = random.Next(1, 7); // creates a number between 1 and 6
            Console.Write("\nValue of Dice 3: " + dice3);
            //generate dice 4 value
            int dice4 = random.Next(1, 7); // creates a number between 1 and 6
            Console.Write("\nValue of Dice 4: " + dice4);
            //generate dice 5 value
            int dice5 = random.Next(1, 7); // creates a number between 1 and 6
            Console.Write("\nValue of Dice 5: " + dice5);
            
            //Showing totals for all categories AFTER ROLL 1

            //Asking which dice the user would like to keep

            //Loop process

            Console.ReadLine();
        }
    }
}

