            //RANDOM # GENERATOR
            Random random = new Random();

            //Score Generators
            //Creation of Upper Section Score
            int ScoreOf1s;
            int ScoreOf2s;
            int ScoreOf3s;
            int ScoreOf4s;
            int ScoreOf5s;
            int ScoreOf6s;
            int UpperPreBonus;
            int UpperTotal;

            //Creation of Upper Section Score
            int ThreeOfKind;
            int FourOfKind;
            int FullHouse;
            int SmallStraight;
            int LargeStraight;
            int Yahtzee;
            int Chance;
            int YahtzeeBonus;
            int LowerTotal;

            //Creation of Total Score
            int GrandTotal;

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

            //Create ArrayList to Use for Dice
            int[] DiceArray = { dice1, dice2, dice3, dice4, dice5 };

            //Showing totals for all categories AFTER ROLL 1
            Console.Write("\n\nMaximum Scores with this Roll...");

            //Calculate Upper Section Scores
            //Score of 1s
            ScoreOf1s = (DiceArray.Count(n => n == 1)) * 1;
            Console.WriteLine("\nAces: " + ScoreOf1s);
            //Score of 2s
            ScoreOf2s = (DiceArray.Count(n => n == 2)) * 2;
            Console.WriteLine("Twos: " + ScoreOf2s);
            //Score of 3s
            ScoreOf3s = (DiceArray.Count(n => n == 3)) * 3;
            Console.WriteLine("Threes: " + ScoreOf3s);
            //Score of 4s
            ScoreOf4s = (DiceArray.Count(n => n == 4)) * 4;
            Console.WriteLine("Fours: " + ScoreOf4s);
            //Score of 5s
            ScoreOf5s = (DiceArray.Count(n => n == 5)) * 5;
            Console.WriteLine("Fives: " + ScoreOf5s);
            //Score of 6s
            ScoreOf6s = (DiceArray.Count(n => n == 6)) * 6;
            Console.WriteLine("Sixes: " + ScoreOf6s);
            //Upper Score Before Bonus
            UpperPreBonus = ScoreOf1s + ScoreOf2s + ScoreOf3s + ScoreOf4s + ScoreOf5s + ScoreOf6s;
            Console.WriteLine("Subtotal of Upper Section: " + UpperPreBonus);
            //Upper Bonus & Upper Total Calculation
            if (UpperPreBonus >= 63)
            {
                UpperTotal = 35 + UpperPreBonus;
                Console.WriteLine("Total of Upper Section: " + UpperTotal);
            }
            else
            {
                UpperTotal = UpperPreBonus;
                Console.WriteLine("Total of Upper Section: " + UpperTotal);
            }

            //Calculate Lower Section Scores

            //Asking which dice the user would like to keep

            //Loop process

            Console.ReadLine();
