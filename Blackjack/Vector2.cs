//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="Vector2.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack
{
    public struct Vector2
    {
        public Vector2(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        public int x { get; set; }

        public int y { get; set; }
    }
}