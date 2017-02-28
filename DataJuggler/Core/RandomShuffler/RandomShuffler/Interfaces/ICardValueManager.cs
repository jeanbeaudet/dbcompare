﻿

#region using statements

using DataJuggler.Core.RandomShuffler.Objects;
using DataJuggler.Core.RandomShuffler.Enumerations;
using System.Collections.Generic;

#endregion

namespace DataJuggler.Core.RandomShuffler.Interfaces
{
    public interface ICardValueManager
    {

        #region Methods

            #region GetCardValue(Card card, int currentValue = 0)
            /// <summary>
            /// Return the value of this card
            /// </summary>
            /// <param name="card"></param>
            /// <returns></returns>
            int GetCardValue(Card card, int currentValue = 0);
            #endregion

            #region GetCardValues(Card card);
            /// <summary>
            /// Return the value of this card
            /// </summary>
            /// <param name="card"></param>
            /// <returns></returns>
            List<int> GetCardValues(Card card);
            #endregion
            
        #endregion

    }
}
