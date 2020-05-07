/*
 * Class : Card
 * Purpose: A class that represents a Card
 * Authors: Sabrina Tessier & Cory Watson
 */
using System.Collections.Generic;

namespace QuiddlerLibrary
{
    public class Card {

        //properties
        public string Value { get; }
        public int Points { get; }

        //Dictionary to hold the number of each card in a deck and its points value
        public static Dictionary<string, KeyValuePair<int, int>> cardsInDeck = new Dictionary<string, KeyValuePair<int, int>>
        {
            {"b", new KeyValuePair<int, int>(2, 8) },
            {"c", new KeyValuePair<int, int>(2, 8) },
            {"f", new KeyValuePair<int, int>(2, 6) },
            {"h", new KeyValuePair<int, int>(2, 7) },
            {"j", new KeyValuePair<int, int>(2, 13) },
            {"k", new KeyValuePair<int, int>(2, 8) },
            {"m", new KeyValuePair<int, int>(2, 5) },
            {"p", new KeyValuePair<int, int>(2, 6) },
            {"q", new KeyValuePair<int, int>(2, 15) },
            {"v", new KeyValuePair<int, int>(2, 11) },
            {"w", new KeyValuePair<int, int>(2, 10) },
            {"x", new KeyValuePair<int, int>(2, 12) },
            {"z", new KeyValuePair<int, int>(2, 14) },
            {"cl", new KeyValuePair<int, int>(2, 10) },
            {"er", new KeyValuePair<int, int>(2, 7) },
            {"in", new KeyValuePair<int, int>(2, 7) },
            {"qu", new KeyValuePair<int, int>(2, 9) },
            {"th", new KeyValuePair<int, int>(2, 9) },
            {"d", new KeyValuePair<int, int>(4, 5) },
            {"g", new KeyValuePair<int, int>(4, 6) },
            {"l", new KeyValuePair<int, int>(4, 3) },
            {"s", new KeyValuePair<int, int>(4, 3) },
            {"y", new KeyValuePair<int, int>(4, 4) },
            {"n", new KeyValuePair<int, int>(6, 5) },
            {"r", new KeyValuePair<int, int>(6, 5) },
            {"t", new KeyValuePair<int, int>(6, 3) },
            {"u", new KeyValuePair<int, int>(6, 4) },
            {"i", new KeyValuePair<int, int>(8, 2) },
            {"o", new KeyValuePair<int, int>(8, 2) },
            {"a", new KeyValuePair<int, int>(10, 2) },
            {"e", new KeyValuePair<int, int>(12, 2) },
        };

        //Constructors
        public Card() { }
        
        public Card(string value)
        {
            if (value.Equals(""))
                Points = 0;
            else
                Points = cardsInDeck[value].Value;
            Value = value;
        }
        /// <summary>
        /// String representation of a card
        /// </summary>
        /// <returns>The card value as a string</returns>
        public override string ToString()
        {
            return Value;
        }



    }
}
