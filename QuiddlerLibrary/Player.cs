/*
 * Class : Player.cs
 * Purpose: A class that represents a player of the game
 * Authors: Sabrina Tessier & Cory Watson
 */
using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace QuiddlerLibrary
{
    public class Player : IPlayer
    {
        //Private properties
        private Dictionary<string, int> hand;
        private Deck deck;
        private static Application application;

        //Constructors
        public Player()
        {
            hand = new Dictionary<string, int>();
        }

        public Player(Deck deck)
        {
            hand = new Dictionary<string, int>();
            this.deck = deck;
            initializeHand();
        }

        /// <summary>
        /// Finalizer -> cleans up the Microsoft Word Application object
        /// </summary>
        ~Player()
        {
            application?.Quit();
        }

        /// <summary>
        /// Returns the number of Cards left in the Player's hand
        /// </summary>
        public int CardCount => CardsInHand();

        /// <summary>
        /// Returns the Player's total points, or sets it (only accessible from inside the class)
        /// </summary>
        public int TotalPoints { get; private set; }

        /// <summary>
        /// Draws the top card from the deck
        /// </summary>
        /// <returns>The Card's value as a string</returns>
        public string DrawTopCard()
        {
            Card topCard = deck.DrawCard();
            if (hand.ContainsKey(topCard.Value))
                hand[topCard.Value]++;
            else
                hand.Add(topCard.Value, 1);

            return topCard.Value;

        }

        /// <summary>
        /// Drops a Card from the Player's hand onto the Discard pile
        /// </summary>
        /// <param name="letter"></param>
        /// <returns>A bool indicating if the chosen Card was in the Player's hand</returns>
        public bool DropDiscard(string letter)
        {
            foreach (KeyValuePair<string, int> item in hand)
            {
                if (item.Key == letter)
                {
                    hand[item.Key]--;   // Remove card from hand
                    deck.Discard = item.Key;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Picks up a Card from the Discard pile and adds it to the Player's hand
        /// </summary>
        /// <returns>The picked up Card's value as a string</returns>
        public string PickupDiscard()
        {
            Card c = new Card(deck.Discard);
            if(hand.ContainsKey(c.Value))
                hand[c.Value]++;
            
            else  
                hand.Add(c.Value, 1);
            deck.Discard = "";
            return c.Value;
        }

        /// <summary>
        /// Plays a word that has been validated
        /// </summary>
        /// <param name="candidate">The word to play</param>
        /// <returns>The number of points the player has after playing the word(if successful), else returns 0</returns>
        public int PlayWord(string candidate)
        {
            int wordValue = TestWord(candidate);

            if (wordValue > 0) //the word can be played, so remove these cards from the hand
            {
                string[] letters = candidate.Split(' ');

                foreach (string l in letters)
                {
                    if(l != "")
                    {
                        hand[l]--;
                        if (hand[l] == 0)
                            hand.Remove(l);
                    }
                }
                TotalPoints += wordValue;
                return TotalPoints;
            }
            return 0;
        }

        /// <summary>
        /// Tests a word for validity based on Cards in hand and if the word is valid according to the Microsoft Word spell checker
        /// </summary>
        /// <param name="candidate">The word to test</param>
        /// <returns>The number of points the word is worth(if valid), else returns 0</returns>
        public int TestWord(string candidate)
        {
            int result = 0;
            string cardValue = "";
            // Convert incoming word to Dictionary<letter, frequency>
            Dictionary<string, int> wordMap = new Dictionary<string, int>();
            for (int x = 0; x < candidate.Length; ++x)
            {
                if (!candidate[x].ToString().Equals(" "))
                {
                    cardValue += candidate[x];
                }
                
                if (candidate[x].ToString().Equals(" ") || x == candidate.Length - 1)
                {
                    if (!wordMap.ContainsKey(cardValue))
                        wordMap.Add(cardValue, 1);
                    else
                        wordMap[cardValue]++;

                    cardValue = "";
                }
            }

            // Check hand for candidate letters
            foreach (KeyValuePair<string, int> item in wordMap)
            {
                if (!hand.ContainsKey(item.Key) || hand[item.Key] < item.Value)
                {
                    return 0;
                }
                //Accumulate points for word
                result += Card.cardsInDeck[item.Key].Value;
            }

            application = new Application();
            string word = candidate.Replace(" ", "").ToLower();
            if (application.CheckSpelling(candidate.Replace(" ", "").ToLower()))
            {
                application.Quit();
                return result;
            }

            return 0;
        }

        /// <summary>
        /// Creates a string representation of the Cards in the Player's hand
        /// </summary>
        /// <returns>The string of Player Cards</returns>
        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<string, int> item in hand)
            {
                for (int x = 0; x < item.Value; ++x)
                    s += item.Key + " ";
            }
            return s;
        }

        /// <summary>
        /// Counts the number of Cards in the Player's hand
        /// </summary>
        /// <returns>The number of Cards</returns>
        private int CardsInHand()
        {
            int nCards = 0;

            foreach (KeyValuePair<string, int> item in hand)
            {
                nCards += item.Value;
            }
            return nCards;
        }

        /// <summary>
        /// Initializes the Player's hand by adding CardsPerPlayer number of Cards to the Player's hand from the Deck
        /// </summary>
        private void initializeHand()
        {
            for (int x = 0; x < deck.CardsPerPlayer; ++x)
            {
               DrawTopCard();
            }
        }
    }
}
