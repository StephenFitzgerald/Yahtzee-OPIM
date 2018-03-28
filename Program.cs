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
        static Random random = new Random();                    // Random number generator.
        static List<int> diceRoll = new List<int>();            // A list of the dice rolls from a round.
        static List<Score> scoreCard = new List<Score>();       // A list of scores for the game.
        static List<Score> finalScoreCard;                      // A list of the final scores for the game.
        static int dice;                                        // Value of a Dice
        static int numberOfDice = 5;                            // Can be changed if the rules of Yahtzee change the number of dice.
        static int currentRound;                                // The current round of the game.
        static int numberOfRounds = 13;                         // Number of rounds in a game of Yahtzee.
        static int currentRoll = 1;                             // The current roll of the round.
        static int numberOfRolls = 3;                           // Number of rolls allowed during a round.
        static int grandTotal;                                  // Total score.

        // Method that creates the initial dice roll. Only executed once.
        static void RollDice()
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                dice = random.Next(1, 7);
                diceRoll.Add(dice);
            }
        }

        // Method that creates the initial scorecard. Only executed once.
        static void CreateScoreCard()
        {
            scoreCard.Add(new Score { Name = "Aces" });
            scoreCard.Add(new Score { Name = "Twos" });
            scoreCard.Add(new Score { Name = "Threes" });
            scoreCard.Add(new Score { Name = "Fours" });
            scoreCard.Add(new Score { Name = "Fives" });
            scoreCard.Add(new Score { Name = "Sixes" });
            scoreCard.Add(new Score { Name = "3 of a Kind" });
            scoreCard.Add(new Score { Name = "4 of a Kind" });
            scoreCard.Add(new Score { Name = "Full House" });
            scoreCard.Add(new Score { Name = "Small Straight" });
            scoreCard.Add(new Score { Name = "Large Straight" });
            scoreCard.Add(new Score { Name = "Yahtzee" });
            scoreCard.Add(new Score { Name = "Chance" });

            // Creates a copy of the score card that is used for after the user inputs his or her score and for final calculations.
            finalScoreCard = scoreCard;
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

        // Score object to be used by the score card.
        public class Score
        {
            public string Name { get; set; }    // Name of the category
            public int Amount = 0;              // Amount of points for that category
        }

        // Method that displays the scorecard.
        static void DisplayScoreCard()
        {
            // Calculations for all of the scoring. Pretty much hardcoded because of how these were defined by the rules of the game.
            // Describe the upper section scoring. (Indices 0-5)
            int upperScore = 6;

            for (int i = 0; i < upperScore; i++)
                scoreCard[i].Amount = diceRoll.Where(j => j == (i + 1)).Count();

            // Describe the lower section scoring. (Indices 6-12)

            // 6. Three of a Kind

            // 7. Four of a Kind

            // 8. Full House

            // 9. Small Straight

            // 10. Large Straight

            // 11. Yahtzee

            // 12. Chance
            scoreCard[12].Amount = diceRoll.Sum();

            Console.WriteLine("Score card:");

            for (int i = 0; i < scoreCard.Count; i++)
            {
                Console.WriteLine(scoreCard[i].Name + ": " + scoreCard[i].Amount);
            }

            Console.WriteLine("Enter the category you want to submit your score to (ex. Fours, Small Straight).");
        }

        // Method that enters the score.
        static void EnterScore()
        {
            string s = Console.ReadLine();

            for (int i = 0; i < scoreCard.Count; i++)
            {
                if (s.ToLower().Equals(scoreCard[i].Name.ToLower()))
                {
                    finalScoreCard[i].Amount = scoreCard[i].Amount;
                    break;
                }
            }
        }

        static void CalculateScore()
        {
            List<int> scores = scoreCard.Select(i => i.Amount).ToList();
            grandTotal = scores.Sum();
        }

        // Main method.
        static void Main(string[] args)
        {
            // Create the score card.
            CreateScoreCard();

            // Roll the dice.
            RollDice();
            DisplayScoreCard();

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