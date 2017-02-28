

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

#endregion

namespace DataJuggler.Net
{

    #region class ProjectFile
    /// <summary>
    /// This object is used to keep track of files that were
    /// added to a project during a build.
    /// </summary>
    public class ProjectFile
    {
    
        #region Private Variables
        private string fullFilePath;
        private DataManager.ProjectTypeEnum projectType;
        private string fileName;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a ProjectFile object.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="projectType"></param>
        public ProjectFile(string fileName, DataManager.ProjectTypeEnum projectType)
        {
            // set the FileName
            this.FullFilePath = fileName;
            
            // Set the project type
            this.ProjectType = projectType;
        } 
        #endregion
        
        #region Methods

            #region SetFileName(string fullFilePath)
            /// <summary>
            /// This method sets the FileName for the fullFilePath.
            /// </summary>
            /// <param name="fullFilePath"></param>
            /// <returns></returns>
            private string SetFileName(string fullFilePath)
            {
                // initial value
                string fileName = "";
            
                try
                {
                    // create a file info object
                    FileInfo fi = new FileInfo(fullFilePath);
                    
                    // set the return value
                    fileName = fi.Name;
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return fileName;
            }
            #endregion
        
            #region ToString()
            /// <summary>
            /// this method returns the FileName when ToString is called.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                // return the file name when ToString is called
                return this.FullFilePath;
            } 
            #endregion
        
        #endregion
        
        #region Properties

            #region FileName
            /// <summary>
            /// This is the FileName only without the full path.
            /// </summary>
            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }
            #endregion
            
            #region FullFilePath
            /// <summary>
            /// This property gets or sets the full path of a
            /// the file being added.
            /// </summary>
            public string FullFilePath
            {
                get { return fullFilePath; }
                set 
                { 
                    // set the full File Path
                    fullFilePath = value;
                    
                    // set the fileName
                    this.FileName = SetFileName(fullFilePath);
                }
            }
            #endregion
        
            #region ProjectType
            /// <summary>
            /// This property is used to determine which 
            /// project new files should get added to.
            /// </summary>
            public DataManager.ProjectTypeEnum ProjectType
            {
                get { return projectType; }
                set { projectType = value; }
            } 
            #endregion
        
        #endregion
    
    } 
    #endregion
    
}
