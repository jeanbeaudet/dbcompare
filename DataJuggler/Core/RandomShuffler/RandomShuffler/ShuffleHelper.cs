

#region using statements

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using DataJuggler.Core.UltimateHelper;

#endregion

namespace DataJuggler.Core.RandomShuffler
{

    #region class ShuffleHelper
    /// <summary>
    /// This method [Enter Method Description]
    /// </summary>
    public static class ShuffleHelper
    {
        
        #region Methods

            #region Shuffle(this IList<T> list)
            /// <summary>
            /// This class is used to Shuffle a list of type 'T'.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="list"></param>
            public static List<T> Shuffle<T>(this IList<T> list)
            {
                // List
                List<T> shuffledList = new List<T>();

                // Use the RNGCryptoServiceProvider to create random zeros or 1
                RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

                // Create the byte array that serves asa                               
                byte[] container = new byte[1];

                // if the list exists
                if (list != null)
                {  
                    // we can't use the collection count it changes
                    int listCount = list.Count;

                    // now we have to 'Randomly' pull items and add them to the end results
                    for (int x = 0; x < listCount; x++)
                    {
                        // Fill the topOrBottom byteArray
                        crypto.GetBytes(container);

                        // Get the value of topOrBottom
                        object randomByte = container.GetValue(0);

                        // if the randomByte exists
                        if (NullHelper.Exists(randomByte))
                        {
                            // local
                            int randomValue = NumericHelper.ParseInteger(randomByte.ToString(), 0, -1);

                            // set the randomIndex to the modulas of the the listCount
                            int randomIndex = randomValue % list.Count;

                            // verify the index is in range (should always be true)
                            if ((randomIndex >= 0) && (randomIndex < list.Count))
                            {
                                // Add the card from the top half of the list
                                shuffledList.Add(list[randomIndex]);
                            }
                            
                            // Remove the item from the sourceList now that we have it
                            list.RemoveAt(randomIndex);
                        }
                    }
                }

                // return value
                return shuffledList;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
