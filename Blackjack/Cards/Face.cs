//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="Face.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Cards
{
    public enum Face
    {
        Ace = 0x1, // 1

        Two = 0x1 << 1, // 2

        Three = 0x1 << 2, // 4

        Four = 0x1 << 3, // 8

        Five = 0x1 << 4, // 16

        Six = 0x1 << 5, // 32

        Seven = 0x1 << 6, // 64

        Eight = 0x1 << 7, // 128

        Nine = 0x1 << 8, // 256

        Ten = 0x1 << 9, // 512

        Jack = 0x1 << 10, // 1024

        Queen = 0x1 << 11, // 2048

        King = 0x1 << 12 // 4096
    }
}