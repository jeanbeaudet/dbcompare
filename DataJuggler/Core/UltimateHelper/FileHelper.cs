

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

#endregion

namespace DataJuggler.Core.UltimateHelper
{

    #region class FileHelper
    /// <summary>
    /// This class helps with accessing Files
    /// </summary>
    public class FileHelper
    {

        #region Methods

            #region GetFileNameWithoutExtension(string fullName)
            /// <summary>
            /// This method returns the File Name Without Extension
            /// </summary>
            public static string GetFileNameWithoutExtension(string fullName)
            {
                // initial value
                string fileNameWithoutExtension = "";

                // Create a fileInfo object
                FileInfo fileInfo = new FileInfo(fullName);

                // Set the return value
                fileNameWithoutExtension = fileNameWithoutExtension = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));

                // return value
                return fileNameWithoutExtension;
            }
            #endregion

            #region GetFileNameWithoutExtensionEx(string fullName, ref string extension)
            /// <summary>
            /// This method returns the File Name Without Extension
            /// </summary>
            public static string GetFileNameWithoutExtensionEx(string fullName, ref string extension)
            {
                // initial value
                string fileNameWithoutExtension = "";

                // Create a fileInfo object
                FileInfo fileInfo = new FileInfo(fullName);

                // Set the return value
                fileNameWithoutExtension = fileNameWithoutExtension = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));

                // set the extension
                extension = fileInfo.Extension;

                // return value
                return fileNameWithoutExtension;
            }
            #endregion
            
            #region GetFiles(string directoryPath, bool removeExtension = false)
            /// <summary>
            /// This method returns a list of file names.
            /// </summary>
            /// <param name="directoryPath">The path to the Directory containing the files</param>
            /// <param name="filterExtension">If an extension is passed in, only files matching this extension
            /// will be returned.</param>
            /// <param name="removeExtension">If true, the extension is removed</param>
            /// <returns></returns>
            public static List<string> GetFiles(string directoryPath, string filterExtension = "", bool removeExtension = false)
            {
                // initial value
                List<string> files = new List<string>();

                // local
                string extension = "";

                // If the filePath string exists
                if ((TextHelper.Exists(directoryPath)) && (Directory.Exists(directoryPath)))
                {
                    // get the fileNames
                    string[] fileNames = Directory.GetFiles(directoryPath);

                    // if the fileNames exist
                    if ((fileNames != null) && (fileNames.Length > 0))
                    {
                        // if the value for removeExtension is true
                        if (removeExtension)
                        {
                            // get a tempList
                            List<string> tempNames = fileNames.ToList();

                            // Iterate the collection of string objects
                            foreach (string tempName in fileNames)
                            {
                                // get the name without the extension
                                string name = GetFileNameWithoutExtensionEx(tempName, ref extension);

                                // If the filterExtension string exists
                                if (TextHelper.Exists(filterExtension))
                                {
                                    // if the extension exists
                                    if (TextHelper.IsEqual(filterExtension, extension))
                                    {
                                        // Add this file
                                        files.Add(name);        
                                    }
                                }
                                else
                                {
                                    // Add this file
                                    files.Add(name);        
                                }
                            }
                        }
                        else
                        {
                            // set the return value
                            files = fileNames.ToList();

                            // if the filterExtension exists
                            if (TextHelper.Exists(filterExtension))
                            {
                                // get a tempList
                                List<string> tempNames = fileNames.ToList();

                                // Iterate the collection of string objects
                                foreach (string tempName in fileNames)
                                {
                                    // The name is not needed here, just getting the extension
                                    GetFileNameWithoutExtensionEx(tempName, ref extension);

                                    // If the filterExtension string exists
                                    if (TextHelper.Exists(filterExtension))
                                    {
                                        // if the extension exists
                                        if (TextHelper.IsEqual(filterExtension, extension))
                                        {
                                            // Add this file
                                            files.Add(tempName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // return value
                return files;
            }
            #endregion

        #endregion

    }
    #endregion

}
