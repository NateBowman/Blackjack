namespace Blackjack.Cards
{
    using System.Collections;
    using System.Collections.Generic;

    public class HandOfCards : IList<Card>
    {
        protected readonly List<Card> Cards = new List<Card>();

        public int Count => Cards.Count;

        public bool IsReadOnly => ((ICollection<Card>)Cards).IsReadOnly;

        public Card this[int index] { get { return Cards[index]; } set { Cards[index] = value; } }

        public virtual void Add(Card item) => Cards.Add(item);

        public virtual void Clear() => Cards.Clear();

        public bool Contains(Card item) => Cards.Contains(item);

        public void CopyTo(Card[] array, int arrayIndex) => Cards.CopyTo(array, arrayIndex);

        public IEnumerator<Card> GetEnumerator() => Cards.GetEnumerator();

        public int IndexOf(Card item)
        {
            return Cards.IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            Cards.Insert(index, item);
        }

        public bool Remove(Card item) => Cards.Remove(item);

        public void RemoveAt(int index)
        {
            Cards.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Cards).GetEnumerator();
    }
}