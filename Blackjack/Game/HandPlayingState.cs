//  --------------------------------------------------------------------------------------------------------------------
//     <copyright file="HandPlayingState.cs">
//         Copyright (c) Nathan Bowman. All rights reserved.
//         Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     </copyright>
//  --------------------------------------------------------------------------------------------------------------------
namespace Blackjack.Game
{
    public enum HandPlayingState
    {
        Start,

        End,

        PlayerTurn,

        Dealerturn,

        ScoreScreen,

        FinishRound
    }
}