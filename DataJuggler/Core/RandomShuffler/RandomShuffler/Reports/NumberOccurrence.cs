

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Core.RandomShuffler.Reports
{

    #region class NumberOccurrence
    /// <summary>
    /// This class is used to keep track of how many teims a specific item is picked.
    /// </summary>
    public class NumberOccurrence
    {
        
        #region Private Variables
        private int number;
        private int count;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// Create an occurrence for a number
        /// </summary>
        /// <param name="number"></param>
        public NumberOccurrence(int number)
        {
            // store the value for Number
            this.Number = number;
        }
        #endregion

        #region Properties

            #region Count
            /// <summary>
            /// This property gets or sets the value for 'Count'.
            /// </summary>
            public int Count
            {
                get { return count; }
                set { count = value; }
            }
            #endregion
            
            #region Number
            /// <summary>
            /// This property gets or sets the value for 'Number'.
            /// </summary>
            public int Number
            {
                get { return number; }
                set { number = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
