/*
 * Class : Deck.cs
 * Purpose: A class that represents a deck of cards
 * Authors: Sabrina Tessier & Cory Watson
 */
using System;
using System.Collections.Generic;
using System.Linq;


namespace QuiddlerLibrary
{
    public class Deck : IDeck 
    {
        //Private properties
        private int cardsPerPlayer;
        private List<Card> DeckOfCards;
        private Card DiscardPile;

        public Deck()
        {
            DeckOfCards = new List<Card>();
            DiscardPile = new Card();
            //populate the deck of cards
            repopulate();
        }

        /// <summary>
        /// Returns a string listing the authors of the project and the name of the library
        /// </summary>
        public string About
        {
            get
            {
                return "QuiddlerLibrary (TM) Library © 2020";
            }
        }

        /// <summary>
        /// Returns the count of cards remaining in the deck
        /// </summary>
        public int CardCount
        {
            get
            {
                return DeckOfCards.Count;
            }
        }

        /// <summary>
        /// Returns the number of cards per player
        /// </summary>
        public int CardsPerPlayer
        {
            get
            {
                return cardsPerPlayer;
            }

            set
            {
                if (value < 3 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("CardsPerPlayer must be between 3 and 10");
                }

                cardsPerPlayer = value;
            }
        }

        /// <summary>
        /// Returns the string value of the card in the Discard pile, or sets the new value of the Discard pile (only accessible from inside the class)
        /// </summary>
        public string Discard
        {
            get
            {
                return DiscardPile.ToString();
            }
            set => DiscardPile = new Card(value);
        }

        /// <summary>
        /// Allows the client to create game players
        /// </summary>
        /// <returns>A player that has been initialized with cards</returns>
        public IPlayer NewPlayer()
        {
            IPlayer player = new Player(this);
            return player;
        }

        /// <summary>
        /// Randomizes the deck of cards using a Random object
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            DeckOfCards = DeckOfCards.OrderBy(card => rng.Next()).ToList();
        }
        /// <summary>
        /// Method that populates the deck with cards
        /// </summary>
        private void repopulate()
        {
            //Clear out the old cards
            DeckOfCards.Clear();
            //loop through the dictionary creating cards
            foreach (var item in Card.cardsInDeck)
            {
                for (int i = 0; i < item.Value.Key; i++)
                {
                    DeckOfCards.Add(new Card(item.Key));
                }
            }

            //Randomize the deck
            Shuffle();

            Console.WriteLine($"The deck is initialized with {CardCount} cards\n");

            //Set the first card in the Discard Pile
            Discard = DrawCard().ToString();
        }

        /// <summary>
        /// Draws a card from the deck and returns it to the client
        /// </summary>
        /// <returns>A Card from the top of the deck</returns>
        public Card DrawCard()
        {
            int lastCard = DeckOfCards.Count - 1;
            Card c = DeckOfCards[lastCard];
            DeckOfCards.RemoveAt(lastCard);
            return c;
        }
    }
}
