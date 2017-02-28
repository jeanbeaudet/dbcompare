

#region using statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataJuggler.Win.Controls.Enumerations;
using DataJuggler.Core.UltimateHelper;

#endregion

namespace DataJuggler.Win.Controls
{

    #region class LabelTextBoxBrowserControl
    /// <summary>
    /// This control is used to make it easy to create a browse button on any Form or Control.
    /// </summary>
    public partial class LabelTextBoxBrowserControl : UserControl
    {

        #region Private Variables
         private string selectedPath;
         private string startPath;
         private string filter;
         private BrowseTypeEnum browseType;
         private int buttonWidth;
         private Image buttonImage;
         #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'LabelTextBoxBrowserControl' object.
        /// </summary>
        public LabelTextBoxBrowserControl()
        {
            // Create Controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion
        
        #region Events
            
            #region BrowseButton_Click(object sender, EventArgs e)
            /// <summary>
            /// event is fired when the 'BrowseButton' is clicked.
            /// </summary>
            private void BrowseButton_Click(object sender, EventArgs e)
            {
                // If we are browsing for a file
                if (this.BrowseType == BrowseTypeEnum.File)
                {
                    // Browse for a file
                    DialogHelper.ChooseFile(this.LabelTextBoxControl.GetTextBox(), this.Filter, this.SelectedPath);
                }
                // If browsing for a directory
                else if (this.BrowseType == BrowseTypeEnum.Folder)
                {
                    // Browse for a directory
                    DialogHelper.ChooseFolder(this.LabelTextBoxControl.GetTextBox(), this.SelectedPath);
                }
            }
        #endregion

            #region BrowseButton_MouseEnter(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Browse Button _ Mouse Enter
            /// </summary>
            private void BrowseButton_MouseEnter(object sender, EventArgs e)
            {
                // Call MouseEnter
                this.Cursor = Cursors.Hand;
            }
            #endregion
            
            #region BrowseButton_MouseLeave(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Browse Button _ Mouse Leave
            /// </summary>
            private void BrowseButton_MouseLeave(object sender, EventArgs e)
            {
                // Call MouseLeave
                this.Cursor = Cursors.Default;
            }
            #endregion
            
        #endregion

        #region Methods
            
            #region EnableBrowseButton(bool enable)
            /// <summary>
            /// This method Enable Browse Button
            /// </summary>
            public void EnableBrowseButton(bool enable)
            {
                // if enable is true
                if (enable)
                {
                    this.BrowseButton.Enabled = true;
                    this.BrowseButton.BackgroundImage = Properties.Resources.DarkBlueButton;
                }
                else
                {
                    this.BrowseButton.Enabled = false;
                    this.BrowseButton.BackgroundImage = Properties.Resources.DarkButton;
                }
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method  This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Default to enabled
                this.Enabled = true;

                // Handle which controls are enabled or disabled and change the colors accordingly
                UIControl(this.Enabled);
            }
            #endregion
            
            #region UIControl(bool enable)
            /// <summary>
            /// This method UI Control
            /// </summary>
            public void UIControl(bool enable)
            {
                // If the value for the property this.Enabled is true
                if (enable)
                {
                    // Use LemonChiffon if enable
                    this.LabelTextBox.LabelColor = Color.LemonChiffon;
                    this.BrowseButton.BackgroundImage = Properties.Resources.DarkBlueButton;
                }
                else 
                {
                    // Use DarkGray if disabled
                    this.LabelTextBox.LabelColor = Color.DarkGray;
                    this.BrowseButton.BackgroundImage = Properties.Resources.DarkButton;
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region BrowseType
            /// <summary>
            /// This property gets or sets the value for 'BrowseType'.
            /// </summary>
            public BrowseTypeEnum BrowseType
            {
                get { return browseType; }
                set { browseType = value; }
            }
            #endregion
            
            #region ButtonImage
            /// <summary>
            /// This property gets or sets the value for 'ButtonImage'.
            /// </summary>
            public Image ButtonImage
            {
                get
                { 
                    // set the return value
                    buttonImage = this.BrowseButton.BackgroundImage;

                    // return value
                    return buttonImage;
                }
                set 
                {
                    // set the value
                    buttonImage = value;

                    // Set the BackgroundImage
                    this.BrowseButton.BackgroundImage = value;
                }
            }
            #endregion
            
            #region ButtonWidth
            /// <summary>
            /// This property gets or sets the value for 'ButtonWidth'.
            /// </summary>
            public int ButtonWidth
            {
                get 
                { 
                    // Set the button width
                    buttonWidth = this.BrowseButton.Width;

                    // return value
                    return buttonWidth;
                }
                set 
                { 
                    // Set the value
                    buttonWidth = value;

                    // set the width
                    this.BrowseButton.Width = value;
                }
            }
            #endregion
            
            #region Filter
            /// <summary>
            /// This property gets or sets the value for 'Filter'.
            /// </summary>
            public string Filter
            {
                get { return filter; }
                set { filter = value; }
            }
        #endregion

            #region LabelTextBox
            /// <summary>
            /// This read only property returns the LabelTextBox from this control
            /// </summary>
            public LabelTextBoxControl LabelTextBox
            {
                get
                {
                    // Return the control
                    return this.LabelTextBoxControl;
                }
            }
            #endregion
            
            #region SelectedPath
            /// <summary>
            /// This property gets or sets the value for 'SelectedPath'.
            /// </summary>
            public string SelectedPath
            {
                get { return selectedPath; }
                set { selectedPath = value; }
            }
            #endregion
            
            #region StartPath
            /// <summary>
            /// This property gets or sets the value for 'StartPath'.
            /// </summary>
            public string StartPath
            {
                get { return startPath; }
                set { startPath = value; }
            }
        #endregion

        #endregion
            
    }
    #endregion

}
