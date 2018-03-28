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
        static int dice;                                        // Value of a Dice
        static int numberOfDice = 5;                            // Can be changed if the rules of Yahtzee change the number of dice.
        static int currentRound = 0;                            // The current round of the game.
        static int numberOfRounds = 13;                         // Number of rounds in a game of Yahtzee.
        static int currentRoll = 0;                             // The current roll of the round.
        static int numberOfRolls = 3;                           // Number of rolls allowed during a round.
        static int currentScore = 0;                            // The current score after each round.
        static int grandTotal = 0;                              // Total score.
        static string lineSeparator = "====================";   // Line separator, for aesthetics.

        // Method that creates the initial dice roll. Only executed once.
        static void RollDice()
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                dice = random.Next(1, 7);
                diceRoll.Add(dice);
            }

            /*
            // Hardcode values of dice rolls to test if each category works.
            diceRoll.Add(1);
            diceRoll.Add(2);
            diceRoll.Add(3);
            diceRoll.Add(4);
            diceRoll.Add(5);
            */
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
        }

        // Method that shows a line separator.
        static void ShowLine()
        {
            Console.WriteLine(lineSeparator);
        }

        // Method that starts the game.
        static void StartGame()
        {
            // Create the score card and roll the dice.
            CreateScoreCard();
            RollDice();

            // Show a line separator.
            ShowLine();
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
            public int ShownScore = 0;          // Shown score for that category
            public int ActualScore = 0;         // Final score for that category
            public bool isUsed = false;         // If the category has been used for scoring.
        }

        // Method that calculates the score for each category.
        static void CalculateScore()
        {
            // Calculations for all of the scoring. Pretty much hardcoded because of how these were defined by the rules of the game.
            // Describe the upper section scoring. (Indices 0-5)
            int sum = diceRoll.Sum();
            int upperAmount = 6;

            // Sum the amounts for each dice value (1-6).
            for (int i = 0; i < upperAmount; i++)
                scoreCard[i].ShownScore = (diceRoll.Where(j => j == (i + 1)).Count() * (i + 1));

            // Describe the lower section scoring. (Indices 6-12)
            // Amount of points awarded for each respective category.
            int fullHouse = 25;
            int smallStraight = 30;
            int largeStraight = 40;
            int yahtzee = 50;

            // 6. Three of a Kind
            // Check to see if there are AT LEAST 3 occurrences of a number
            foreach (var v in diceRoll.GroupBy(i => i))
            {
                if (v.Count() >= 3)
                    scoreCard[6].ShownScore = sum;
            }

            // 7. Four of a Kind
            // Check to see if there are AT LEAST 4 occurrences of a number
            foreach (var v in diceRoll.GroupBy(i => i))
            {
                if (v.Count() >= 4)
                    scoreCard[7].ShownScore = sum;
            }

            // 8. Full House
            // Declare variables to determine if a full house was rolled.
            bool isDouble = false;
            bool isTriple = false;

            // Check to see if there are EXACTLY 2 occurrences of a number
            foreach (var v in diceRoll.GroupBy(i => i))
            {
                if (v.Count() == 2)
                    isDouble = true;
            }

            // Check to see if there are EXACTLY 3 occurrences of a number
            foreach (var v in diceRoll.GroupBy(i => i))
            {
                if (v.Count() == 3)
                    isTriple = true;
            }

            // If both conditions are met, then a full house was rolled and reward the points.
            if (isDouble && isTriple)
                scoreCard[8].ShownScore = fullHouse;

            // 9. Small Straight
            // 1, 2, 3, 4 OR 2, 3, 4, 5 OR 3, 4, 5, 6 are all small straights.
            // If all numbers are distinct, it must include a (3 & 4).
            if (diceRoll.Distinct().Count() == diceRoll.Count() && diceRoll.Contains(3) && diceRoll.Contains(4))
                scoreCard[9].ShownScore = smallStraight;

            // If only 4 numbers are distinct, then a (1 & 2) OR a (1 & 6) OR a (5 & 6) are required to be missing.
            else if (diceRoll.Distinct().Count() == 4 &&
                ((!diceRoll.Contains(1) && !diceRoll.Contains(2)) ||    // Is a 1 AND 2 missing? OR...
                (!diceRoll.Contains(1) && !diceRoll.Contains(6)) ||     // Is a 1 AND 6 missing? OR...
                (!diceRoll.Contains(5) && !diceRoll.Contains(6))))      // Is a 5 AND 6 missing?
                    scoreCard[9].ShownScore = smallStraight;

            // 10. Large Straight
            // This occurs if all dice are distinct AND either a 1 or 6 is missing.
            // 1, 2, 3, 4, 5 OR 2, 3, 4, 5, 6 are all large straights.
            if (diceRoll.Distinct().Count() == diceRoll.Count() && (!diceRoll.Contains(1) || !diceRoll.Contains(6)))
                scoreCard[10].ShownScore = largeStraight;

            // 11. Yahtzee (five of a kind)
            // Check to see if there are EXACTLY 5 occurrences of a number
            foreach (var v in diceRoll.GroupBy(i => i))
            {
                if (v.Count() == 5)
                    scoreCard[11].ShownScore = yahtzee;
            }

            // 12. Chance
            // Sum the amounts shown on the dice.
            scoreCard[12].ShownScore = sum;
        }

        // Method that displays the scorecard.
        static void DisplayScoreCard()
        {
            // Calculate the score for each category.
            CalculateScore();

            // Display a line separator.
            ShowLine();

            // Display the score card.
            Console.WriteLine("Score card. These are the categories available for scoring.");
            Console.WriteLine("The number after shows result for that category.");

            // Display the list of categories.
            for (int i = 0; i < scoreCard.Count; i++)
            {
                // Do not display categories that the user already used for scoring.
                if (!scoreCard[i].isUsed)
                    Console.WriteLine(scoreCard[i].Name + ": " + scoreCard[i].ShownScore);
            }

            // Display a line separator.
            ShowLine();
        }

        // Method that displays the header information.
        static void DisplayHeader()
        {
            // Display the current round.
            string round = "Current round: " + (currentRound + 1);

            // If it is the last round, display a different message.
            if ((currentRound + 1) == numberOfRounds)
                Console.WriteLine(round + " (last round!)");
            else
                Console.WriteLine(round);

            // Display the number of the dice rolls.
            Console.WriteLine("Roll #" + (currentRoll + 1));

            // Display a line separator.
            ShowLine();
        }

        // Method that displays the results of the dice roll.
        static void DisplayResults()
        {
            // Display each of the dice rolls.
            for (int i = 1; i < diceRoll.Count + 1; i++)
            {
                Console.WriteLine("Dice #" + i + ": " + diceRoll[i - 1].ToString());
            }
        }

        // Method that enters the score.
        static void EnterScore()
        {
            // Boolean to determine if the user completed a successful action.
            bool isSuccessful = false;

            do
            {
                // Ask the user for the category.
                Console.WriteLine("Enter the category you want to submit your score to (ex. Fours, Small Straight).");

                // User-inputted string.
                string s = Console.ReadLine();

                // Is the category already used?
                bool isAlreadyUsed = false;

                // Check to see the category that the user inputted.
                for (int i = 0; i < scoreCard.Count; i++)
                {
                    if (s.ToLower().Equals(scoreCard[i].Name.ToLower()))
                    {
                        // If the category selected was already used...
                        if (scoreCard[i].isUsed)
                        {
                            // Display a message to try again.
                            Console.WriteLine("Invalid input. Category " + s + " is already used for scoring. Please choose a different one.");
                            isAlreadyUsed = true;
                            break;
                        }

                        // If the category selected has not been used...
                        else
                        {
                            // Enter the score.
                            scoreCard[i].ActualScore = scoreCard[i].ShownScore;
                            Console.WriteLine("Successfully submitted. Current score: " + GetScore());

                            // Display a line separator.
                            ShowLine();

                            // Mark the category as "used."
                            scoreCard[i].isUsed = true;

                            // User input is successful. Break out of the loop.
                            isSuccessful = true;
                            break;
                        }
                    }
                }

                // If a category was not found AND not used, the user misspelled.
                if (!isSuccessful && !isAlreadyUsed)
                    Console.WriteLine("Invalid input. Make sure you spelled the category correctly.");
            }
            while (!isSuccessful);

            // Reset the round.
            Reset();
        }

        // Method that resets everything to prepare for the next round.
        static void Reset()
        {
            // Reset the scores shown back to their default values.
            foreach (Score score in scoreCard)
                score.ShownScore = 0;

            // Reset the current roll back to 0.
            currentRoll = 0;

            // Increment the current round.
            currentRound++;

            // Reset the dice values back to 0. This is required for rerolling the dice.
            for (int i = 0; i < diceRoll.Count; i++)
                diceRoll[i] = 0;

            // Reroll the dice for the next round.
            RerollDice();
        }

        // Method that returns the score.
        static int GetScore()
        {
            // Sum the scores.
            currentScore = scoreCard.Sum(i => i.ActualScore);

            // Return the current score.
            return currentScore;
        }

        // Method that asks the user if he or she wants a reroll.
        static void AskForReroll()
        {
            // Boolean to determine if the user completed a successful action.
            bool isSuccessful = false;

            // Do this until the user enters a valid input (either "Y" or "N").
            do
            {
                // Asks the user if he or she would like to use a reroll.
                string roll = "Do you want to re-roll? Yes or No.";

                // If it is the last roll, display a different message.
                if ((currentRoll + 2) == numberOfRolls)
                    Console.WriteLine(roll + " (last re-roll!)");
                else
                    Console.WriteLine(roll);

                // User-inputted string.
                string s = Console.ReadLine();

                // If the user enters YES...
                if (s.ToLower() == "yes")
                {
                    // Display a message and break out of the loop.
                    Console.WriteLine("A reroll was selected.");
                    isSuccessful = true;

                    // Ask the user to select the dice to keep.
                    SelectDice();
                }

                // If the user enters NO...
                else if (s.ToLower() == "no")
                {
                    // Display a message and break out of the loop.
                    Console.WriteLine("No reroll was selected.");
                    isSuccessful = true;

                    // Ask the user to enter the score
                    EnterScore();
                }

                // If the user enters an invalid input...
                else
                {
                    // Display a message and continue looping.
                    Console.WriteLine("Invalid input. Please type Yes or No.");
                }
            }
            while (!isSuccessful);
        }

        // Method that asks the user which dice he or she wants to keep.
        static void SelectDice()
        {
            // Boolean to determine if the user completed a successful action.
            bool isSuccessful = false;

            // Do this until the user enters a valid input.
            do
            {
                // Ask the user which dice he or she wants to keep.
                Console.WriteLine("Which dice #s do you want to keep? Leave blank for none. Separate using spaces (ex. 2 3 5)");

                // User-inputted string.
                string s = Console.ReadLine();

                // Check for any invalid input. Ex. inputting any symbols or letters.
                try
                {
                    // If left blank...
                    if (String.IsNullOrEmpty(s))
                    {
                        // Display a message.
                        Console.WriteLine("No dice were selected. Rerolling...");

                        // Reset all dice values to be rerolled.
                        for (int i = 0; i < diceRoll.Count; i++)
                            diceRoll[i] = 0;
                    }

                    // If not blank...
                    else
                    {
                        // Split the inputted string into each individual dice value.
                        List<int> diceKept = s.Split(null).Select(Int32.Parse).ToList();

                        // Sort the list so numbers go from smallest to largest.
                        diceKept.Sort();

                        // Check to see if the user entered the same number twice.
                        bool isUnique = diceKept.Distinct().Count() == diceKept.Count();

                        // If so, then warn the user and try again.
                        if (!isUnique)
                        {
                            Console.WriteLine("Invalid input. Dice number was repeated.");
                            continue;
                        }

                        // Check to see if a number less than 1 or greater than 6 was inputted.
                        foreach (int i in diceKept)
                        {
                            if (i < 1 || i > 6)
                            {
                                Console.WriteLine("Invalid input. Please choose a number between 1 and 6.");
                                continue;
                            }
                        }

                        // Show a message displaying the user's kept dice.
                        string diceSelected = "";

                        for (int i = 0; i < s.Length; i++)
                        {
                            // Add a period after the last selected dice instead of a comma.
                            if (i == (s.Length - 1))
                                diceSelected += s[i] + ".";
                            else
                                diceSelected += s[i];
                        }

                        // Display the message.
                        Console.WriteLine("You selected to keep dice " + diceSelected);

                        // Display a line separator.
                        Console.WriteLine(lineSeparator);

                        // Temporary variable. Used for iterating.
                        int j = 0;

                        // Check to see which dice the user selected to keep.
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
                    }

                    // Break out of the loop.
                    isSuccessful = true;
                }

                // If there are any errors...
                catch (Exception e)
                {
                    // Display the message and continue looping.
                    Console.WriteLine("Invalid input." + e.Message);
                    continue;
                }
            }
            while (!isSuccessful);

            // Reroll the dice.
            RerollDice();
        }

        // Method that ends the game.
        static void EndGame()
        {
            // Show a message indicating that the game is over.
            Console.WriteLine("Game over! Results:");

            // Show a line separator.
            ShowLine();

            // Display the results.
            for (int i = 0; i < scoreCard.Count; i++)
                Console.WriteLine(scoreCard[i].Name + ": " + scoreCard[i].ActualScore);

            // Show a line separator.
            ShowLine();

            // Check to see if an Upper Section bonus should be rewarded.
            int upperScoreThreshold = 23;
            int upperScoreSum = 0;
            int upperScoreBonus = 35;

            // Check to see if the Upper Section total surpassed the threshold.
            for (int i = 0; i < 6; i++)
                upperScoreSum += scoreCard[i].ActualScore;

            // Reward the user with the bonus.
            if (upperScoreSum >= upperScoreThreshold)
            {
                grandTotal += upperScoreBonus;
                Console.WriteLine("Upper Section total: " + upperScoreSum);
                Console.WriteLine("Upper Section bonus: " + upperScoreBonus);
                ShowLine();
            }

            // Calculate the final score after all bonuses.
            grandTotal += currentScore;

            Console.WriteLine("Final score: " + grandTotal);

            // Display information on where to buy the game.
            // TODO.
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        // Main method.
        static void Main(string[] args)
        {
            // Start the game. 
            StartGame();

            // If the game is not over...
            do
            {
                // Display the header information.
                DisplayHeader();

                // Display the results.
                DisplayResults();

                // Display the score card.
                DisplayScoreCard();

                // Ask the user wants to reroll if it is not the last reroll for the round.
                if ((currentRoll + 1) < numberOfRolls)
                    AskForReroll();

                // If the user has reached the maximum number of allowed rolls, ask him or her to choose a category to enter the score in.
                else
                {
                    // Enter the score.
                    EnterScore();
                }
            }
            while (currentRound < numberOfRounds);

            // End the game.
            EndGame();
        }
    }
}