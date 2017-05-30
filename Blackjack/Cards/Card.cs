//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="Card.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Cards
{
    using System;
    using System.Collections.Generic;

    public class Card : IComparable<Card>, IComparable<Suit>, IComparable<Face>
    {
        private static readonly Dictionary<Face, KeyValuePair<string, int>> FaceValues = new Dictionary<Face, KeyValuePair<string, int>>
                                                                                             {
                                                                                                 {
                                                                                                     Face.Ace,
                                                                                                     new KeyValuePair<string, int>("A", 1)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Two,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "2",
                                                                                                         2)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Three,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "3",
                                                                                                         3)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Four,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "4",
                                                                                                         4)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Five,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "5",
                                                                                                         5)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Six,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "6",
                                                                                                         6)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Seven,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "7",
                                                                                                         7)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Eight,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "8",
                                                                                                         8)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Nine,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "9",
                                                                                                         9)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Ten,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "10",
                                                                                                         10)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Jack,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "J",
                                                                                                         10)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.Queen,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "Q",
                                                                                                         10)
                                                                                                 },
                                                                                                 {
                                                                                                     Face.King,
                                                                                                     new KeyValuePair<string, int>(
                                                                                                         "K",
                                                                                                         10)
                                                                                                 }
                                                                                             };

        public Card(Face cardFace, Suit cardSuit, bool faceup = true)
        {
            CardFace = cardFace;
            CardSuit = cardSuit;
            Value = FaceValues[cardFace].Value;
            FaceString = FaceValues[cardFace].Key;
            FaceUp = faceup;
        }

        public static string[] BackGraphic { get; } = { "╔════╦════╗", "║▓▓▓▓║░░░░║", "║▓▓▓▓║░░░░║", "╠════╬════╣", "║░░░░║▓▓▓▓║", "║░░░░║▓▓▓▓║", "╚════╩════╝" };

        public ConsoleColor BackgroundColor { get; } = ConsoleColor.White;

        public Face CardFace { get; set; }

        public Suit CardSuit { get; set; }

        public string[] FaceGraphic { get; private set; }

        public string FaceString { get; set; }

        public bool FaceUp { get; set; }

        public ConsoleColor ForegroundColor => FaceUp ? ((CardSuit == Suit.Club) || (CardSuit == Suit.Spade) ? ConsoleColor.Black : ConsoleColor.Red) : ConsoleColor.Blue;

        public char SuitString => (char)(int)CardSuit;

        public int Value { get; set; }

        public string[] CardString()
        {
            if (!FaceUp)
            {
                return BackGraphic;
            }
            if (FaceGraphic != null)
            {
                return FaceGraphic;
            }

            var str = SuitString.ToString();
            var val = FaceString.PadRight(2);
            var val2 = FaceString.PadRight(2);

            FaceGraphic = new[] { "╔═════════╗", $"║{val}.---.  ║", $"║ {str}()-()  ║", "║  :( ):  ║", $"║  ()-(){str} ║", $"║  '---'{val2}║", "╚═════════╝", };
            return FaceGraphic;
        }

        public int CompareTo(Card other)
        {
            var val = CardSuit.CompareTo(other.CardSuit);

            if (val == 0)
            {
                val = CardFace.CompareTo(other.CardFace);
            }

            return val;
        }

        public int CompareTo(Suit other)
        {
            return CardSuit.CompareTo(other);
        }

        public int CompareTo(Face other)
        {
            return CardFace.CompareTo(other);
        }
    }
}