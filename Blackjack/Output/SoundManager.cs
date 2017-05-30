//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="SoundManager.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Output
{
    using System;
    using System.Collections.Generic;
    using System.Media;
    using System.Reflection;

    public static class SoundManager
    {
        private static readonly Random Rng;

        private static readonly int SoundChannels = 10;

        private static readonly SoundPlayer[] SoundPlayers = new SoundPlayer[SoundChannels];

        private static readonly Dictionary<SoundEffect, string[]> Sounds;

        private static int playerIndex;

        static SoundManager()
        {
            Rng = new Random(unchecked(Environment.TickCount * 31));
            for (var p = 0; p < SoundPlayers.Length; p++)
            {
                SoundPlayers[p] = new SoundPlayer();
            }

            Sounds = new Dictionary<SoundEffect, string[]>
                         {
                             { SoundEffect.Shove, new[] { "cardShove1.wav", "cardShove2.wav", "cardShove3.wav", "cardShove4.wav" } },
                             {
                                 SoundEffect.Slide,
                                 new[]
                                     {
                                         "cardSlide1.wav", "cardSlide2.wav", "cardSlide3.wav", "cardSlide4.wav", "cardSlide5.wav", "cardSlide6.wav",
                                         "cardSlide7.wav", "cardSlide8.wav"
                                     }
                             },
                             { SoundEffect.Shuffle, new[] { "cardShuffle.wav" } },
                             { SoundEffect.Chips, new[] { "chipsHandle6.wav" } }
                         };
        }

        public enum SoundEffect
        {
            Shove,

            Slide,

            Shuffle,

            Chips
        }

        private static SoundPlayer NextPlayer => SoundPlayers[playerIndex++ % SoundChannels];

        public static void PlayRandom(SoundEffect effect, bool blocking = false)
        {
            var effectArray = Sounds[effect];
            var player = NextPlayer;

            player.Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Blackjack.Resources.{effectArray[Rng.Next(0, effectArray.Length)]}");

            if (blocking)
            {
                player.PlaySync();
            }
            else
            {
                player.Play();
            }
        }
    }
}