//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="BlackjackGame.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
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