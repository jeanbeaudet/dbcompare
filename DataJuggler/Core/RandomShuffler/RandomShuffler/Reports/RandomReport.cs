

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Core.RandomShuffler.Reports
{

    #region class RandomReport
    /// <summary>
    /// This class is used to report on how many of each items were choosen to test a shuffler.
    /// </summary>
    public class RandomReport
    {
        
        #region Private Variables
        private int totalItemsPulled;
        private List<NumberOccurrence> occurrences;
        #endregion

        #region RandomReport(int minValue, int maxValue)
        /// <summary>
        /// Create a new instance of a RandomReport
        /// </summary>
        public RandomReport(int minValue, int maxValue)
        {
            // Create
            this.Occurrences = new List<NumberOccurrence>();

            // iterate the values from minValue to maxValue
            for (int x = minValue; x <= maxValue; x++)
            {
                // create an occurrence
                NumberOccurrence occurrence = new NumberOccurrence(x);

                // Add this occurrence to the report
                this.Occurrences.Add(occurrence);
            }
        }
        #endregion

        #region Methods

            #region LogOccurrence(int itemPulled)
            /// <summary>
            /// This method returns the Occurrence
            /// </summary>
            public void LogOccurrence(int itemPulled)
            {
                // If the Occurrences object exists
                if (this.HasOccurrences)
                {
                    // Increment the total items pulled
                    this.TotalItemsPulled++;

                    // Increment this number
                    this.Occurrences[itemPulled - 1].Count++;
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region HasOccurrences
            /// <summary>
            /// This property returns true if this object has an 'Occurrences'.
            /// </summary>
            public bool HasOccurrences
            {
                get
                {
                    // initial value
                    bool hasOccurrences = (this.Occurrences != null);
                    
                    // return value
                    return hasOccurrences;
                }
            }
            #endregion
            
            #region Occurrences
            /// <summary>
            /// This property gets or sets the value for 'Occurrences'.
            /// </summary>
            public List<NumberOccurrence> Occurrences
            {
                get { return occurrences; }
                set { occurrences = value; }
            }
            #endregion
            
            #region TotalItemsPulled
            /// <summary>
            /// This property gets or sets the value for 'TotalItemsPulled'.
            /// </summary>
            public int TotalItemsPulled
            {
                get { return totalItemsPulled; }
                set { totalItemsPulled = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
