

#region using statements

using System;
using System.Windows.Forms;
using RandomBanner.Enumerations;
using System.Drawing;
using System.Xml.Serialization;
using DataJuggler.Core.UltimateHelper;
using DataJuggler.Core.RandomShuffler;

#endregion

namespace RandomBanner
{

    #region class BannerControl
    /// <summary>
    /// This class is used to show different banners
    /// </summary>
    public partial class BannerControl : UserControl
    {
        
        #region Private Variables
        private BannerEnum currentBanner;
        private BannerEnum applicationBanner;
        private RandomShuffler shuffler;
        private bool hovering;
        #endregion
        
        #region Constructor
        /// <summary>
        /// This control is used to show banner images that change every minute
        /// </summary>
        public BannerControl()
        {
            // Create controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Events
            
            #region Banner_Enter(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Banner _ Enter
            /// </summary>
            private void Banner_Enter(object sender, EventArgs e)
            {
                // Change the cursor to a hand
                Cursor = Cursors.Hand;

                // Show the back image
                GetBannerImage(CurrentBanner, ApplicationBanner, true);

                // Refresh the UI
                this.Refresh();
                Application.DoEvents();
            }
            #endregion
            
            #region Banner_Leave(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Banner _ Leave
            /// </summary>
            private void Banner_Leave(object sender, EventArgs e)
            {
                // Change the cursor back to the default pointer
                Cursor = Cursors.Default;

                   // Show the back image
                GetBannerImage(CurrentBanner, ApplicationBanner, false);
                
                // Refresh the UI
                this.Refresh();
                Application.DoEvents();
            }
            #endregion
            
            #region BannerControl_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'BannerControl' is clicked.
            /// </summary>
            private void BannerControl_Click(object sender, EventArgs e)
            {
                // Open the browser
                OpenBrowser(this.CurrentBanner);
            }
            #endregion
            
            #region BannerControl_MouseEnter(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Banner Control _ Mouse Enter
            /// </summary>
            private void BannerControl_MouseEnter(object sender, EventArgs e)
            {
                // turn the cursor to a hand
                Cursor = Cursors.Hand;

                // Set Hovering to true
                Hovering = true;

                // Display the curernt banner
                DisplayCurrentBanner();
            }
            #endregion
            
            #region BannerControl_MouseLeave(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Banner Control _ Mouse Leave
            /// </summary>
            private void BannerControl_MouseLeave(object sender, EventArgs e)
            {
                // revert back
                this.Cursor = Cursors.Default;

                 // Set Hovering to true
                Hovering = false;

                // Display the current banner not hovering
                DisplayCurrentBanner();
            }
            #endregion
        
            #region ChangeBannerTimer_Tick(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Change Banner Timer _ Tick
            /// </summary>
            private void ChangeBannerTimer_Tick(object sender, EventArgs e)
            {
                // Change the banner every 30 seconds
                this.ChangeBanner();
            }
            #endregion
            
        #endregion

        #region Methods

            #region ChangeBanner()
            /// <summary>
            /// This method Changes the current Banner
            /// </summary>
            public void ChangeBanner()
            {
                // do not change images while hovering, as it means someone is reading so let them finish
                // and then the next time this is called it will change
                if (!this.Hovering)
                {
                    // Shuffle ten times
                    this.Shuffler.Shuffle(10);

                    // get a tempBanner
                    BannerEnum tempBanner = GetRandomBanner();

                    do
                    {
                        // if the same banner was selected
                        if (tempBanner == CurrentBanner)
                        {
                            // get a tempBanner
                            tempBanner = GetRandomBanner();
                        }
                        else
                        {   
                            // Set the value for TempBanner
                            CurrentBanner = tempBanner;

                            // break out of the loop
                            break;
                        }

                    } while (true);
                }
            }
            #endregion
            
            #region DisplayCurrentBanner()
            /// <summary>
            /// This method Displays the Current Banner
            /// </summary>
            private void DisplayCurrentBanner()
            {
                // Get the banner image
                this.BannerImage.BackgroundImage = GetBannerImage(this.CurrentBanner, ApplicationBanner, Hovering);

                // Refresh the BannerImage
                this.BannerImage.Refresh();

                // Refresh this control
                this.Refresh();

                // If this object has a parent form
                if (this.ParentForm != null)
                {
                    // Refresh the ParentForm
                    this.ParentForm.Refresh();
                }
            }
            #endregion
            
            #region GetBannerImage(BannerEnum bannerEnum, BannerEnum applicationBanner, bool hover = false)
            /// <summary>
            /// This method returns the Banner Image
            /// </summary>
            private Image GetBannerImage(BannerEnum bannerEnum, BannerEnum applicationBanner, bool hover = false)
            {
                // initial value
                Image image = null;

                switch (bannerEnum)
                {
                    case BannerEnum.LibertarianDictator:

                        // if the value for hover is true
                        if (hover)
                        {
                            // return the back image
                            image = Properties.Resources.The_Libertarian_Dictator_Back;
                        }
                        else
                        {
                            // return the front image
                            image = Properties.Resources.The_Libertarian_Dictator;
                        }

                        // required
                        break;

                    case BannerEnum.XmlMirror:
                    case BannerEnum.DBCompare:

                        // if the value for hover is true
                        if (hover)
                        {
                            // if the calling app is DBCompare, show Xml Mirror
                            if (applicationBanner == BannerEnum.DBCompare)
                            {
                                // Show the hover image of Xml Mirror hover
                                image = Properties.Resources.Xml_Mirror_2_0_Hover;
                            }
                            // if the calling app is Xml Mirror show DB Compare
                            else
                            {
                                // Show the hover image of DB Compare
                                image = Properties.Resources.DB_Compare_Ad_Back;
                            }
                        }
                        else
                        {
                            // if the xmlMiror
                            if (applicationBanner == BannerEnum.DBCompare)
                            {
                                // Show the hover image of Xml Mirror hover
                                image = Properties.Resources.Xml_Mirror_2_0_Image;
                            }
                            else
                            {
                                // Show the hover image of DB Compare
                                image = Properties.Resources.DB_Compare_Ad;
                            }
                        }

                        // required
                        break;

                    case BannerEnum.Teethathon:

                        // if the value for hover is true
                        if (hover)
                        {
                            // return the back image
                            image = Properties.Resources.Teethathon_Ad_Back_2;
                        }
                        else
                        {
                            // return the front image
                            image = Properties.Resources.Teethathon_Ad;
                        }

                        // required
                        break;
                }

                // return value
                return image;
            }
            #endregion
            
            #region GetRandomBanner()
            /// <summary>
            /// This method returns a Random Banner
            /// </summary>
            public BannerEnum GetRandomBanner()
            {
                //// initial value
                BannerEnum randomBanner = BannerEnum.NotSet;

                // If the Shuffler object exists
                if (this.HasShuffler)
                {
                    // Create a random value of 1 - 60
                    int randomValue = this.Shuffler.PullNextItem(true, false, true);

                    // if greater than 40
                    if (randomValue > 40)
                    {
                        // set to XmlMirror
                        randomBanner = BannerEnum.Teethathon;
                    }
                    // if 21 - 40
                    else if (randomValue > 20)
                    {
                        // if this app is Xml Mirror
                        if (this.ApplicationBanner == BannerEnum.XmlMirror)
                        {
                            // set to DB Compare
                            randomBanner = BannerEnum.DBCompare;
                        }
                        else if (this.ApplicationBanner == BannerEnum.DBCompare)
                        {
                            // set to Xml Mirror
                            randomBanner = BannerEnum.XmlMirror;
                        }
                    }
                    // 1 - 20
                    else
                    {
                        // set to The Libertarian Dictator
                        randomBanner = BannerEnum.LibertarianDictator;
                    }
                }

                //// return value
                return randomBanner;
            }
            #endregion
            
            #region GetWebUrl(BannerEnum banner)
            /// <summary>
            /// This method returns the Web Url
            /// </summary>
            private string GetWebUrl(BannerEnum banner)
            {
                // initial value
                string webUrl = "";

                // determine the web url based upon the banner
                switch (banner)
                {
                    case BannerEnum.LibertarianDictator:
                    
                        // set the return value
                        webUrl = Sponsor.WebUrl;

                        // required
                        break;

                    case BannerEnum.DBCompare:

                        // set the return value
                        webUrl = "https://dbcompare.codeplex.com";

                        // required
                        break;

                    case BannerEnum.Teethathon:

                        // set the webUrl
                        webUrl = "https://www.gofundme.com/teeth-a-thon";

                        // required
                        break;
                }

                // return value
                return webUrl;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create a shuffler
                Shuffler = new RandomShuffler(1, 60, 10);

                // Shuffle ten times
                Shuffler.Shuffle(10);

                // Start with a random banner
                this.CurrentBanner = GetRandomBanner();
            }
            #endregion
            
            #region OpenBrowser(BannerEnum bannerEnum)
            /// <summary>
            /// This method open a browser window to the selected product (banner)
            /// </summary>
            private void OpenBrowser(BannerEnum bannerEnum)
            {
                // if the CurrentBanner exists
                if (this.HasCurrentBanner)
                {
                    // get the web url for the CurrentBanner
                    string webUrl = GetWebUrl(CurrentBanner);

                    // If the webUrl string exists
                    if (TextHelper.Exists(webUrl))
                    {
                        // set the webUrl
                        System.Diagnostics.Process.Start(webUrl);
                    }
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region ApplicationBanner
            /// <summary>
            /// This property gets or sets the value for 'ApplicationBanner'.
            /// </summary>
            public BannerEnum ApplicationBanner
            {
                get { return applicationBanner; }
                set { applicationBanner = value; }
            }
            #endregion
            
            #region CurrentBanner
            /// <summary>
            /// This property gets or sets the value for 'CurrentBanner'.
            /// </summary>
            [XmlIgnoreAttribute]
            public BannerEnum CurrentBanner
            {
                get { return currentBanner; }
                set 
                {
                    // set the return value
                    currentBanner = value;

                    // Display the current banner
                    DisplayCurrentBanner();
                }
            }
            #endregion
            
            #region HasCurrentBanner
            /// <summary>
            /// This property returns true if this object has a 'CurrentBanner'.
            /// </summary>
            public bool HasCurrentBanner
            {
                get
                {
                    // initial value
                    bool hasCurrentBanner = (this.CurrentBanner != BannerEnum.NotSet);
                    
                    // return value
                    return hasCurrentBanner;
                }
            }

        #endregion

            #region HasShuffler
            /// <summary>
            /// This property returns true if this object has a 'Shuffler'.
            /// </summary>
            public bool HasShuffler
            {
                get
                {
                    // initial value
                    bool hasShuffler = (this.Shuffler != null);
                    
                    // return value
                    return hasShuffler;
                }
            }
            #endregion
            
            #region Hovering
            /// <summary>
            /// This property gets or sets the value for 'Hovering'.
            /// </summary>
            public bool Hovering
            {
                get { return hovering; }
                set { hovering = value; }
            }
            #endregion
            
            #region Shuffler
            /// <summary>
            /// This property gets or sets the value for 'Shuffler'.
            /// </summary>
            public RandomShuffler Shuffler
            {
                get { return shuffler; }
                set { shuffler = value; }
            }
            #endregion
            
        #endregion
            
    }
    #endregion

}
