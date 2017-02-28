

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Net
{

    #region class ProjectFileManager
    /// <summary>
    /// This class is used to keep track of new files that 
    /// were created during a build.
    /// </summary>
    public class ProjectFileManager
    {
        
        #region private variables
        private IList<ProjectFile> files;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'ProjectFileManager' object.
        /// </summary>
        public ProjectFileManager()
        {
            // perform initializations for this object
            Init();
        }
        #endregion
        
        #region Methods

            #region Init()
            /// <summary>
            /// Perform initializations for this object
            /// </summary>
            private void Init()
            {
                // create the NewFiles list
                this.Files = new List<ProjectFile>();
            }  
            #endregion
        
        #endregion
        
        #region Properties

            #region Files
            /// <summary>
            /// This property gets or sets the Files list.
            /// This list is a list of files that must be added to a project.
            /// </summary>
            public IList<ProjectFile> Files
            {
                get { return files; }
                set { files = value; }
            } 
            #endregion
        
        #endregion
        
    } 
    #endregion
    
}
