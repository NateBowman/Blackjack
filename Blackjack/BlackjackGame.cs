namespace Blackjack {
    using System;

    internal class BlackjackGame {
        public const int GameHeight = 25;
        public const int GameWidth = 80;

        private static Game.Game game;

        private static void Main(string[] args) {
            game = new Game.Game(GameWidth, GameHeight);
        }
    }
}