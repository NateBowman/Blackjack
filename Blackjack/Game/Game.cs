//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="Game.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Game
{
    using System;
    using System.Threading;

    using Blackjack.Cards;
    using Blackjack.Output;

    public class Game
    {
        private const int FirstDealCardCount = 2;

        private static readonly int CardCountToReshuffleCards = 10;

        private static readonly int MinBetAmount = 20;

        private int currentBet = MinBetAmount;

        private DeckOfCards currentDeck;

        private Dealer dealer;

        private Player player;

        public Game(int width, int height)
        {
            GameHeight = height;
            GameWidth = width;
            Screen.Clear();
            GameLoop();
        }

        public static int GameHeight { get; private set; }

        public static int GameWidth { get; private set; }

        public GameState CurrentGameState { get; set; } = GameState.Start;

        public HandPlayingState CurrentHandState { get; private set; } = HandPlayingState.Start;

        private static void EndGame()
        {
            // Show credits screen

            // Exit
            Environment.Exit(0);
        }

        private void BettingLoop()
        {
            // make sure the bet is in range
            currentBet = Math.Min(player.Chips, Math.Max(currentBet, MinBetAmount));

            var key = ConsoleKey.Escape;
            while (key != ConsoleKey.Enter)
            {
                Screen.DrawBettingInstructions(currentBet);
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Screen.ClearInstructions();
                        continue;
                    case ConsoleKey.Add:
                    case ConsoleKey.A:
                        currentBet = Math.Min(player.Chips, currentBet + MinBetAmount);
                        break;
                    case ConsoleKey.Subtract:
                    case ConsoleKey.S:
                        currentBet = Math.Max(currentBet - MinBetAmount, MinBetAmount);
                        break;
                }
            }

            player.PlaceBet(currentBet);
            dealer.PlaceBet(currentBet);

            Screen.DrawPlayerHand(dealer);
            Screen.DrawPlayerHand(player);

            Screen.DrawBet(currentBet);
            SoundManager.PlayRandom(SoundManager.SoundEffect.Chips, true);
        }

        private void DealCard(PlayerBase p, bool faceUp = true)
        {
            Card card;
            var success = currentDeck.TryGetNext(out card);

            if (success)
            {
                card.FaceUp = faceUp;
                p.Hand.Add(card);
            }
            else
            {
                throw new InvalidOperationException("Not enough cards in the deck");
            }
        }

        private void DealerTurnLoop()
        {
            Screen.ClearInstructions();

            // Flip dealer card
            dealer.Hand.TurnAllFaceUp();
            Screen.DrawPlayerHand(dealer);
            SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);

            // use dealer rules to dictate stick/twist
            while (dealer.Hand.Value < 17)
            {
                DealCard(dealer);
                Screen.DrawPlayerHand(dealer);
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);
            }

            // goto score check
            CurrentHandState = HandPlayingState.FinishRound;
        }

        private void DealStartingCards()
        {
            var i = 0;
            while (i < FirstDealCardCount)
            {
                DealCard(player);
                Screen.DrawPlayerHand(player);
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);

                DealCard(dealer, i != 0);
                Screen.DrawPlayerHand(dealer);
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);

                i++;
            }
        }

        private void FinalScoreScreen()
        {
            throw new NotImplementedException();
        }

        private void FinishRound()
        {
            if (!player.Hand.IsBust && (dealer.Hand.IsBust || (player.Hand.Value > dealer.Hand.Value)))
            {
                // player win
                for (var i = 0; i < 5; i++)
                {
                    Screen.DrawWinner("Player Wins!");
                    Sleep(300);
                    Screen.ClearWinner();
                    Sleep(300);
                }

                var winnings = player.Hand.HasBlackjack ? 2 : 1.5f;
                player.GiveWinnings(winnings);
                dealer.RemoveWinnings(winnings);
            }
            else if ((dealer.Hand.Value == player.Hand.Value) && !dealer.Hand.IsBust && !player.Hand.IsBust)
            {
                Screen.DrawWinner("Push! Return the Stakes.");
                Sleep(300);
                player.GiveWinnings(1);
                dealer.RemoveWinnings(1);
            }
            else
            {
                // dealer win
                Screen.DrawWinner("Dealer Wins!");
            }

            Screen.DrawPlayerHand(dealer);
            Screen.DrawPlayerHand(player);

            Screen.DrawCenterBox("Press any key to continue!");
            Console.ReadKey(true);

            Sleep(200);

            if (player.IsBankrupt || dealer.IsBankrupt)
            {
                CurrentHandState = HandPlayingState.End;
            }
            else
            {
                CurrentHandState = HandPlayingState.Start;
            }
        }

        private void GameLoop()
        {
            while (true)
            {
                switch (CurrentGameState)
                {
                    case GameState.Start:
                        StartGame();
                        break;
                    case GameState.End:
                        EndGame();
                        break;
                    case GameState.MainMenu:
                        MainMenuLoop();
                        break;
                    case GameState.Playing:
                        PlayGame();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void MainMenuLoop()
        {
            // Show Main menu
            // Screen.DrawMainMenu();
            // Conosle.Readkey ... 
            Screen.DrawMenu();
            while (true)
            {
                var key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                    case '2':
                        CurrentGameState = GameState.Playing;
                        return;
                    case 'e':
                    case 'E':
                        CurrentGameState = GameState.End;
                        return;
                }
            }
        }

        private void PlayerTurnLoop()
        {
            while (!player.Hand.IsBust && !player.Hand.HasBlackjack)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    DealCard(player);
                    Screen.DrawPlayerHand(player);
                    SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);
                }
                else if (key == ConsoleKey.Enter)
                {
                    CurrentHandState = HandPlayingState.Dealerturn;
                    return;
                }
            }

            CurrentHandState = HandPlayingState.Dealerturn;
        }

        private void PlayGame()
        {
            CurrentHandState = HandPlayingState.Start;
            ResetPlayers();
            PlayHandLoop();
        }

        private void PlayHandLoop()
        {
            while (CurrentHandState != HandPlayingState.End)
            {
                switch (CurrentHandState)
                {
                    case HandPlayingState.Start:
                        StartRound();
                        break;
                    case HandPlayingState.PlayerTurn:
                        PlayerTurnLoop();
                        break;
                    case HandPlayingState.Dealerturn:
                        DealerTurnLoop();
                        break;
                    case HandPlayingState.FinishRound:
                        FinishRound();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //Screen.DrawScreen();
            }

            CurrentGameState = GameState.MainMenu;
        }

        private void ResetPlayers()
        {
            player = new Player(1000);
            dealer = new Dealer(10000);
        }

        private void SetupDeck()
        {
            if ((currentDeck == null) || (currentDeck.RemainingCards < CardCountToReshuffleCards))
            {
                currentDeck = new DeckOfCards(true);
            }
        }

        private void ShowIntroScreen()
        {
            Screen.CoverScreenWithCardBacks();
            Screen.DrawLogo();
            Sleep(2000);
        }

        private void Sleep(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        private void StartGame()
        {
            // Init and run intro
            ShowIntroScreen();
            CurrentGameState = GameState.MainMenu;
        }

        private void StartRound()
        {
            SetupDeck();

            dealer.Hand.Clear();
            player.Hand.Clear();

            Screen.DrawGameplayBackground();
            Screen.DrawPlayerHand(dealer);
            Screen.DrawPlayerHand(player);

            BettingLoop();

            DealStartingCards();

            Screen.DrawGameplayInstructions();

            CurrentHandState = HandPlayingState.PlayerTurn;
        }
    }
}