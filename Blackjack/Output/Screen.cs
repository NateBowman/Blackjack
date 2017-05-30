namespace Blackjack.Output
{
    using System;
    using System.Text;
    using System.Threading;

    using Blackjack.Cards;
    using Blackjack.Game;

    /* TODO Make this a fully qualified display class, display cards values from hands
     * Decouple game logic from display
     * 
     * Base current display on the game state, 
     * 
     * menu -> Draw menu
     * Intro -> draw intro
     * game -> draw game stuff
     * 
     */

    public static class Screen
    {
        public static readonly string[][] LogoString = new[]
                                                           {
                                                               new[] { "╔═════════╗", "║ B.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'B ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ L.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'L ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ A.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'A ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ C.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'C ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ K.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'K ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ J.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'J ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ A.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'A ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ C.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'C ║", "╚═════════╝", },
                                                               new[] { "╔═════════╗", "║ K.---.  ║", "║  ()-()  ║", "║  :( ):  ║", "║  ()-()  ║", "║  '---'K ║", "╚═════════╝", },
                                                           };

        public static readonly int LogoWidth = 27;

        public static readonly int MenuHeight = 20;

        public static readonly string[][] MenuString =
            {
                new[]
                    {
                        @"╔══════════════════════════════════════════╗", @"║▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓                                        ▓║",
                        @"║▓                                        ▓║", @"║▓╚══════════════════════════════════════╝▓║",
                        @"║▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓║", @"╚══════════════════════════════════════════╝",
                    },
                new[]
                    {
                        @"", @"", @"╔══════════════════════════════════════╗", @"║▓▓░░                              ░░▓▓║",
                        @"║░░                                  ░░║", @"║                                      ║",
                        @"║                                      ║", @"╠══════════════════════════════════════╣",
                        @"║                                      ║", @"║                                      ║",
                        @"║                                      ║", @"║                                      ║",
                        @"║                                      ║", @"║                                      ║",
                        @"║                                      ║", @"║░░                                  ░░║",
                        @"║▓▓░░                              ░░▓▓║", @"╚══════════════════════════════════════╝", @"", @"",
                    },
                new[]
                    {
                        @"", @"", @"", @"  /\/\    ___  _ __   _   _ ", @" /    \  / _ \| '_ \ | | | |", @"/ /\/\ \|  __/| | | || |_| |",
                        @"\/    \/ \___||_| |_| \__,_|", @"", @"", @"1) Beat The Dealer", @"2) x- High Scores -x", @"E) Exit", @"", @"",
                    }
            };

        public static readonly int MenuWidth = 44;

        private static readonly Vector2 CursorPosition = new Vector2(0, Game.GameHeight - 2);

        private static readonly Vector2 DealerStartPosition = new Vector2 { x = 5, y = 3 };

        private static readonly Vector2 PlayerStartPosition = new Vector2 { x = 5, y = 15 };

        private static readonly Vector2 InstructionPosition = new Vector2(Game.GameWidth - 33, PlayerStartPosition.y + 1);

        private static bool hasChanged;

        private static ConsoleCharacter[,] lastScreenBuffer;

        private static ConsoleCharacter[,] screenBuffer;

        static Screen()
        {
            // set console encoding to unicode so we can use unicode card chars             
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(Game.GameWidth + 2, Game.GameHeight + 1);
            Console.SetBufferSize(Game.GameWidth + 2, Game.GameHeight + 1);

            Console.CursorVisible = false;

            Console.Title = "Blackjack";

            Console.WindowTop = 0;
            Console.WindowLeft = 0;

            lastScreenBuffer = new ConsoleCharacter[Game.GameWidth, Game.GameHeight];
            screenBuffer = new ConsoleCharacter[Game.GameWidth, Game.GameHeight];
        }

        private static string[] BoxSlice { get; } = new[] { "╔═╗", "║ ║", "╠═╣", "╚═╝", };

        public static void Clear()
        {
            screenBuffer = new ConsoleCharacter[Game.GameWidth, Game.GameHeight];
        }

        public static void ClearInstructions()
        {
            var pos = new Vector2(Game.GameWidth - 33, PlayerStartPosition.y + 1);
            ClearArea(pos, 31, 4);
            DrawScreen();
        }

        public static void ClearWinner()
        {
            var pos = new Vector2(Game.GameWidth - 33, DealerStartPosition.y + 1);
            ClearArea(pos, 31, 3);
            DrawScreen();
        }

        public static void CoverScreenWithCardBacks()
        {
            var rnd = new Random();
            for (var i = 0; i < 50; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    PrintStringArrayToScreen(
                        new Vector2(rnd.Next(Game.GameWidth), rnd.Next(Game.GameHeight)),
                        Card.BackGraphic,
                        (i % 3) == 1 ? ConsoleColor.White : ConsoleColor.Gray,
                        ConsoleColor.Black);
                }

                DrawScreen();
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide);
                Thread.Sleep(100);
            }
        }

        public static void DrawBet(int currentBet)
        {
            DrawCenterBox($"Bet: ${currentBet}");
        }

        public static void DrawBettingInstructions(int currentBet)
        {
            DrawInstructions("Place your bet!", $"    +/A  {currentBet}  S/-");
        }

        public static void DrawCenterBox(string text)
        {
            var pos = new Vector2(Game.GameWidth / 2, Game.GameHeight / 2);

            pos.x -= 15;

            PrintBox(pos, 30, 3);
            pos.x += 2;
            pos.y += 1;
            PrintStringToScreenPosition(pos, text);
            DrawScreen();
        }

        public static void DrawGameplayBackground()
        {
            Clear();
            PrintBox(new Vector2(1, 1), Game.GameWidth - 1, Game.GameHeight - 1, true);
            DrawScreen();
        }

        public static void DrawGameplayInstructions()
        {
            DrawInstructions("Press Space to take a card", "Press Enter to stick");
        }

        public static void DrawLogo()
        {
            var topOffset = 2;
            var leftOffset = (Game.GameWidth / 2) - (LogoWidth / 2);
            for (var i = 0; i < 5; i++)
            {
                PrintStringArrayToScreen(new Vector2((i * 4) + leftOffset, topOffset), LogoString[i], ConsoleColor.White, ConsoleColor.Red);
                DrawScreen();
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);
            }
            for (var i = 0; i < 4; i++)
            {
                PrintStringArrayToScreen(
                    new Vector2((i * 4) + leftOffset + 2, topOffset + 3),
                    LogoString[i + 5],
                    ConsoleColor.White,
                    i == 0 ? ConsoleColor.Black : ConsoleColor.Red);
                DrawScreen();
                SoundManager.PlayRandom(SoundManager.SoundEffect.Slide, true);
            }
        }

        public static void DrawMenu()
        {
            var offset = new Vector2((Game.GameWidth / 2) - (MenuWidth / 2), (Game.GameHeight / 2) - (MenuHeight / 2));
            PrintStringArrayToScreen(offset, MenuString[0], ConsoleColor.DarkRed, ConsoleColor.DarkGray);
            offset.x += 2;
            PrintStringArrayToScreen(offset, MenuString[1]);
            offset.x += 6;
            PrintStringArrayToScreen(offset, MenuString[2], foreground: ConsoleColor.Red);

            DrawScreen();
        }

        public static void DrawPlayerHand(PlayerBase player)
        {
            PrintHand(player, player.PlayerType == PlayerType.Dealer ? DealerStartPosition : PlayerStartPosition);
            DrawScreen();
        }

        public static void DrawScreen()
        {
            if (!hasChanged)
            {
                return;
            }

            Console.CursorVisible = false;
            for (var x = 0; x < screenBuffer.GetLength(0); x++)
            {
                for (var y = 0; y < screenBuffer.GetLength(1); y++)
                {
                    // Only output changes
                    if (screenBuffer[x, y] == lastScreenBuffer[x, y])
                    {
                        continue;
                    }

                    Console.BackgroundColor = screenBuffer[x, y].Background;
                    Console.ForegroundColor = screenBuffer[x, y].Foreground;
                    Console.SetCursorPosition(x, y);
                    Console.Write(screenBuffer[x, y].Character);
                }
            }

            // Flush the read buffer
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            Console.SetCursorPosition(CursorPosition.x, CursorPosition.y);
            Console.ResetColor();
            lastScreenBuffer = (ConsoleCharacter[,])screenBuffer.Clone();
            hasChanged = false;
        }

        public static void DrawWinner(string winnerText)
        {
            var pos = new Vector2(Game.GameWidth - 33, DealerStartPosition.y + 1);
            PrintBox(pos, 31, 3);
            pos.x += 2;
            pos.y += 1;
            PrintStringToScreenPosition(pos, winnerText);
            DrawScreen();
        }

        public static void PrintBox(Vector2 position, int width, int height, bool split = false)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var i = 1;
                    var j = 1;

                    if (x == 0)
                    {
                        i = 0;
                    }
                    else if (x == (width - 1))
                    {
                        i = 2;
                    }

                    if (y == 0)
                    {
                        j = 0;
                    }
                    else if (y == (height - 1))
                    {
                        j = 3;
                    }
                    else if (split && (y == (height / 2)))
                    {
                        j = 2;
                    }

                    PrintConsoleCharacterToScreenPoint(position.x + x, position.y + y, new ConsoleCharacter(BoxSlice[j][i]));
                }
            }
        }

        public static void PrintConsoleCharacterToScreenPoint(int x, int y, ConsoleCharacter consoleCharacter, bool wrap = true)
        {
            if (wrap)
            {
                x = x % screenBuffer.GetLength(0);
                y = y % screenBuffer.GetLength(1);
            }
            if ((x < 0) || (y < 0) || (x > screenBuffer.GetLength(0)) || (y > screenBuffer.GetLength(1)) || (screenBuffer[x, y] == consoleCharacter))
            {
                return;
            }

            screenBuffer[x, y] = consoleCharacter;
            hasChanged = true;
        }

        public static void PrintStringArrayToScreen(Vector2 position, string[] strings, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
        {
            if ((position.x >= screenBuffer.GetLength(0)) || (position.x <= 0))
            {
                return;
            }

            for (var y = 0; y < strings.Length; y++)
            {
                if ((position.y < screenBuffer.GetLength(0)) && (position.x > 0))
                {
                    PrintStringToScreenPosition(new Vector2(position.x, position.y + y), strings[y], background, foreground);
                }
            }
        }

        private static void ClearArea(Vector2 position, int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    PrintConsoleCharacterToScreenPoint(position.x + x, position.y + y, default(ConsoleCharacter));
                }
            }
        }

        private static void DrawInstructions(string line1, string line2)
        {
            var pos = InstructionPosition;
            PrintBox(pos, 31, 4);
            pos.x += 2;
            pos.y += 1;
            PrintStringToScreenPosition(pos, line1);
            pos.y += 1;
            PrintStringToScreenPosition(pos, line2);
            DrawScreen();
        }

        private static void PrintCardToScreenPosition(Vector2 position, Card card)
        {
            var foregroundColor = card.ForegroundColor;
            var backgroundColor = card.BackgroundColor;

            var s = card.CardString();
            PrintStringArrayToScreen(position, s, backgroundColor, foregroundColor);
        }

        private static void PrintHand(PlayerBase player, Vector2 startPosition)
        {
            // hand Value box
            var scorePosition = new Vector2(startPosition.x, startPosition.y + 3);
            PrintBox(scorePosition, 4, 3);
            scorePosition.x++;
            scorePosition.y++;
            PrintStringToScreenPosition(scorePosition, player.Hand.Value.ToString().PadLeft(2));

            // chips Value box
            var moneyPosition = new Vector2(startPosition.x - 1, startPosition.y + 6);
            PrintBox(moneyPosition, 8, 3);
            moneyPosition.x++;
            moneyPosition.y++;
            PrintStringToScreenPosition(moneyPosition, $"${player.Chips}");

            // Cards
            var pos = startPosition;
            pos.x += 9;

            for (var i = 0; i < player.Hand.Count; i++)
            {
                PrintCardToScreenPosition(pos, player.Hand[i]);
                pos.x += 4;
            }

            DrawScreen();
        }

        private static void PrintStringToScreenPosition(Vector2 pos, string value, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
        {
            for (var x = 0; (x < (screenBuffer.GetLength(0) - pos.x)) && (x < value.Length); x++)
            {
                PrintConsoleCharacterToScreenPoint(x + pos.x, pos.y, new ConsoleCharacter(value[x], background, foreground));
            }
        }
    }
}