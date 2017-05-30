//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="DeckOfCards.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DeckOfCards
    {
        private static readonly Random Rng = new Random(unchecked(Environment.TickCount * 31));

        private readonly Queue<Card> deck;

        public DeckOfCards()
        {
            deck = new Queue<Card>();
        }

        public DeckOfCards(bool shuffled = true, int deckCount = 1)
        {
            var listOfCards = new List<Card>();

            PopulateWith(listOfCards, deckCount);

            if (shuffled)
            {
                listOfCards.Shuffle(Rng);
            }

            deck = new Queue<Card>(listOfCards);
        }

        public int RemainingCards => deck.Count;

        public Card GetNext()
        {
            return deck.Dequeue();
        }

        public bool TryGetNext(out Card card)
        {
            return deck.TryDequeue(out card);
        }

        private static void PopulateWith(ICollection<Card> list, int deckCount)
        {
            for (var i = 0; i < deckCount; i++)
            {
                foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                {
                    foreach (var face in Enum.GetValues(typeof(Face)).Cast<Face>())
                    {
                        list.Add(new Card(face, suit, false));
                    }
                }
            }
        }
    }
}