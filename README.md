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
            int UpperPreTotal;
            int UpperBonus;
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

            //Creation of TotalScore
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

            //Showing totals for all categories AFTER ROLL 1
            Console.Write("\nMaximum Scores with this Roll...");



            //Asking which dice the user would like to keep

            //Loop process

            Console.ReadLine();
