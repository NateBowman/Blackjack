namespace Blackjack.Output
{
    using System;

    public struct ConsoleCharacter
    {
        public ConsoleCharacter(char character, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
        {
            Background = background;
            Foreground = foreground;
            Character = character;
        }

        public ConsoleColor Background { get; }

        public char Character { get; }

        public ConsoleColor Foreground { get; }

        public static bool operator ==(ConsoleCharacter a, ConsoleCharacter b)
        {
            return (a.Background == b.Background) && (a.Foreground == b.Foreground) && (a.Character == b.Character);
        }

        public static bool operator !=(ConsoleCharacter a, ConsoleCharacter b)
        {
            return !(a == b);
        }

        public bool Equals(ConsoleCharacter other)
        {
            return (Background == other.Background) && (Foreground == other.Foreground) && (Character == other.Character);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is ConsoleCharacter && Equals((ConsoleCharacter)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Background;
                hashCode = (hashCode * 397) ^ (int)Foreground;
                hashCode = (hashCode * 397) ^ Character.GetHashCode();
                return hashCode;
            }
        }
    }
}