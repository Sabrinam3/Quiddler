/*
 * Class : Program
 * Purpose: A client project that uses the QuiddlerLibrary.dll to play a game of Quiddler
 */
using QuiddlerLibrary;
using System;
using System.Collections.Generic;

namespace QuiddlerClient
{
    class Program
    {
        private static Deck GameDeck;
        private static List<IPlayer> GamePlayers;

        static void Main(string[] args)
        {
            do
            {
                GameDeck = new Deck();
                GamePlayers = new List<IPlayer>();
                Console.WriteLine($"Test Client for {GameDeck.About}\n");
                initializeGame();

                //Game runs until one player runs out of cards
                int playerIndex;
                bool gameWon = false;
                do
                {
                    playerIndex = 1;

                    //each player in the list of players takes a turn
                    foreach (var player in GamePlayers)
                    {
                        gameWon = takeTurn(player, playerIndex);
                        if (gameWon)
                            break;
                        playerIndex++;
                    }
                } while (!gameWon);

                Console.Write("Play again? (y/n): ");
                string choice = Console.ReadLine().ToLower();
                if (choice == "n") break;
            } while (true);
        }

        /// <summary>
        /// Method that initializes the game by asking for the number of players and number of cards per player
        /// and then sets the player objects for the game
        /// </summary>
        static void initializeGame()
        {
            int playerCount, cardCount;
            //set playerCount
            do
            {
                Console.Write("How many players are there? (1-18): ");
                if (!int.TryParse(Console.ReadLine(), out playerCount) || (playerCount < 1 || playerCount > 18))
                {
                    Console.WriteLine("[Error]: Number of players must be a number between 1-18.");
                    continue;
                }
                break;
            } while (true);

            //set cardCount
            do
            {
                Console.Write("How many cards will be dealt to each player? (3-10): ");
                if (!int.TryParse(Console.ReadLine(), out cardCount))
                {
                    Console.WriteLine("[Error]: Number of cards must be a number between 3-10");
                    continue;
                }
                try
                {
                    GameDeck.CardsPerPlayer = cardCount;
                    break;
                }
                catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            } while (true);


            //Initialize the player objects and add them to the game list
            for (int i = 0; i < playerCount; i++)
            {
                GamePlayers.Add(GameDeck.NewPlayer());
            }
            Console.WriteLine();
            Console.WriteLine($"Cards were dealt to {playerCount} player(s)");
            Console.WriteLine($"The top card which was '{GameDeck.Discard}' was moved to the discard pile.");
            Console.WriteLine($"The deck now contains {GameDeck.CardCount} cards.\n");
        }


        /// <summary>
        /// Method that simulates one player's turn. Method ends when a player wins or the turn is done
        /// </summary>
        /// <param name="player">The current player that is taking a turn</param>
        /// <param name="playerIndex">The index value of the player for printing</param>
        /// <returns>a bool indicating if the player has won the game</returns>
        static bool takeTurn(IPlayer player, int playerIndex)
        {
            //Print the header
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Player {playerIndex}");
            Console.WriteLine(new string('-', 50));
            //Show cards
            Console.WriteLine($"Your cards are [{player.ToString()}].");
            //Ask about discard pile
            string choice = "";
            do
            {
                Console.Write($"Do you want the top card in the discard pile which is '{GameDeck.Discard}'? (y/n):");
                choice = Console.ReadLine().ToLower();
            } while (choice != "y" && choice != "n");

            //Test choice and either deal from Discard pile or deck
            if (choice == "y")
                player.PickupDiscard();
            else
                Console.WriteLine($"The dealer dealt '{player.DrawTopCard()}' to you from the deck.");
               
            Console.WriteLine($"Your cards are [{player.ToString()}].");

            //Ask about playing a word
            do
            {
                Console.Write("Test a word for its points value (y/n): ");
                choice = Console.ReadLine().ToLower();
                if (choice != "y" && choice != "n")
                    continue;
                if (choice == "y")
                {
                    Console.Write($"Enter a word using [{player.ToString()}] leaving a space between cards: ");
                    string word = Console.ReadLine();
                    int points = player.TestWord(word);
                    Console.WriteLine($"The word [{word}] is worth {points} points.");
                    if (points == 0)
                        continue;
                    do
                    {
                        Console.Write($"Do you want to play the word [{word}]? (y/n): ");
                        choice = Console.ReadLine().ToLower();
                    } while (choice != "y" && choice != "n");

                    if (choice == "n")
                        continue;

                    //play word
                    player.PlayWord(word);

                    //If the player has one or zero card(s) left, the player wins
                    if(player.CardCount == 0)
                    {
                        Console.WriteLine($"***** Player {playerIndex} is out! Game over!! ******");
                        return true;
                    }
                    if (player.CardCount == 1)
                    {
                        Console.WriteLine($"Dropping '{player.ToString().Trim()}' on the discard pile.");
                        Console.WriteLine($"***** Player {playerIndex} is out! Game over!! ******");
                        return true;
                    }
                    
                }
                //Ask player to discard a card
                Console.WriteLine($"Your cards are [{player.ToString()}] and you have {player.TotalPoints} points.");
                    do
                    {
                        Console.Write("Enter a card from your hand to drop on the discard pile: ");
                        choice = Console.ReadLine();
                    } while (!player.DropDiscard(choice));
                    Console.WriteLine($"Your cards are [{player.ToString()}].");
                return false;
            } while (true);
        }        
    }
}
