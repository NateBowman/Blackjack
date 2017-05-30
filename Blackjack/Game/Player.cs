namespace Blackjack.Game
{
    using System;

    using Blackjack.Cards;

    public abstract class PlayerBase
    {
        protected PlayerBase(PlayerType playerType, int chips = 0)
        {
            PlayerType = playerType;
            Chips = chips;
            Hand = new HandOfBlackjackCards();
        }

        public int Chips { get; protected set; }

        public HandOfBlackjackCards Hand { get; }

        public bool IsBankrupt => Chips < 1;

        public int LastBet { get; protected set; }

        public PlayerType PlayerType { get; }

        public abstract void PlaceBet(int amount);
    }

    internal class Dealer : PlayerBase
    {
        public Dealer(int chips = 0)
            : base(PlayerType.Dealer, chips)
        {
        }

        public override void PlaceBet(int amount)
        {
            Chips += amount;
            LastBet = amount;
        }

        public void RemoveWinnings(float rate)
        {
            Chips -= (int)(LastBet * rate);
            LastBet = 0;
        }
    }

    public class Player : PlayerBase
    {
        public Player(int chips = 0)
            : base(PlayerType.Player, chips)
        {
        }

        public void GiveWinnings(float rate)
        {
            Chips += (int)(LastBet * rate);
            LastBet = 0;
        }

        public override void PlaceBet(int amount)
        {
            Chips -= Math.Abs(amount);
            LastBet = amount;
        }
    }
}