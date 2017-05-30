namespace Blackjack.Cards
{
    public class HandOfBlackjackCards : HandOfCards
    {
        private const int HandValueLimit = 21;

        private int value;

        public bool HasBlackjack => Value == HandValueLimit;

        public bool IsBust => Value > HandValueLimit;

        public int Value
        {
            get
            {
                if ((value < (HandValueLimit - 10)) && Cards.Exists(card => card.CardFace == Face.Ace))
                {
                    value = value + 10;
                }

                return value;
            }

            set { this.value = value; }
        }

        public override void Add(Card item)
        {
            Cards.Add(item);
            if (item.FaceUp)
            {
                Value += item.Value;
            }
        }

        public override void Clear()
        {
            value = 0;
            base.Clear();
        }

        public void TurnAllFaceUp()
        {
            Cards.ForEach(
                card =>
                    {
                        if (!card.FaceUp)
                        {
                            value += card.Value;
                            card.FaceUp = true;
                        }
                    });
        }
    }
}