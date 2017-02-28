

#region using statements

using System;
using System.Windows.Forms;

#endregion

namespace RandomBanner
{

    #region class SponsorControl
    /// <summary>
    /// This control is used to show the current months sponsor and info about
    /// how to sponsor a project.
    /// </summary>
    public partial class SponsorControl : UserControl
    {
        
        #region Private Variables
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'SponsorControl' object.
        /// </summary>
        public SponsorControl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Events
            
            #region Mouse_Enter(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Mouse _ Enter
            /// </summary>
            private void Mouse_Enter(object sender, EventArgs e)
            {
                // Change the cursor to a hand
                Cursor = Cursors.Hand;
            }
            #endregion
            
            #region Mouse_Leave(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Mouse _ Leave
            /// </summary>
            private void Mouse_Leave(object sender, EventArgs e)
            {
                // Change the cursor back to the default pointer
                Cursor = Cursors.Default;
            }
            #endregion
            
            #region SponsorButton_Click(object sender, EventArgs e)
            /// <summary>
            /// event is fired when the 'SponsorButton' is clicked.
            /// </summary>
            private void SponsorButton_Click(object sender, EventArgs e)
            {
                // Send the user to the Sponsors web site (or Audible.com in the case of The Libertarian Dictator Audio Book).
                System.Diagnostics.Process.Start(Sponsor.WebUrl);
            }
            #endregion
            
        #endregion
            
    }
    #endregion

}
