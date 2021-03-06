﻿    
     
#region using statements

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

#endregion

namespace DataJuggler.Net
{
  
    #region class CSharpClassWriter
    /// <summary>
    /// This class does the actual writing of all C# class objects.
    /// * All RAD Studio Class Builders extend this class.
    /// There are numerous examples in RADStudioClient.Builders folder.
    /// If you need to code generate a new type of class, create a new 
    /// builder and inherit from this class.
    /// </summary>
	public class CSharpClassWriter
	{  

		#region Private Variables
		private int indent;
		private bool hardcodecreatetable;
		private bool createtablefromxml;
		private bool createtablefromdatabase;
		private StreamWriter writer;
		private string failedreason;
		private bool businessObjectPass;
        private ProjectFileManager fileManager;
        #endregion
		
		#region Constructor
		/// <summary>
		/// Create a new instance of a CSharpClassWriter.
		/// </summary>
		/// <param name="fileManager">The fileManager is used to keep track of which 
        /// files were added during a build.
        /// </param>
		/// <param name="businessObjectPassArg">If true the business class is created, else the data class.
        /// This only applies to the RADStudioClassBuilder.
        /// </param>
		public CSharpClassWriter(ProjectFileManager fileManager, bool businessObjectPassArg)
		{
		    // set the FileManager 
		    this.FileManager = fileManager;
		
            // set the BusinessObjectPass property
		    this.BusinessObjectPass = businessObjectPassArg;
		}
		#endregion

		#region Methods

			#region BeginClassRegion(string className)
			/// <summary>
			/// This method writes a region with the className
            /// </summary>
			/// <param name="className"></param>
			private void BeginClassRegion(string className)
			{
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("#region class ");

				// Add Text To Line For StringBuilder
				sb.Append(className);
				
				// Write This Line 
				WriteLine(sb.ToString());

			}
		    #endregion

            #region BeginRegion(string regionText) void
            public void BeginRegion(string regionText)
			{
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("#region ");

				// Add Text To Line For StringBuilder
				sb.Append(regionText);

				// Write This Line 
				WriteLine(sb.ToString());

			}
			#endregion

            #region CapitalizeFirstChar(string word)
            /// <summary>
            /// This override capitalizes the first char.
            /// Use the other override for lowercase.
            /// </summary>
            /// <param name="word"></param>
            /// <returns></returns>
            public string CapitalizeFirstChar(string word)
            {
                // return the capitalized first letter of the word
                return CapitalizeFirstChar(word, false);
            }
            #endregion

            #region CapitalizeFirstCharEx(string word)
            /// <summary>
            /// This override capitalizes the first char.
            /// Use the other override for lowercase.
            /// </summary>
            /// <param name="word"></param>
            /// <returns></returns>
            public static string CapitalizeFirstCharEx(string word)
            {
                // call the override for this method
                return CapitalizeFirstCharEx(word, false);
            }
            #endregion

            #region CapitalizeFirstChar(string word, bool lowerCase)
			public string CapitalizeFirstChar(string word, bool lowerCase)
			{
				// If Null String
				if(String.IsNullOrEmpty(word))
				{
					// Return Null String
					return word;
				}

				// Create Char Array
				Char[] letters = word.ToCharArray();

				// if lower case
				if(lowerCase)
				{
				    // Capitalize First Character
				    letters[0] = Char.ToLower(letters[0]);
				}
				else
				{
				    // Capitalize First Character
				    letters[0] = Char.ToUpper(letters[0]);
				}
				
				// return new string
				return new string(letters);

			}
			#endregion

            #region CapitalizeFirstCharEx(string word, bool lowerCase)
            public static string CapitalizeFirstCharEx(string word, bool lowerCase)
            {
                // set the newWord
                string newWord = "";
            
                // If Null String
                if (String.IsNullOrEmpty(word))
                {
                    // Return Null String
                    return newWord;
                }

                // Create Char Array
                Char[] letters = word.ToCharArray();

                // if lower case
                if (lowerCase)
                {
                    // if this word is less than 3
                    if (word.Length < 3)
                    {
                        // go with all lower case here
                        newWord = word.ToLower();
                        
                        // return the newWord
                        return newWord;
                    }
                    else
                    {
                        // Capitalize First Character
                        letters[0] = Char.ToLower(letters[0]);
                    }   
                }
                else
                {
                    // Capitalize First Character
                    letters[0] = Char.ToUpper(letters[0]);
                }
                
                // set the newWord
                newWord = new string(letters);

                // return new string
                return newWord;

            }
            #endregion

            #region CaseString(string caseText)
            public string CaseString(string caseText)
			{
				// char Quote
				char Quote = '"';

				// Create StringBuilder
				StringBuilder sb = new StringBuilder("case ");

				// Append Quote
				sb.Append(Quote);
					
				// Append Text Passed In
				sb.Append(caseText);

				// Append Quote
				sb.Append(Quote);

				// Append Colon:
				sb.Append(":");

				// Return New String
				return sb.ToString();
			}
			#endregion

            #region CloneField(DataJuggler.Net.DataField field,string newObjectName)
            public string CloneField(DataJuggler.Net.DataField field,string newObjectName)
			{
                // local
                string fieldName = CapitalizeFirstChar(field.FieldName, true);

				// Create StringBuilder
				StringBuilder sb = new StringBuilder(newObjectName);
				sb.Append(".");
                sb.Append(fieldName);
				sb.Append(" = this.");
				sb.Append(field.FieldName);
				sb.Append(";");

				// Return New String
				return sb.ToString();
			}
		    #endregion
	
			#region CloseFile() void
			public void CloseFile()
			{
				// Close The File
				Writer.Close();
			}
			#endregion
            
            #region ConvertDataType(DataField field)
            /// <summary>
            /// This method converts the datatype of a field.
            /// For certain fields, enumerations are created, and
            /// the IsEnumeration method checks to see if the
            /// field passed in is in that list. If not then
            /// the datatype is parsed. 
            /// </summary>
            /// <param name="field"></param>
            /// <returns></returns>
            public string ConvertDataType(DataField field)
			{
		        // locals
                string dataType = "notsupported";
                
                // if field exists
                if(field != null)
                {
                    // is this field an enumeration field
                    if(field.IsEnumeration)
                    {
                        // set data type
                        dataType = field.EnumDataTypeName;
                    }
                    else
                    {
				        // determine data type
                        switch (field.DataType)
				        {

                            case DataManager.DataTypeEnum.Binary:

                                // set data type to int
                                dataType = "byte[]";

                                // required
                                break;
				        
					        case DataManager.DataTypeEnum.Integer:
					        case DataManager.DataTypeEnum.Autonumber:
					        
						        // set data type to int
						        dataType = "int";
        						
						        // required
						        break;
        						
					        case DataManager.DataTypeEnum.Percentage: 
					        case DataManager.DataTypeEnum.Double:
					        case DataManager.DataTypeEnum.Currency:
					        case DataManager.DataTypeEnum.Decimal:
					        
					            // set dataType to double
						        dataType =  "double";
						        
						        // required
						        break;
						        
					        case DataManager.DataTypeEnum.DateTime:
					        
					            // set dataType to DateTime
						        dataType =  "DateTime";

                                // required
                                break;
                                
					        case DataManager.DataTypeEnum.String:
						        
						        // set data type to string
						        dataType =  "string";

                                // required
                                break;
                                
					        case DataManager.DataTypeEnum.DataTable:
					        
					            // set data type to DataTable
						        dataType =  "DataJuggler.Net.DataTable";

                                // required
                                break;
						        
					        case DataManager.DataTypeEnum.Boolean:
					        case DataManager.DataTypeEnum.YesNo:
						        
						        // set dataType to bool 
						        dataType =  "bool";

                                // required
                                break;

                            case DataManager.DataTypeEnum.Guid:

                                // set dataType to bool 
                                dataType = "Guid";

                                // required
                                break;
                                
					        default: // Not Supported
					        
					            // set datatype to not supported
						        dataType =  "notsupported";

                                // required
                                break;
				        }
				    }
	            }
	            
	            // return value
	            return dataType;
			}
			#endregion

            #region CreateAddMethod(DataTable table) string
            /// <summary>
            /// Create CreateAddMethod
            /// </summary>
            /// <returns></returns>
            private string CreateAddMethod(DataTable table)
            {
                // Create SringBuilder
                StringBuilder sb = new StringBuilder();
                
                // Append method declaration
                sb.Append("public void Add(");
                
                // Append tableName
                sb.Append(GetClassName(table));
                
                // Append Space
                sb.Append(" ");
                
                // Append lower case variables
                sb.Append(CapitalizeFirstChar(GetClassName(table), true));
                
                // Append Closing )
                sb.Append(")");
                
                // return value
                return sb.ToString();
            }
            #endregion

            #region CreateFromFieldLine(DataField field,string sourceObjectName)
            /// <summary>
            /// This method is to create a BusinessObject from a DataObject
            /// </summary>
            /// <param name="field"></param>
            /// <param name="sourceObjectName"></param>
            /// <returns></returns>
            public string CreateFromFieldLine(DataField field, string sourceObjectName)
			{
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("this.");
				sb.Append(field.FieldName);
                sb.Append(" = ");
				sb.Append(sourceObjectName);
				sb.Append(".");
				sb.Append(field.FieldName);
				sb.Append(";");

				// Return New String
				return sb.ToString();
					
			}
		    #endregion

            #region CreateFile(string filePath, DataManager.ProjectTypeEnum projectType) void
            /// <summary>
			/// This method creates a file.
            /// If the path of the file being created does not exist, hence a 
            /// folder is missing, you will get an error. The error becomes
            /// you cannot write to a closed 
			/// </summary>
			/// <param name="filePath"></param>
			public void CreateFile(string filePath, DataManager.ProjectTypeEnum projectType)
			{
                try
                {
                    // Open StreamObject
                    Writer = File.CreateText(filePath);
                    
                    // if the FileManager exists
                    if (this.FileManager != null)
                    {
                        // create a project file object
                        ProjectFile projectFile = new ProjectFile(filePath, projectType);
                        
                        // add this file to the FileManager
                        this.FileManager.Files.Add(projectFile);
                    }
                }
                catch (Exception error)
                {
                    // set the error
                    this.FailedReason = error.ToString();
                    
                    // changed to raise the error, so the error thrown is correct,
                    // before it was cannot write to closed writer because I wasnt
                    // checking if the stream was open before writing.
                    throw error;
                }
			}
			#endregion
			
			#region CreateForEach(DataJuggler.Net.DataTable table)
			/// <summary>
			/// This method creates a foreach for each field in the table
			/// </summary>
			/// <param name="table"></param>
			/// <returns></returns>
			public string CreateForEach(DataJuggler.Net.DataTable table)
			{
				// Create NewObjectName; Example CategoriesObject
				string newobjectname = NewObjectName(GetClassName(table),true,"Object",true);
				
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("foreach(");
				sb.Append(GetClassName(table));
				
				// Append Space
				sb.Append(" ");
				
				// Append New Object Name
				sb.Append(newobjectname);
				
				// Append in this
				sb.Append(" in this)");
				
				// Return foreach([ClassName] [ClassNameObject] in this)
				return sb.ToString();
			}
			#endregion

			#region CreateGetText(DataField field) string
			internal string CreateGetText(DataField field)
			{
				// Create StringBuild and Seed with word return
				StringBuilder sb = new StringBuilder("return ");

				// Append fieldName
                string fieldName = CapitalizeFirstChar(field.FieldName, true);
				sb.Append(fieldName);

				// Now Add Closing Semicolon
				sb.Append(";");

				// Return Line
				return sb.ToString();
			}
			#endregion 

			#region CreateSetText(DataField field) string
			internal string CreateSetText(DataField field)
			{
				// Create StringBuild and Seed with word return
                string fieldName = CapitalizeFirstChar(field.FieldName, true);
				StringBuilder sb = new StringBuilder(fieldName);

				// Append = value
				sb.Append(" = value");

				// Now Add Closing Semicolon
				sb.Append(";");

				// Return Line
				return sb.ToString();

			}
			#endregion
			
			#region CreateSelectSQL()
			public void CreateSelectSQL()
			{
				// Write Blank Line
				WriteLine();
				
				// Write Comment Create sql Statement 
				WriteComment("Create sql Statement");
				
				string Line = "string sql = this.table.SQLGenerator.CreateSelectSQL(this.table, fieldName, fieldValue);";
				WriteLine(Line);
				
			}
			#endregion

			#region DatabaseConstructor(string ClassName)
			private string DatabaseConstructor(string className)
			{
				// Build String For Default Constructor
				StringBuilder sb = new StringBuilder("public ");

				// Append ClassName
				sb.Append(className);

				// Append Parenthese with Parameter For xmlFileName
				sb.Append("(DataJuggler.Net.Database database,string tableName)");

				// Return String 
				return sb.ToString();

			}
			#endregion

			#region DefaultConstructor(string className)
			private string DefaultConstructor(string className)
			{
				// Build String For Default Constructor
				StringBuilder sb = new StringBuilder("public ");

				// Append ClassName
				sb.Append(className);

				// Append Parenthese
				sb.Append("()");
				
				// Return String 
				return sb.ToString();

			}
			#endregion
			
			#region EndRegion() void
			/// <summary>
			/// Writes the #endregion directive.
			/// </summary>
			public void EndRegion()
			{
				// Write This Line 
				WriteLine("#endregion");
			}
			#endregion

            #region FormatClassName(string tableName)
            /// <summary>
            /// This method formats class names and removes any
            /// prefixes to tables.
            /// A possible exception here is a table name that starts 
            /// with a lower case letter (bad naming convention in my opinion)
            /// and the name is 5 characters or less. Otherwise summaries 
            /// like tblProperty will return Property.
            /// </summary>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public string FormatClassName(string tableName)
            {
                // initial value
                string className = "";

                // local
                int firstCapitalLetterIndex = -1;

                // check for a prefix
                bool hasPrefix = DataTable.CheckIfNameHasPrefix(tableName, ref firstCapitalLetterIndex);

                // if the table does have a prefix
                if ((hasPrefix) && (firstCapitalLetterIndex > 0))
                {
                    // remove the prefix
                    className = tableName.Substring(firstCapitalLetterIndex);
                }
                else
                {
                    // set the class name
                    className = CapitalizeFirstChar(tableName, false);
                }

                // return value
                return className;
            } 
            #endregion

            #region FormatClassNameEx(string tableName)
            /// <summary>
            /// This method formats class names and removes any
            /// prefixes to tables.
            /// A possible exception here is a table name that starts 
            /// with a lower case letter (bad naming convention in my opinion)
            /// and the name is 5 characters or less. Otherwise summaries 
            /// like tblProperty will return Property.
            /// </summary>
            /// <param name="tableName"></param>
            /// <returns></returns>
            public static string FormatClassNameEx(string tableName)
            {
                // initial value
                string className = tableName;

                // Capitalize First Character
                className = CapitalizeFirstCharEx(tableName);

                // Remove Any Spaces In ClassName
                className = className.Replace(" ", "");

                // Remove Any Underscores In ClassName
                className = className.Replace("_", "");
                
                // local
                int firstCapitalLetterIndex = -1;

                // check for a prefix
                bool hasPrefix = DataTable.CheckIfNameHasPrefix(tableName, ref firstCapitalLetterIndex);

                // if the table does have a prefix
                if ((hasPrefix) && (firstCapitalLetterIndex > 0))
                {
                    // remove the prefix
                    className = tableName.Substring(firstCapitalLetterIndex);
                }
                else
                {
                    // set the class name
                    className = CapitalizeFirstCharEx(className);
                }

                // return value
                return className;
            }
            #endregion

            #region GetClassName(DataTable table)
            /// <summary>
            /// Get the class name for a table
            /// </summary>
            /// <returns></returns>
            public string GetClassName(DataTable table)
            {
                // initial value
                string className = "";
                
                // if table exists
                if ((table != null) && (!String.IsNullOrEmpty(table.Name)))
                {
                    // get a temp variable
                    string tableName = table.Name;
                
                    // format the class name
                    className = FormatClassName(tableName);
                }
                
                // return value
                return className;
            }
            #endregion

            #region GetClassFileName(DataTable dataTable)
            /// <summary>
            /// This method gets the file name
            /// </summary>
            /// <param name="dataTable"></param>
            /// <returns></returns>
            private string GetClassFileName(DataTable dataTable)
            {
                // get className
                string className = GetClassName(dataTable);

                // Create StringBuilder
                StringBuilder sb = new StringBuilder(className);

                if (this.BusinessObjectPass)
                {
                    // append .business.cs
                    sb.Append(".business.cs");
                }
                else
                {
                    // append .data.cs
                    sb.Append(".data.cs");
                }

                // classFileName
                string classFileName = sb.ToString();
                return classFileName;
            } 
            #endregion

            #region GetCompareLine(DataJuggler.Net.DataTable table, string primaryKeyName)
            /// <summary>
            /// This method writes the line in the GetIndex method to check for the match
            /// </summary>
            /// <param name="table"></param>
            /// <param name="primaryKeyName"></param>
            /// <returns></returns>
            public string GetCompareLine(DataJuggler.Net.DataTable table, string primaryKeyName)
			{
				// Create New Object Name
				string newobjectname = NewObjectName(GetClassName(table),true,"Object",true);
				
				// Create String Builder	
				StringBuilder sb = new StringBuilder("if(");
				
				// Append newobjectname
				sb.Append(newobjectname);
				
				// Append .
				sb.Append(".");
				
				// Append PrimaryKeyName
				sb.Append(primaryKeyName);
				
				// Append = 
				sb.Append(" == ");
				
				// get the parameterName
				string parameterName = CSharpClassWriter.LowerCaseFirstCharEx(primaryKeyName);
				
				// Append PrimaryKeyName
				sb.Append(parameterName);
				
				// Append Closing Paren
				sb.Append(")");
				
				// Return New String
				return sb.ToString();
			}
			#endregion

            #region GetCompareLine2(string newObjectName)
            public string GetCompareLine2(string newObjectName)
			{	
				// Create String Builder	
				StringBuilder sb = new StringBuilder("if(");
				
				// Append newobjectname
				sb.Append(newObjectName);
				
				// Append .
				sb.Append(".Changes)");
				
				// Return New String
				return sb.ToString();
			}
			#endregion
            
			#region GetIndexDeclaration(string dataType, string primaryKeyFieldName)
			public string GetIndexDeclaration(string dataType, string primaryKeyFieldName)
			{
			    // we need to convert to lowercase variable name
			    string paramName = CapitalizeFirstChar(primaryKeyFieldName, true);
			
				// Creat StringBuilder
				StringBuilder sb = new StringBuilder("public int GetIndex(");
                sb.Append(dataType);
				sb.Append(" ");
				string parameterName = CSharpClassWriter.LowerCaseFirstCharEx(primaryKeyFieldName);
                sb.Append(parameterName);
				sb.Append(")");
				
				// Return IndexRegion 
				return sb.ToString();
			}
			#endregion

            #region GetIndexRegion(string dataType, string primaryKeyFieldName)
            /// <summary>
            /// This method creates the GetIndex method.
            /// </summary>
            /// <param name="dataType"></param>
            /// <param name="primaryKeyFieldName"></param>
            /// <returns></returns>
            public string GetIndexRegion(string dataType, string primaryKeyParameterName)
			{
				// Creat StringBuilder
				StringBuilder sb = new StringBuilder("GetIndex(");
				sb.Append(dataType);
				sb.Append(" ");
				primaryKeyParameterName = CSharpClassWriter.LowerCaseFirstCharEx(primaryKeyParameterName);
				
				// changed to add the lowercase for the parameter
				sb.Append(CSharpClassWriter.LowerCaseFirstCharEx(primaryKeyParameterName));
				sb.Append(")");
				
				// Return IndexRegion 
				return sb.ToString();
			}
			#endregion

			#region GetReferenceLine(Reference Ref) string
			internal string GetReferenceLine(Reference Ref)
			{

				// Begin Building Property
				StringBuilder sb = new StringBuilder("using ");

				// Append Reference Name
				sb.Append(Ref.ReferenceName);

				// Append Closing Semicolon ;
				sb.Append(";");

				// Return Line
				return sb.ToString();

			}
			#endregion

			#region GetRegionLine(DataField field) string +1 override

				#region GetRegionLine(DataField field) string
				internal string GetRegionLine(DataField field)
				{
						
					// Get dataType
                    string datatype = ConvertDataType(field);

					// Begin Building Property
					StringBuilder sb = new StringBuilder(datatype);

					// Append Space
					sb.Append(" ");

					// String fieldName
					sb.Append(field.FieldName);

					// Write A New Line
					return sb.ToString();

				}
				#endregion

				#region GetRegionLine(DataField field,string dataType) string
				internal string GetRegionLine(DataField Field,string DataType)
				{
						
					// Begin Building Property
					StringBuilder sb = new StringBuilder(DataType);

					// Append Space
					sb.Append(" ");

					// String fieldName
					sb.Append(Field.FieldName);

					// Write A New Line
					return sb.ToString();

				}
				#endregion

			#endregion

			#region GetPropertyLine(DataField field) string +1 override

				#region internal string GetPropertyLine(DataField field)
				internal string GetPropertyLine(DataField field)
				{
					// Set Scope Text
					string scope = GetScopeText(field.Scope);

					// Begin Building Property
					StringBuilder sb = new StringBuilder(scope);

					// Get dataType
					string datatype = ConvertDataType(field);

					// Append dataType
					sb.Append(datatype);

					// Append Space
					sb.Append(" ");

					// String fieldName
					sb.Append(field.FieldName);

					// Write A New Line
					return sb.ToString();

				}
				#endregion

				#region internal string GetPropertyLine(DataField field, string dataType)
				internal string GetPropertyLine(DataField Field, string DataType)
				{
					// Set Scope Text
					string scope = GetScopeText(Field.Scope);

					// Begin Building Property
					StringBuilder sb = new StringBuilder(scope);

					// Append dataType
					sb.Append(DataType);

					// Append Space
					sb.Append(" ");

					// String fieldName
					sb.Append(Field.FieldName);

					// Write A New Line
					return sb.ToString();

				}
				#endregion

			#endregion

			#region GetScopeText(DataManager.Scope Scope) string
			public string GetScopeText(DataManager.Scope Scope)
			{
				// Until Scope Is Implemented Make Public
				return "public ";
			}
			#endregion

			#region IfFieldHasChanges(string fieldName, string ClassName)
			public string IfFieldHasChanges(string FieldName, string ClassName)
			{
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("if(this.Initial");
				sb.Append(ClassName);
				sb.Append(".");
				sb.Append(FieldName);
				sb.Append(".ToString() != fieldValue)");

				// Return Line
				return sb.ToString();
					
			}
			#endregion
			
			#region InitializeStrings(List<DataField> fields)
			public void InitializeStrings(List<DataField> fields)
			{
				// Write Comment Initialize All Strings
				WriteComment("Initialize All Strings");
				
				// Create StringBuilder Object
				StringBuilder sb;
					
				// Loop Through Each field
				foreach(DataJuggler.Net.DataField field in fields)
				{
					// If This field Is A String 
					if((field.DataType == DataManager.DataTypeEnum.String) || (field.DataType == DataManager.DataTypeEnum.DateTime))
					{
						// Create Line
						sb = new StringBuilder(field.FieldName);
						sb.Append(" = ");
						sb.Append('"');
						sb.Append('"');
						sb.Append(";");	
						WriteLine(sb.ToString());
					}
				}
			}
			#endregion

			#region IndentString(string StringToIndent) string
			private string IndentString(string StringToIndent)
			{
				//Create String Builder
				StringBuilder sb = new StringBuilder();

				int NumberSpaces = Indent * 4;

				// PadLeft Spaces Up To Indent Number
				for(int x = 0;x < NumberSpaces;x++)
				{
					sb.Append(" ");
				}

				sb.Append(StringToIndent);

				return sb.ToString();
			}
			#endregion
            
			#region LoadField(DataJuggler.Net.DataField field)
			public string LoadField(DataJuggler.Net.DataField Field)
			{
				// String Builder  
				StringBuilder sb = new StringBuilder("this.");

				sb.Append(Field.FieldName.ToLower());

				sb.Append(" = ");

				string line = ParseFieldValue(Field);

				sb.Append(line);

				sb.Append(";");

				return sb.ToString();
			}
			#endregion
			
			#region LoadMethodRegion(int OverloadCount)
			public string LoadMethodRegion()
			{
				// Create StringBuilder
				return "Load(DataJuggler.Net.DataRow row) +2 overides";
			}
			#endregion
			
			#region LoadMethodFieldRegion()
			public string LoadMethodFieldRegion()
			{
				// Load 
				return "Load(string fieldName, string fieldValue, AccessDatabaseConnector DBConnector)";
			}
			#endregion
			
			#region LoadMethodFieldDeclaration()
			public string LoadMethodFieldDeclaration()
			{
				// String To Load This field
				return "public void Load(string fieldName, string fieldValue, AccessDatabaseConnector DBConnector)";
			}
			#endregion

            #region LowerCaseFirstChar(string word)
            /// <summary>
            /// This override lowerizes the first char.
            /// Use the other override for lowercase.
            /// </summary>
            /// <param name="word"></param>
            /// <returns></returns>
            public string LowerCaseFirstChar(string word)
            {
                // return the lowerized first letter of the word
                return CapitalizeFirstChar(word, true);
            }
            #endregion

            #region LowerCaseFirstCharEx(string word)
            /// <summary>
            /// This override lowerizes the first char.
            /// Use the other override for lowercase.
            /// </summary>
            /// <param name="word"></param>
            /// <returns></returns>
            public static string LowerCaseFirstCharEx(string word)
            {
                // call the override for this method
                return CapitalizeFirstCharEx(word, true);
            }
            #endregion

			#region NewObjectName(string ClassName, bool LowerCase, string AppendText, bool AppendToEnd) string
			public string NewObjectName(string ClassName, bool LowerCase, string AppendText, bool AppendToEnd)
			{
					
				// Create New StringBuilder Object
				StringBuilder sb = new StringBuilder();

				// If Initial = True
				if(!AppendToEnd)
				{
					// Start With Append Text
					sb.Append(AppendText);

					// Now Append ClassName
					sb.Append(ClassName);
				}
				else
				{
					// Append ClassName
					sb.Append(ClassName);

					// Now Append AppendText
					sb.Append(AppendText);
				}
				
				// string newobjectname
				string newObjectName = sb.ToString();
				
				// Replace Any Spaces
				newObjectName = newObjectName.Replace(" ","");
				
				// If This Is A Private Variable Or Not
				if(LowerCase)
				{
                    // Capitalize the first character
                    return CapitalizeFirstChar(newObjectName, true);
				}
				else
				{
                    // Return newObjectName
                    return newObjectName;
				}		
			}
			#endregion

			#region NullField(DataJuggler.Net.DataField field)
			public string NullField(DataJuggler.Net.DataField Field)
			{
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("if(this.");
				sb.Append(Field.FieldName);
				sb.Append(" != null)");
				return sb.ToString();
			}
			#endregion

            #region ParseFieldValue(DataJuggler.Net.DataField field)
            /// <summary>
            /// Parse the field value
            /// </summary>
            /// <param name="field"></param>
            /// <returns></returns>
            public string ParseFieldValue(DataJuggler.Net.DataField field)
			{
				// Create StringBuilder Object
				StringBuilder sb = new StringBuilder();

				switch(field.DataType)
				{
					case DataManager.DataTypeEnum.Autonumber:
						sb.Append("row.ParseStringToInteger(row.Fields[");
						sb.Append(field.Index);
						sb.Append("].fieldValue)");
						return sb.ToString();
						// case DataTypeEnum.Currency:
						//	break;
					case DataManager.DataTypeEnum.DateTime:
						sb.Append("row.Fields[");
						sb.Append(field.Index);
						sb.Append("].fieldValue");
						return sb.ToString();
					case DataManager.DataTypeEnum.Double:
					case DataManager.DataTypeEnum.Currency:
						sb.Append("row.ParseStringToDouble(row.Fields[");
						sb.Append(field.Index);
						sb.Append("].fieldValue)");
						return sb.ToString();
					case DataManager.DataTypeEnum.Integer:
						sb.Append("row.ParseStringToInteger(row.Fields[");
						sb.Append(field.Index);
						sb.Append("].fieldValue)");
						return sb.ToString();
						// case DataTypeEnum.Percentage:
						//	break;
					case DataManager.DataTypeEnum.String:
						sb.Append("row.Fields[");
						sb.Append(field.Index);
						sb.Append("].fieldValue");
						return sb.ToString();
						//case DataTypeEnum.YesNo:
						//	break;
					default:
						return "void";
				}
			}
			#endregion
			
			#region SaveObject(DataJuggler.Net.DataTable table)
			/// <summary>
			/// This method writes the call to save
            /// success = 
			/// </summary>
			/// <param name="table"></param>
			/// <returns></returns>
			public string SaveObject(DataJuggler.Net.DataTable table)
			{
				// Create New Object Name
				string newobjectname = NewObjectName(GetClassName(table),false,"Object",true);
				
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("success = ");
				
				// Append New ObjectName
				sb.Append(newobjectname);
				
				// Append .Save(DBConnector);
				sb.Append(".Save(DBConnector);");
				
				// so this can be debugged.
				string temp = sb.ToString();
				
				// return SaveObject
				return temp;
												
			}
			#endregion
				
			#region UpdateLine(DataJuggler.Net.DataField field)
			public string UpdateLine(DataJuggler.Net.DataField Field)
			{
				// Create StringBuilder To Update This field
				StringBuilder sb = new StringBuilder("dataRow.Fields[");
				sb.Append(Field.Index.ToString());
				sb.Append("].UpdateFieldValue(this.");
				sb.Append(Field.FieldName);
				sb.Append(".ToString(),this.FieldHasChanges(");
				sb.Append('"');
				sb.Append(Field.FieldName);
				sb.Append('"');
				sb.Append(",this.");
				sb.Append(Field.FieldName);
				sb.Append(".ToString()));");

				// Return The New String
				return sb.ToString();
			}
			#endregion

            #region Validate(DataManager dataManager) bool
            /// <summary>
            /// Validate the project before building
            /// </summary>
            /// <param name="dataManager"></param>
            /// <returns></returns>
            public bool Validate(DataManager dataManager)
			{
				// Verify DataManager Class Is Not Excluded
				if(dataManager.Exclude)
				{
					this.FailedReason = "This DataManager has the 'Exclude' property set to true.";
					return false;
				}

				// Verify ClassFileName Is Set
				if(String.IsNullOrEmpty(dataManager.ClassFileName))
				{
					this.FailedReason = "The 'ClassFileName' can not be null.";
					return false;
				}

				// Verify That The Path For The File Exists
				if(!dataManager.ValidPath(dataManager.ProjectFolder))
				{
					this.FailedReason = "The specified folder does not exist.";
					return false;
				}

				// there is not a failed reason
				this.FailedReason = null;

                // Return true;
				return true;
			}
			#endregion

			#region WriteAddMethod(DataTable table)
			private void WriteAddMethod(DataJuggler.Net.DataTable table)
			{
				// Begin Region For AddMethod
				BeginRegion("Add() void");

				// Write Line For The Add Method
				string addMethod = CreateAddMethod(table);
				WriteLine(addMethod);

				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;

				// Write Comment For Add This Item To This Collection
				WriteComment("Add This Item To This Collection");

				// Add This Item To This Collection
				string addItemToList = "this.List.Add(" + CapitalizeFirstChar(GetClassName(table), true) + ");";
				WriteLine(addItemToList); 

				// Decrease Indent;
				Indent--;

				// Write Close Bracket
				WriteCloseBracket();

				// Write End Region
				EndRegion();
					
			}
			#endregion				
			
            #region WriteAllClassesToSingleFile()
            /// <summary>
			/// This method writes all data classes to a single file
			/// </summary>
            private void WriteAllClassesToSingleFile(DataManager dataManager)
            {
                // Create File
                string fullPath = dataManager.ReturnFullPath(dataManager.ClassFileName);

                // if this is not the business object pass
                // or if the file exists do not write the file
                if ((!this.BusinessObjectPass) || (!System.IO.File.Exists(fullPath)))
                {
                    // Create File
                    this.CreateFile(fullPath, DataManager.ProjectTypeEnum.ObjectLibrary);

                    // Write References
                    this.WriteReferences(dataManager.References);

                    // Write Blank Line & NameSpace
                    WriteLine();

                    // Write NameSpace
                    WriteNamespace(dataManager.NamespaceName);

                    // Write OpenBracket
                    WriteOpenBracket();

                    // Indent
                    Indent++;

                    // Write Class For Every Database
                    foreach (Database DB in dataManager.Databases)
                    {
                        // Write Classes
                        WriteClasses(DB.Tables, dataManager.NamespaceName);
                    }

                    // Write Blank Line
                    WriteLine();

                    // Decrease Indent
                    Indent--;

                    // Write CloseBracket
                    WriteCloseBracket();

                    // Close The File
                    this.CloseFile();
                }
            }
            #endregion

			#region WriteChangesProperty(DataJuggler.Net.DataTable table) void
			private void WriteChangesProperty(DataJuggler.Net.DataTable table,DataJuggler.Net.DataField Field)
			{
				// locals
				string PropertyLine = null;
				string RegionLine = null;

				// Create StringBuilder
				StringBuilder sb = new StringBuilder();

				// Write A NewLine
				WriteLine();

				// increment Indent
				Indent++;

				// string newobjectname
				string newobjectname = NewObjectName(GetClassName(table),false,"Initial",false);

				// Start A New Region For This PropertyLine
				RegionLine = GetRegionLine(Field);
				BeginRegion(RegionLine);

				// Get PropertyLineText
				PropertyLine = GetPropertyLine(Field);

				// Write The PropertyLine
				WriteLine(PropertyLine);

				// Write An OpenBracket
				WriteOpenBracket();

				// Increment Indent
				Indent++;

				// If ReadOnly or ReadWrite 
				if(WriteGet(Field))
				{
							
					// Write The Word Get
					WriteLine("get");

					// Write An OpenBracket
					WriteOpenBracket();

					// Increment Indent
					Indent++;
					
					// Write Initial Changes
					WriteInitialChanges(table);
					
					// Write Delete Changes
					WriteDeleteChanges(table);
					
					// Write If Statement For Each field
					foreach(DataField TField in table.Fields)
					{
						WriteFieldChangesGet(TField, table);
					}

					// Write Blank Line
					WriteLine();

					// Write Comment No Changes
					WriteComment("No Changes");

					// Write Return False
					WriteLine("return false;");

					// Decrement Indent
					Indent--;

					// Write CloseBracket
					WriteCloseBracket();
				}

				// If ReadWrite or WriteOnly
				if(WriteSet(Field))
				{
					// Write The Word set
					WriteLine("set");

					// Write An OpenBracket
					WriteOpenBracket();

					// Increment Indent
					Indent++;

					// Write Comment For Clone Object Again To Reset Changes
					WriteComment("Clone Object Again; Can Only Be Reset To False (No Changes)");

					// This Resets The OriginalNewObjects
					sb = new StringBuilder("this.");
					sb.Append(newobjectname);
					sb.Append(" = this.Clone();");
					WriteLine(sb.ToString());

					// Decrement Indent
					Indent--;

					// Write A CloseBracket
					WriteCloseBracket();
				}

				// Decrement Indent
				Indent--;

				// Write A CloseBracket
				WriteCloseBracket();

				// EndRegion
				EndRegion();

				// decrement Indent
				Indent--;
			}
			#endregion

			#region WriteClass(DataTable table)
			private void WriteClass(DataTable table)
			{		
				// Write Blank Line
				WriteLine();

				// set class name
				string className = GetClassName(table);
				
				// Begin Region For This Class
				BeginClassRegion(className);
				
				// If the table should be Serialized
				if ((table.Serializable) && (this.BusinessObjectPass))
				{
				    // Write Serializable
				    WriteSerializable();
				}

				// Write Class Line -- example: public class ThisClass
				WriteClassLine(className);

				// Write Open Bracket
				WriteOpenBracket();

				// Write Blank Line
				WriteLine();

				// Increase Indent
				Indent++;
						
				// Write Private Variables
				WritePrivateVariables(table);
						
				// Write Blank Line
				WriteLine();

				// only write the constructor for the business object pass
				if(this.BusinessObjectPass)
				{
				    // Write Constructors
				    WriteConstructor(table);
				    
                    // Write Blank Line
                    WriteLine();
				}

           	    // Write Methods Section
			    WriteMethods(table);

			    // Write Blank Line
			    WriteLine();
			
				// Write Properties
				WriteProperties(table);

				// Write Blank Line
				WriteLine();

				//Decrease Indent
				Indent--;

				// WriteCloseBracket
				WriteCloseBracket();
						
				// End Region For The Class
				EndRegion();
			}
			#endregion

            #region WriteClasses(List<DataTable> tables, string nameSpaceName)
            /// <summary>
            /// This method writes the classes
            /// </summary>
            /// <param name="tables"></param>
            /// <param name="nameSpaceName"></param>
            private void WriteClasses(List<DataTable> tables, string nameSpaceName)
			{	
				// Write Each Class
				foreach(DataJuggler.Net.DataTable table in tables)
				{
                    // must set the ObjectLibraryNameSpace for each table here
                    table.ObjectNameSpaceName = nameSpaceName;
				
					// Write This Class
					WriteClass(table);
				}
			}
			#endregion

			#region WriteClassLine
			private void WriteClassLine(string ClassName)
			{
				// Build Class Line
				StringBuilder sb = new StringBuilder("public partial class ");

				// Append ClassName
				sb.Append(ClassName);

				// Write Line For This Class
				WriteLine(sb.ToString());
			}
			#endregion

            #region WriteCloneMethod(DataTable table, bool collectionClass = false)
            /// <summary>
            /// Update for version 5, the CloneMethod is now created in the Business class only
            /// </summary>
            /// <param name="table"></param>
			private void WriteCloneMethod(DataTable table, bool collectionClass = false)
			{
				// Begin Region For Clone
				BeginRegion("Clone()");

				// Build Class Line
				StringBuilder sb = new StringBuilder("public ");
	                
				// Append ClassName
				sb.Append(GetClassName(table));

                // if this is a collectionClass
                if (collectionClass)
                {
                    // Add the world Collection
                    sb.Append("Collection");
                }

				// Append MethodName: Clone
				sb.Append(" Clone()");

				// Write Line For Clone
				WriteLine(sb.ToString());

				// Write Open Bracket
				WriteOpenBracket(true);

				// Create Name For New Object
				string newobjectname = NewObjectName(GetClassName(table),true,"New",false);

                // if this is a collectionClass
                if (collectionClass)
                {
                    // Add the world Collection to the newObjectName
                    newobjectname = newobjectname + "Collection";
                }

				// Write Comment Create New Object
				WriteComment("Create New Object");

				// Write Create New Object Line
				sb = new StringBuilder(GetClassName(table));

                // if this is a collectionClass
                if (collectionClass)
                {
                    // Add the world Collection
                    sb.Append("Collection");
                }

				sb.Append(" ");
				sb.Append(newobjectname);
				sb.Append(" = (");
				sb.Append(GetClassName(table));

                // if this is a collectionClass
                if (collectionClass)
                {
                    // Add the world Collection to the newObjectName
                    sb.Append("Collection");
                }
                
                // append close paren and finish the statement
				sb.Append(") this.MemberwiseClone();");
				
                // Now write out the clone line
                WriteLine(sb.ToString());

				// Write Blank Line
				WriteLine();

				// Write Comment For Return New Object
				WriteComment("Return Cloned Object");

				// Return Cloned Object
				sb = new StringBuilder("return ");
				sb.Append(newobjectname);
				sb.Append(";");
				WriteLine(sb.ToString());

				// Write Close Bracket
				WriteCloseBracket(true);

				// Write EndRegion
				EndRegion();
			}
			#endregion

            #region WriteClose Bracket
            
                #region WriteCloseBracket() void
                /// <summary>
                /// This method writes the close bracket }
                /// </summary>
                public void WriteCloseBracket()
                {
                    // Write CloseBracket
                    WriteLine("}");
                }
                #endregion

                #region WriteCloseBracket(bool decreaseIndent) void
                /// <summary>
                /// This method writes the close bracket }
                /// and decreases identity
                /// </summary>
                public void WriteCloseBracket(bool decreaseIndent)
                {
                    // if decrease identity
                    if (decreaseIndent)
                    {
                        // Decrease Indent
                        Indent--;
                    }
                    
                    // Write CloseBracket
                    WriteLine("}");
                }
                #endregion 
            
            #endregion
	
			#region WriteComment() void
			/// <summary>
			/// This method writes a comment by passing in the text to write.
            /// Example:
            /// WriteComment("test");
            /// returns // test
			/// </summary>
			/// <param name="commentText"></param>
			public void WriteComment(string commentText)
			{
				// Write Line Containing Comment
				StringBuilder sb = new StringBuilder("// ");
				sb.Append(commentText);
				WriteLine(sb.ToString());
			}
			#endregion

			#region WriteConstructor()
			private void WriteConstructor(DataJuggler.Net.DataTable table)
			{
				// Begin Region Constructor
				BeginRegion("Constructor");

                // Write Default Constructor Code Here
                string constructor = DefaultConstructor(GetClassName(table));

                // Write This Constructor
                WriteLine(constructor);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Write Blank Line
				WriteLine();
				
				// Write Close Bracket
				WriteCloseBracket();

				// End Region Default Constructor
				EndRegion();
			}
			#endregion

			#region WriteCreateTableMethod(DataJuggler.Net.DataTable table)
			private void WriteCreateTableMethod(DataJuggler.Net.DataTable table)
			{
				// string for writing lines
				string line = null;

				// Quote
				char q = '"';

				// StringBuilder To Build Complex Strings
				StringBuilder sb = new StringBuilder();

				// Write BeginRegion "CreateTable(DataJuggler.Net.DataTable table)";
				line = "CreateTable(DataJuggler.Net.DataTable table)";
				BeginRegion(line);

				// Write Method Name
				line = "private void CreateTable(DataJuggler.Net.DataTable table)";
				WriteLine(line);

				// WriteOpenBracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;

				// Instanciate table
				line = "this.table = new DataJuggler.Net.DataTable();";
				WriteLine(line);

				// Create field For Adding To Fields Collection
				line = "DataJuggler.Net.DataField field = new DataJuggler.Net.DataField();";
				WriteLine(line);

				// Set table Properties

				// Check If There Is A Need To Write ConnectionString
				// If table.parentDatabase Is Null Then ConnectionString Must Be Set
				// Else The parentDatabase ConnectionString Will Have To Be Used
				if(table.ParentDatabase == null)
				{
					// Check If table.ConnectionString Is Null
					if(table.ConnectionString != null)
					{
						// Write ConnectionString Property
						// This Must Be A Relative Path Not HardCoded Drive Letters
						sb = new StringBuilder("this.table.ConnectionString = ");
						sb.Append(q);
						sb.Append(table.ConnectionString);
						sb.Append(q);
						line = sb.ToString();
						WriteLine(line);
					}
				}

				// Write CreateCollectionClass
				if(table.CreateCollectionClass)
				{
					line = "this.table.CreateCollectionClass = true;";
				}
				else
				{
					line = "this.table.CreateCollectionClass = false;";
				}
				WriteLine(line);

				// WriteName
				sb = new StringBuilder("this.table.Name = ");
				sb.Append(q);
				sb.Append(table.Name);
				sb.Append(q);
				sb.Append(";");
				line = sb.ToString();
				WriteLine(line);

				// xmlFileName
				if(table.XmlFileName != null)
				{
					sb = new StringBuilder("table.xmlFileName = ");
					sb.Append(q);
					sb.Append(table.XmlFileName);
					sb.Append(q);
					sb.Append(";");
					line = sb.ToString();
					WriteLine(line);
				}

				// WriteFields Collection
				foreach(DataJuggler.Net.DataField Field in table.Fields)
				{
					// Write Blank Line
					WriteLine();

					// Create New field
					sb = new StringBuilder();
					sb.Append("field: ");
					sb.Append(Field.FieldName);
					WriteComment(sb.ToString());
					line = "field = new DataJuggler.Net.DataField();";
					WriteLine(line);

					// Write field Property
					WriteFieldProperty("fieldName",Field.FieldName);
					WriteFieldProperty("Caption",Field.Caption);
					WriteFieldProperty("dataType",Field.DataType);
					WriteFieldProperty("DecimalPlaces",Field.DecimalPlaces);
					WriteFieldProperty("DefaultValue",Field.DefaultValue);
					WriteFieldProperty("fieldOrdinal",Field.FieldOrdinal);
					WriteFieldProperty("Index",Field.Index);
					WriteFieldProperty("IsNullable",Field.IsNullable);
					WriteFieldProperty("PrimaryKey",Field.PrimaryKey);
					WriteFieldProperty("Required",Field.Required);
					WriteFieldProperty("Size",Field.Size);
						
					// Add This field To This Class.table.Fields Collection
					line = "this.table.Fields.Add(field);";
					WriteLine(line);

				}
					
				// Decrease Indent
				Indent--;

				// Write Close Bracket
				WriteCloseBracket();

				// End Region
				EndRegion();

			}
			#endregion

			#region WriteCustomCode(System.Collections.ArrayList List)
			private void WriteCustomCode(System.Collections.ArrayList List)
			{
					
				// Write Each Line Of Custom Code For This Region
				foreach(string line in List)
				{
					// Write This Line
					WriteLine(line,true,false,true);
				}
					
			}
			#endregion

            #region WriteDataClasses(DataManager dataManager)
            public bool WriteDataClasses(DataManager dataManager)
			{
				// Validate DataManager Class Is Ready To Export
                if (!Validate(dataManager))
				{
					return false;
				}
				
				// If all classes are written to a single file
                if (dataManager.ClassFileOptions == DataManager.FileOptions.SingleFile)
				{
				    // Write Single File
                    WriteAllClassesToSingleFile(dataManager);
			    }
			    else
			    {   
                    // Write Each Class To Its Own File
                    WriteEachClassToSeperateFile(dataManager);
			    }

				// Return True
				return true;
			}
			#endregion

			#region WriteDataClassVariables(DataJuggler.Net.DataTable table)
			private void WriteDataClassVariables(DataJuggler.Net.DataTable table)
			{
				// local StringBuilder
				StringBuilder sb = new StringBuilder();
				
				// local
                string dataType = null;

				// Loop Through Each field
				foreach(DataField field in table.Fields)
				{
					// Verify This field Is Not Excluded
					if((!field.Exclude) && (field.DataType != DataManager.DataTypeEnum.NotSupported))
					{
						// Create New String
						sb = new StringBuilder("private ");

                        // parse data type
                        dataType = ConvertDataType(field);
                        		
						// Append dataType
						sb.Append(dataType);

						// Append Space
						sb.Append(" ");

						// Append Variable Name
                        string fieldName = CapitalizeFirstChar(field.FieldName, true);
						sb.Append(fieldName);

						// Append Closing Semicolon
						sb.Append(";");

						// Write This Line
						WriteLine(sb.ToString());
					}
				}

				// If This table Has A Primary Key
				if(table.HasPrimaryKey)
				{
					// Write Line
					WriteLine("private bool delete;");
				}
			}
			#endregion

			#region WriteDatabaseConstructor(string ClassName)
			private void WriteDatabaseConstructor(string ClassName, List<DataField> fields)
			{
				// Begin Region Database Constructor
				BeginRegion("Database Constructor");
						
				// String To Hold Database Constructor
				string con = DatabaseConstructor(ClassName);

				// Write Constructor
				WriteLine(con);

				// To Do: Write Call To Load table
		
				// Write Open Bracket
				WriteOpenBracket();
				
				// Write Blank Line()
				WriteLine();
				
				// Initialize All Strings
				InitializeStrings(fields);
				
				// Write Blank Line()
				WriteLine();

				// Write Close Bracket
				WriteCloseBracket();

				// End Region XmlConstructor;
				EndRegion();

				// Write Blank Line
				WriteLine();
			}
			#endregion
			
			#region WriteDeleteChanges(DataJuggler.Net.DataTable table)
			private void WriteDeleteChanges(DataJuggler.Net.DataTable table)
			{
				// If table Has Primary Key
				if(table.HasPrimaryKey)
				{
					// Write Comment If Delete = True
					WriteComment("If Delete = True");
					
					// Write Line If(this.Delete)
					WriteLine("if(this.Delete)");
					
					// Write Open Bracket
					WriteOpenBracket();
					
					// Increase Indent
					Indent++;
					
					// Write Comment Delete Counts As A Change
					WriteComment("Delete Counts As A Change");
					
					// Write Line return true;
					WriteLine("return true;");
					
					// Decrease Indent
					Indent--;
					
					// Write Close Bracket
					WriteCloseBracket();
					
					// Write Blank Line
					WriteLine();
				
				}
			}
			#endregion

            #region WriteEachClassToSeperateFile(DataManager dataManager)
            /// <summary>
            /// This method writes each class to a seperate file
            /// </summary>
            /// <param name="dataManager"></param>
            private void WriteEachClassToSeperateFile(DataManager dataManager)
            {
                // Write the classes for every table in all databases
                foreach(Database db in dataManager.Databases)
                {
                    // now write classes for each table in this database
                    foreach(DataTable dataTable in db.Tables)
                    {
                        // must set the ObjectLibraryNameSpace for each table here
                        dataTable.ObjectNameSpaceName = dataManager.NamespaceName;
                    
                        // Create File
                        string fileName = GetClassFileName(dataTable);
                        string fullPath = dataManager.ReturnFullPath(fileName);

                        // if this is not the business object pass
                        // or if the file exists do not write the file
                        // Existing BusinessObject Classes are not overwritten
                        // so you can modify the code. If there are any new tables
                        // this will create a business object
                        if ((!this.BusinessObjectPass) || (!System.IO.File.Exists(fullPath)))
                        {
                            // Create File
                            this.CreateFile(fullPath, DataManager.ProjectTypeEnum.ObjectLibrary);

                            // Write References
                            this.WriteReferences(dataManager.References);

                            // Write Blank Line & NameSpace
                            WriteLine();

                            // Write NameSpace
                            WriteNamespace(dataManager.NamespaceName);

                            // Write OpenBracket
                            WriteOpenBracket();

                            // Indent
                            Indent++;

                            // Write The Class For This File
                            WriteClass(dataTable);
                            
                            // Write Blank Line
                            WriteLine();

                            // Decrease Indent
                            Indent--;

                            // Write CloseBracket
                            WriteCloseBracket();

                            // Close The File
                            this.CloseFile();
                        }
                    }
                }
            }
            #endregion

			#region WriteExportToDataRowMethod(DataJuggler.Net.DataRow dataTable)
			private void WriteExportToDataRowMethod(DataJuggler.Net.DataTable dataTable)
			{
				// string for each fieldline to update
				string line = null;

				// Write Begin Region
				BeginRegion("ExportToDataRow()");

				// Write Method Declaration
				line = "public DataJuggler.Net.DataRow ExportToDataRow()";
				WriteLine(line);

				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;

                // Create Recreate dataClass.table.Rows
                WriteComment("Recreate this.table.Rows Collection");
                WriteLine("this.table.Rows = new DataRowsCollection();");
                
                // Write Blank Line
                WriteLine();

                WriteComment("Create DataRow For This Object");
                WriteLine("DataJuggler.Net.DataRow dataRow = new DataJuggler.Net.DataRow(this.table);");
                WriteLine("dataRow.Fields = this.table.Fields.Clone();");

                // Write Blank Line
                WriteLine();
				
				// Write Comment Update Each field
				WriteComment("Set Parent table");
				WriteLine("dataRow.ParentTable = this.table;");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Update Each field
				WriteComment("Update Each field");
				
				// Write Blank Line
				WriteLine();

				// Create row
				DataJuggler.Net.DataRow dataRow = new DataRow(dataTable);

				// Loop Through Fields Collection 
				foreach(DataField dataField in dataTable.Fields)
				{
					// If This field.dataType Is Supported
					if(DataManager.IsSupported(dataField.DataType))
					{
						// Check This field For Being Null
						WriteComment(dataField.FieldName);
						
						// If This Is Not A Number
						if(!dataRow.IsNumericDataType(dataField.DataType))
						{
							// Create NullField
							line = NullField(dataField);
							WriteLine(line);

							// WriteOpenBracket()
							WriteOpenBracket();

							// Increase Indent
							Indent++;
						}

						// Get The String To Write For This Line
						line = UpdateLine(dataField);
						WriteLine(line);

						// If This Is Not A Number
						if(!dataRow.IsNumericDataType(dataField.DataType))
						{
							// Descrease Indent
							Indent--;

							// Write Close Bracket();
							WriteCloseBracket();
						}
					}

					// Write Blank Line
					WriteLine();
				}
				
				// If This table Has A Primary key
				if(dataTable.HasPrimaryKey)
				{
					// Write Comment Set row.Delete To True
					WriteComment("Set dataRow.Delete");
					
					// Write Line row.Delete = true;
					WriteLine("dataRow.Delete = this.Delete;");
					
				}
				
				// Write Blank Line
				WriteLine();

                WriteComment("Add dataRow To this.table.Rows Collection");
                WriteLine("this.table.Rows.Add(dataRow);");

                // Write Blank Line
                WriteLine();

				// Write Comment Return dataRow
				WriteComment("Return dataRow");

				// Write Line "return dataRow;"
				WriteLine("return dataRow;");

				// Decrease Indent
				Indent--;

				// Write Close Bracket
				WriteCloseBracket();

				// Write End Region
				EndRegion();

			}
			#endregion

			#region WriteFieldChangesGet(DataField field, DataTable table)
			public void WriteFieldChangesGet(DataJuggler.Net.DataField Field,DataJuggler.Net.DataTable table)
			{
				// If field Is Supported
				if(DataManager.IsSupported(Field.DataType))
				{
					// Write Blank Line
					WriteLine();
					
					// string for new objectname
					string newobjectname = NewObjectName(GetClassName(table),false,"Initial",false);
					
					// Write Comment For This field
					StringBuilder sb = new StringBuilder("If ");
					sb.Append(Field.FieldName);
					sb.Append(" Changed");
					WriteComment(sb.ToString());

					// Now Created Comparison Line
					sb = new StringBuilder();
					sb.Append("if(");
					sb.Append("this.");
					sb.Append(Field.FieldName);
					sb.Append(" != ");
					sb.Append(newobjectname);
					sb.Append(".");
					sb.Append(Field.FieldName);
					sb.Append(")");
					WriteLine(sb.ToString());

					// Write Open Bracket {
					WriteOpenBracket();

					// Increase Indent
					Indent++;

					// Write Comment
					sb = new StringBuilder(Field.FieldName);
					sb.Append(" Changed");
					WriteComment(sb.ToString());

					// Write Return True
					WriteLine("return true;");

					// Decrease Indent
					Indent--;

					// Write Close Bracket }
					WriteCloseBracket();

				}
			}
			#endregion

			#region WriteFieldProperty(string PropertyName, string PropertyValue) +3 overrides

				#region WriteFieldProperty(string PropertyName, string PropertyValue) 
				private void WriteFieldProperty(string PropertyName, string PropertyValue)
				{
					// Quote
					char q = '"';

					// Write field Properties
					StringBuilder sb = new StringBuilder("field.");
					sb.Append(PropertyName);
					sb.Append(" = ");
					sb.Append(q);
					sb.Append(PropertyValue);
					sb.Append(q);
					sb.Append(";");
					WriteLine(sb.ToString());
				}
				#endregion

                #region WriteFieldProperty(string PropertyName, DataManager.DataTypeEnum dataType)
                private void WriteFieldProperty(string PropertyName, DataManager.DataTypeEnum DataType)
				{

					// Write field Properties
					StringBuilder sb = new StringBuilder("field.");
					sb.Append(PropertyName);
					sb.Append(" = DataJuggler.Net.DataTypeEnum.");
					sb.Append(DataType.ToString());
					sb.Append(";");
					WriteLine(sb.ToString());
				}
				#endregion

				#region WriteFieldProperty(string PropertyName, int PropertyValue) 
				private void WriteFieldProperty(string PropertyName, int PropertyValue)
				{
					// Write field Properties
					StringBuilder sb = new StringBuilder("field.");
					sb.Append(PropertyName);
					sb.Append(" = ");
					sb.Append(PropertyValue);
					sb.Append(";");
					WriteLine(sb.ToString());
				}
				#endregion

				#region WriteFieldProperty(string PropertyName, bool PropertyValue) 
				private void WriteFieldProperty(string PropertyName, bool PropertyValue)
				{
					// Write field Properties
					StringBuilder sb = new StringBuilder("field.");
					sb.Append(PropertyName);
					sb.Append(" = ");
					sb.Append(PropertyValue.ToString().ToLower());
					sb.Append(";");
					WriteLine(sb.ToString());
				}
				#endregion

			#endregion

			#region WriteGet(DataField field) bool
			private bool WriteGet(DataField field)
			{
                if (field.AccessMode == DataManager.AccessMode.ReadOnly)
				{
					return true;
				}

                if (field.AccessMode == DataManager.AccessMode.ReadWrite)
				{
					return true;
				}

				// Else Return False
				return false;

			}
			#endregion 
			
			#region WriteGetIndex(DataJuggler.Net.DataTable table)
			private void WriteGetIndex(DataJuggler.Net.DataTable table)
			{
				// Determine Type Of field Primary Key Is
				string dataType = ConvertDataType(table.PrimaryKey);
				
				// Get Primary Key field Name
				string primaryKeyName = table.PrimaryKey.FieldName;
				
				// Begin Region GetIndex()
				string indexRegion = GetIndexRegion(dataType, primaryKeyName);
				
				// Write Comment BeginRegion GetIndex(int categoryID)
				BeginRegion(indexRegion);
				
				// Get Index Declaration public int GetIndex(int categoryID)
				string IndexDeclaration = GetIndexDeclaration(dataType, primaryKeyName);
				
				// Write Index Declaration
				WriteLine(IndexDeclaration);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Comment Initial Value
				WriteComment("Initial Value");
				
				// Write Line int Index = -1;
				WriteLine("int index = -1;");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Check Each Item In Collection
				WriteComment("Check Each Item");
				
				// Get For Each
				string ForEachLine = CreateForEach(table);
				
				// Write For Each Line
				WriteLine(ForEachLine);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// WriteComment Increment Index
				WriteComment("Increment Index");
				
				// Write Line To Increment Index
				WriteLine("index++;");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Check This Item
				WriteComment("Check This Item");
				
				// Get Comparison Line
				string CompareLine = GetCompareLine(table, primaryKeyName);
				
				// Write CompareLine
				WriteLine(CompareLine);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Comment Return Index
				WriteComment("Return Index");
				
				// Write Line Return Index
				WriteLine("return index;");
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Return Not Found (-1)
				WriteComment("Return Not Found (-1)");
				
				// Write Line return -1
				WriteLine("return -1;");
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Write End Region
				EndRegion();
				
				// Write Blank Line
				WriteLine();
			}
			#endregion
			
			#region WriteIndexProperty(string className)
			private void WriteIndexProperty(string className)
			{
				// Local
				string line = "";
				
				// Write Blank Line
				WriteLine();
				
				// Increase Indent
				Indent++;
				
				// Begin Region For Index
				BeginRegion("Index");
				
				// Create StringBuilder
				StringBuilder sb = new StringBuilder("public ");
				
				// Append ClassName
				sb.Append(className);
				
				// Append this[int Index]
                sb.Append(" this[int Index]");
				
				// Set Line
                line = sb.ToString();
				
				// Write Line
				WriteLine(line);
				
				// WriteOpen Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Get
				WriteLine("get");
				
				// WriteOpen Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Line
                WriteLine("return (" + className + ") this.List[Index];");
				
				// Decrease Indent
				Indent--;
				
				// Write CloseBracket();
				WriteCloseBracket();
				
				// Decrease Indent
				Indent--;
				
				// Write CloseBracket();
				WriteCloseBracket();
				
				// Write EndRegion
				EndRegion();
				
				// Decrease Indent
				Indent--;

                // Write Blank Line
                WriteLine();
			}
			#endregion
			
			#region WriteInitialChanges(DataJuggler.Net.DataTable table)
			private void WriteInitialChanges(DataJuggler.Net.DataTable table)
			{
				// string for new objectname
				string newobjectname = NewObjectName(GetClassName(table),false,"Initial",false);

				// Write Comment For This 
				WriteComment("If this is an addNew return true (Object has not been loaded)");
				
				// Write Line For Test
				StringBuilder sb = new StringBuilder("if(this.");
				sb.Append(newobjectname);
				sb.Append(" == null)");
				WriteLine(sb.ToString());
				
				// Write Open Bracket {
				WriteOpenBracket();

				// Increase Indent
				Indent++;

				// Write Comment
				WriteComment("New Records Are Always True");

				// Write Return True
				WriteLine("return true;");

				// Decrease Indent
				Indent--;

				// Write Close Bracket }
				WriteCloseBracket();
					
				// Write Blank Line
				WriteLine();
					
			}
        #endregion

        #region WriteIsNewProperty(DataTable table)
        /// <summary>
        /// This method writes the IsNew property.
        /// </summary>
        /// <param name="table"></param>
        private void WriteIsNewProperty(DataTable table)
        {
            // locals
            string propertyLine = null;
            string regionLine = null;
            string getText = null;

            // if this table exists, it has a primary key and it is not a view
            if ((table != null) && (table.HasPrimaryKey) && (!table.IsView))
            {
                // Create DataField
                DataField field = new DataField();

                // Set fieldName
                field.FieldName = "IsNew";

                // Set field Properties
                field.DataType = DataManager.DataTypeEnum.Boolean;

                // Write A NewLine
                WriteLine();

                // increment Indent
                Indent++;

                // Start A New Region For This PropertyLine
                regionLine = GetRegionLine(field);
                BeginRegion(regionLine);

                // Get PropertyLineText
                propertyLine = GetPropertyLine(field);

                // Write The PropertyLine
                WriteLine(propertyLine);

                // Write An OpenBracket
                WriteOpenBracket();

                // Increment Indent
                Indent++;

                // Write The Word Get
                WriteLine("get");

                // Write An OpenBracket
                WriteOpenBracket();

                // Increment Indent
                Indent++;

                // Write Comment
                WriteComment("Initial Value");

                // Changed this from AutoNumber to AutoNumber or Integer for the check, as a foreign key could be the Primary Key
                if ((table.PrimaryKey.DataType == DataManager.DataTypeEnum.Integer) || (table.PrimaryKey.DataType == DataManager.DataTypeEnum.Autonumber))
                    {
                        // Write Get Code Now
                        getText = "bool isNew = (this." + table.PrimaryKey.FieldName + " < 1);";
                    }
                    else if (table.PrimaryKey.DataType == DataManager.DataTypeEnum.String)
                    {
                        // Write Get Code Now
                        getText = "bool isNew = (!String.IsNullOrEmpty(" + table.PrimaryKey.FieldName + "));";
                    }
                    
                    // write out the getText
                    WriteLine(getText);

                    // Write A NewLine
                    WriteLine();
                    
                    // Write Comment Return Value
                    WriteComment("return value");
                    
                    // WriteLine return isNew
                    WriteLine("return isNew;");

                    // Decrement Indent
                    Indent--;

                    // Write A CloseBracket
                    WriteCloseBracket();
                    
                    // Decrement Indent
                    Indent--;

                    // Write A CloseBracket
                    WriteCloseBracket();

                    // EndRegion
                    EndRegion();

                    // decrement Indent
                    Indent--;
                }    
            }
            #endregion
            
			#region WriteLine() + 4 overrides
					
				#region WriteLine(string LineText, bool NewLine, bool SkipText, bool SkipIndent) void
				public void WriteLine(string LineText, bool NewLine, bool SkipText, bool SkipIndent)
				{
					// string to hold linetext + identity
					string linetext = null;

					// If Text Is Not Skipped
					if(!SkipText)
					{
						// Set linetext to LineText incase SkipIndent 
						linetext = LineText;

						// If SkipIndent Is False Do Not Indent String
						if(!SkipIndent)
						{
							// string to hold linetext + identity
							linetext = IndentString(LineText);
						}

						// Write The Current Line
						Writer.Write(linetext);
					}

					// If NewLine Add New Line
					if(NewLine)
					{
						// Write A New Line
						Writer.Write(Writer.NewLine);
					}
				}
				#endregion

				#region WriteLine(string LineText,bool NewLine, bool SkipText) void
				/// <summary>
				/// Indent Is Automatic With This Method
				/// </summary>
				/// <param name="LineText"></param>
				/// <param name="NewLine"></param>
				public void WriteLine(string LineText,bool NewLine, bool SkipText)
				{
					// Call Write Line With These Parameters
					WriteLine(LineText, NewLine, SkipText, false);
				}
				#endregion		
			
				#region WriteLine(string LineText, bool NewLine) void
				/// <summary>
				/// Indent & SkipText Are Automatic
				/// </summary>
				/// <param name="LineText"></param>
				/// <param name="NewLine"></param>
				public void WriteLine(string LineText, bool NewLine)
				{
					// Call WriteLine
					WriteLine(LineText, NewLine, false,false);
				}
				#endregion

				#region WriteLine(string LineText) void
				/// <summary>
				/// SkipText Is False With This Method (Text Must Not Be Null)
				/// NewLine Is True. Indent Is Automatic
				/// </summary>
				/// <param name="LineText"></param>
				/// <param name="NewLine"></param>
				public void WriteLine(string LineText)
				{
					// WriteLine
					WriteLine(LineText, true, false,false);
				}
				#endregion

				#region WriteLine() void 
				/// <summary>
				/// Writes A Blank Line
				/// </summary>
				/// <param name="LineText"></param>
				/// <param name="NewLine"></param>
				public void WriteLine()
				{
					WriteLine(null, true, true,false);
				}
				#endregion

			#endregion
			
			#region WriteLoadMethodFromDataRow(DataJuggler.Net.DataTable table)
			private void WriteLoadMethodFromDataRow(DataJuggler.Net.DataTable table)
			{
				// Local Variable
				string line = "";
				
				// Write Begin Region With Overload
				BeginRegion("Load(DataJuggler.Net.DataRow row)");
				
				// Function Declaration
				line = "public void Load(DataJuggler.Net.DataRow row)";
				WriteLine(line);
				
				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;

				// Write Comment Load Each field
				WriteComment("Load Each field");
				
				// Load Each field
				foreach(DataJuggler.Net.DataField Field in table.Fields)
				{
					// If This Type Of field Is Supported
					if(DataManager.IsSupported(Field.DataType))
					{
						// Text To Load field
						line = LoadField(Field);
						WriteLine(line);
					}
				}

				// Write Blank Line
				WriteLine();

				// Clone Original New Object For Changes Comparison

				// string newobjectname
				string newobjectname = NewObjectName(GetClassName(table),false,"Initial",false);

				// Write Comment For Clone New Object
				WriteComment("Clone This Object Into InitialObject For Changes Comparison");
					
				// Create StringBuilder 
				StringBuilder sb = new StringBuilder("this.");
				sb.Append(newobjectname);
				sb.Append(" = this.Clone();");
				WriteLine(sb.ToString());

				// Write Blank Line
				WriteLine();

				// Decrease Indent
				Indent--;

				// Write Close Bracket
				WriteCloseBracket();
				
				// Write EndRegion
				EndRegion();
				
				// Write Blank Line
				WriteLine();
				
			}
			#endregion

			#region WriteLoadMethodFromSQL()
			private void WriteLoadMethodFromSQL()
			{
				// Local Variable
				string line = "";
				
				// Write Begin Region With Overload
				BeginRegion("Load(string sql, AccessDatabaseConnector DBConnector)");
				
				// Function Declaration
				line = "public void Load(string sql, AccessDatabaseConnector DBConnector)";
				WriteLine(line);
				
				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;
				
				// Begin Cut
				// Write Comment Load table.Rows
				WriteComment("Load table.Rows");
		
				// Load table.Rows
				WriteLine("this.table.Rows = DBConnector.LoadDataRows(this.table, sql);");
				
				// Write Blank Line
				WriteLine();
				
				// Now Test RowCount
				WriteComment("Test For A Valid row");
				line = "if(this.table.Rows.Count > 0)";
				WriteLine(line);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Load Object From row[0]
				WriteComment("Load object from row[0]");
				WriteLine("this.Load(this.table.Rows[0]);");
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Write Blank Line
				WriteLine();
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Write EndRegion
				EndRegion();
				
				// Write Blank Line
				WriteLine();
				
			}
			#endregion
			
			#region WriteLoadMethodField()
			private void WriteLoadMethodField()
			{
				// Local Variable
				string line = "";
				
				// Get RegionLine For This Method
				line = LoadMethodFieldRegion();
								
				// Write Begin Region Wit
				BeginRegion(line);
				
				// Function Declaration
				line = LoadMethodFieldDeclaration();
				WriteLine(line);
				
				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;
				
				// Write Create sql Statement 
				CreateSelectSQL();
				
				// Write Blank Line
				WriteLine();
				
				// Call this.Load(sql, DBConnector
				WriteComment("Call this.Load(sql, DBConnector)");
				line = "this.Load(sql, DBConnector);";
				WriteLine(line);
				
				// WriteBlankLine();
				WriteLine();
				
				// Decrease Indent
				Indent--;

				// Write Close Bracket
				WriteCloseBracket();
				
				// Write EndRegion
				EndRegion();
				
				// Write Blank Line
				WriteLine();
			}
			#endregion

			#region WriteLoadMethods(DataJuggler.Net.DataTable table)
			private void WriteLoadMethods(DataJuggler.Net.DataTable table)
			{	
				// string for LoadMethodRegion
				string line = LoadMethodRegion();
				
				// Write Begin Region With Overload
				BeginRegion(line);
					
				// Write Blank Line
				WriteLine();
					
				// Increase Indent
				Indent++;
				
				// Write LoadFromDataRow
				WriteLoadMethodFromDataRow(table);
				
				// WriteLoadMethodFromSQL()
				WriteLoadMethodFromSQL();
				
				// Write LoadMethod From Primary Key
				WriteLoadMethodField();
				
				// Decrease Indent
				Indent--;
				
				// End Region
				EndRegion();
			}	
			#endregion

			#region WriteMethods()
			private void WriteMethods(DataJuggler.Net.DataTable table)
			{
				// Begin Region Constructor
				BeginRegion("Methods");

                // Write Blank Line
                WriteLine();

                // if the Data pass
				if(!BusinessObjectPass)
				{
				    // Increase Indent
				    Indent++;
                    
				    // if we have a primary key
                    if ((table.HasPrimaryKey) && (!table.IsView))
                    {
                        // Write UpdateIdentity method
                        WriteUpdateIdentityMethod(table);
                    }
                    else
                    {
                        // get table name
                        string tableName = table.Name; 
                    }
    				
				    // Decrease Indent
				    Indent--;

				    // Write Blank Line
				    WriteLine();
	            }
                else
                {
                    // Increase Indent
                    Indent++;

                    // write the clone method
                    WriteCloneMethod(table);

                    // Decrease Indent
                    Indent--;

                    // write a blank line
                    WriteLine();
                }
	           
				// End Region Constructors
				EndRegion();
			}
			#endregion

			#region WriteNamespace(string Namespace)
			private void WriteNamespace(string Namespace)
			{
				// Build String To Write
				StringBuilder sb = new StringBuilder("namespace ");

				// Append Namespace
				sb.Append(Namespace);
					
				// Write This Line
				WriteLine(sb.ToString());
			}
			#endregion

            #region WriteOpenBracket() void

                #region WriteOpenBracket() void
                /// <summary>
                /// This method writes an Open Bracket {
                /// </summary>
                public void WriteOpenBracket()
                {
                    // Write Openbracket
                    WriteLine("{");
                }
                #endregion
    			
			    #region WriteOpenBracket(bool increaseIndent) void
			    /// <summary>
			    /// This method writes an Open Bracket {
                /// and increased identity
			    /// </summary>
			    public void WriteOpenBracket(bool increaseIndent)
			    {
				    // Write Openbracket
				    WriteLine("{");
				    
				    // if increase identity (should always be true,
				    // other wise call the over load to this method
				    if(increaseIndent)
				    {
				        // Increase Indent
				        Indent++;
				    }
			    }
			    #endregion
			    
			#endregion

			#region WritePrivateVariables(DataTable table)
			private void WritePrivateVariables(DataTable table)
			{
				// Begin Region Private Variables
				BeginRegion("Private Variables");

				// if this is the data object pass
				if(!businessObjectPass)
				{
				    // Write DataClassVariables
				    WriteDataClassVariables(table);
			    }

				// Write EndRegion DataClassVariables
				EndRegion();
			}
			#endregion

			#region WriteProperties(DataJuggler.Net.DataTable table) void
			private void WriteProperties(DataJuggler.Net.DataTable table)
			{
				// Begin Region For Properties
				BeginRegion("Properties");

                // if this is the data object pass
				if(!BusinessObjectPass)
				{
				    // Write Each Property
				    foreach(DataField Field in table.Fields)
				    {
					    // If this field should have a property created
                        if((!Field.Exclude) && (Field.DataType != DataManager.DataTypeEnum.NotSupported))
					    {
						    // Write this property
                            WriteProperty(Field);
					    }
				    }

				    // Write Property For Delete If This table Has A Primary Key
				    if(table.HasPrimaryKey)
				    {
					    // Create field For Delete
					    DataJuggler.Net.DataField DelField = new DataField();
					    DelField.FieldName = "Delete";
					    DelField.DataType = DataManager.DataTypeEnum.Boolean;
					    DelField.AccessMode = DataManager.AccessMode.ReadWrite;
					    DelField.Scope = DataManager.Scope.Public;
					    WriteProperty(DelField);

                        // if this is an Identity column
                        if (table.PrimaryKey.DataType == DataManager.DataTypeEnum.Autonumber)
                        {
					        // Write Property For IsNew
					        WriteIsNewProperty(table);
                        }
				    }

                    // Write Empty Blank Line
                    WriteLine();	
                }
				
				// EndRegion For DataClass Properties
				EndRegion();
			}
			#endregion

			#region WriteProperty(DataField field) void +1 override

				#region WriteProperty(DataField field) void 
				private void WriteProperty(DataField field)
				{
					// locals
					string PropertyLine = null;
					string RegionLine = null;

					// Write A NewLine
					WriteLine();

					// increment Indent
					Indent++;

					// Start A New Region For This PropertyLine
                    RegionLine = GetRegionLine(field);
					BeginRegion(RegionLine);

					// Get PropertyLineText
                    PropertyLine = GetPropertyLine(field);

					// Write The PropertyLine
					WriteLine(PropertyLine);

					// Write An OpenBracket
					WriteOpenBracket();

					// Increment Indent
					Indent++;

					// If ReadOnly or ReadWrite 
                    if (WriteGet(field))
					{			
						// Write The Word Get
						WriteLine("get");

						// Write An OpenBracket
						WriteOpenBracket();

						// Increment Indent
						Indent++;

						// Write Get Code Now
                        string GetText = CreateGetText(field);
						WriteLine(GetText);

						// Decrement Indent
						Indent--;

						// Write A CloseBracket
						WriteCloseBracket();
					}

					// If ReadWrite or WriteOnly
                    if (WriteSet(field))
					{
						// Write The Word set
						WriteLine("set");

						// Write An OpenBracket
						WriteOpenBracket();

						// Increment Indent
						Indent++;

						// Write Get Code Now
                        string SetText = CreateSetText(field);
						WriteLine(SetText);

						// Decrement Indent
						Indent--;

						// Write A CloseBracket
						WriteCloseBracket();
					}

					// Decrement Indent
					Indent--;

					// Write A CloseBracket
					WriteCloseBracket();

					// EndRegion
					EndRegion();

					// decrement Indent
					Indent--;	
				}
				#endregion

				#region WriteProperty(DataField field, string dataType) void 
				private void WriteProperty(DataField Field, string DataType)
				{
					// locals
					string PropertyLine = null;
					string RegionLine = null;

					// Write A NewLine
					WriteLine();

					// increment Indent
					Indent++;

					// Start A New Region For This PropertyLine
					RegionLine = GetRegionLine(Field,DataType);
					BeginRegion(RegionLine);

					// Get PropertyLineText
					PropertyLine = GetPropertyLine(Field,DataType);

					// Write The PropertyLine
					WriteLine(PropertyLine);

					// Write An OpenBracket
					WriteOpenBracket();

					// Increment Indent
					Indent++;

					// If ReadOnly or ReadWrite 
					if(WriteGet(Field))
					{
								
						// Write The Word Get
						WriteLine("get");

						// Write An OpenBracket
						WriteOpenBracket();

						// Increment Indent
						Indent++;

						// Write Get Code Now
						string GetText = CreateGetText(Field);
						WriteLine(GetText);

						// Decrement Indent
						Indent--;

						// Write A CloseBracket
						WriteCloseBracket();
					}

					// If ReadWrite or WriteOnly
					if(WriteSet(Field))
					{
						// Write The Word set
						WriteLine("set");

						// Write An OpenBracket
						WriteOpenBracket();

						// Increment Indent
						Indent++;

						// Write Get Code Now
						string SetText = CreateSetText(Field);
						WriteLine(SetText);

						// Decrement Indent
						Indent--;

						// Write A CloseBracket
						WriteCloseBracket();
					}

					// Decrement Indent
					Indent--;

					// Write A CloseBracket
					WriteCloseBracket();

					// EndRegion
					EndRegion();

					// decrement Indent
					Indent--;

								
				}
				#endregion

			#endregion

			#region WriteReference(Reference Ref) void
			public void WriteReference(Reference Ref)
			{
				// local
				string Line = null;

				// Get ReferenceLine
				Line = GetReferenceLine(Ref);

				// Write The PropertyLine
				WriteLine(Line);

			}
			#endregion

			#region WriteReferences(ReferencesSet References)
			public void WriteReferences(ReferencesSet references)
			{
                try
                {
                    // Write Blank Line
                    WriteLine();

                    // Write Blank Line
                    WriteLine();

                    // Begin Regin Using Statements
                    BeginRegion("using statements");

                    // Write Blank Line
                    WriteLine();

                    // Write Each References
                    foreach (Reference refObject in references)
                    {
                        //if(!this.BusinessObjectPass)
                        //{
                            // Write This References
                            WriteReference(refObject);
                        //}
                    }

                    // Write Blank Line
                    WriteLine();

                    // Write EndRegion Line
                    WriteLine("#endregion");

                    // Write Blank Line
                    WriteLine();
                }
                catch (Exception error)
                {
                    // Set the failed reason
                    this.FailedReason = error.ToString();
                }
			}
			#endregion
			
			#region WriteSaveMethod()
			private void WriteSaveMethod()
			{
				// Begin Region For AddMethod
				BeginRegion("Save(DataJuggler.Net.AccessDatabaseConnector DBConnector");
				
				// String For Method Declaration
				string SaveMethod = "public bool Save(DataJuggler.Net.AccessDatabaseConnector DBConnector)";
				
				// Write Method Declaration
				WriteLine(SaveMethod);

				// Write Open Bracket
				WriteOpenBracket();

				// Increase Indent
				Indent++;

				// Write Comment Return True If There Are Not Any Changes
				WriteComment("Return True If There Are Not Any Changes");

				// Write Check for Changes
				string line = "if(!this.Changes)";
				
				// Write Line 
				WriteLine(line);
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Comment Return True
				WriteComment("return true");
				
				// Write return true
				WriteLine("return true;");
				
				// Decrease Indent;
				Indent--;
				
				// Write Close Bracket();
				WriteCloseBracket();
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Is Save Successful
				WriteComment("Is Save Successful");
				
				// Declartaion For bool success = false;
				WriteLine("bool success = false;");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment
				WriteComment("Call ExportToDataRow() method to update table.Rows[0]");
				
				// Write Export To DataRow
				WriteLine("this.ExportToDataRow();");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Save Changes	
				WriteComment("Save Changes");
				
				// Write Line To Save Changes
				WriteLine("success = DBConnector.SaveChanges(this.table);");
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment If Save Was Successful
				WriteComment("If Save Was Successful");
				WriteLine("if(success)");
				
				// Write Open Bracket
				WriteOpenBracket();
				
				// Increase Indent
				Indent++;
				
				// Write Comment Set Changes To False
				WriteComment("Set Changes To False");
				
				// Write Line this.Changes = false
				WriteLine("this.Changes = false;");
				
				// Decrease Indent
				Indent--;
				
				// Write Close Bracket();
				WriteCloseBracket();
				
				// Write Blank Line
				WriteLine();
				
				// Write Comment Retirm success
				WriteComment("Return success");
							
				// Write Line To Return success
				WriteLine("return success;");
				
				// Write Blank Line
				WriteLine();
				
				// Decrease Indent;
				Indent--;
				
				// Write Close Bracket
				WriteCloseBracket();
				
				// Write End Region
				EndRegion();
					
			}
			#endregion				
			
            #region WriteSerializable()
            /// <summary>
            /// This method writes the Serializable Tag
            /// </summary>
            private void WriteSerializable()
            {
                // Write the Serializable Tag
                WriteLine("[Serializable]");
            } 
            #endregion

			#region WriteSet(DataField field) bool
			private bool WriteSet(DataField field)
			{
                if (field.AccessMode == DataManager.AccessMode.WriteOnly)
				{
					return true;
				}

                if (field.AccessMode == DataManager.AccessMode.ReadWrite)
				{
					return true;
				}

				// Else Return False
				return false;

			}
			#endregion
			
			#region WriteTableProperty()
			public void WriteTableProperty()
			{
				// Create field For table
				DataJuggler.Net.DataField TField = new DataField();
				TField.FieldName = "table";
				TField.DataType = DataManager.DataTypeEnum.DataTable;
				TField.AccessMode = DataManager.AccessMode.ReadWrite;
				TField.Scope = DataManager.Scope.Public;
				WriteProperty(TField);

				// Write Blank Line
				WriteLine();				
			}
			#endregion 

            #region WriteUpdateIdentityMethod(DataTable table)
            /// <summary>
            /// This method is used to update the 'ID' column for this table.
            /// </summary>
            /// <param name="table"></param>
            private void WriteUpdateIdentityMethod(DataTable table)
            {
                //#region UpdateIdentity(int id)
                ///// <summary>
                ///// This method provides a "setter"
                ///// functionality for the ID field.
                ///// </summary>
                ///// <param name="id"></param>
                //public void UpdateIdentity(int id)
                //{
                //    // update the UserID value
                //    this.userID = id;
                //} 
                //#endregion
                
                // initial value
                string line = null;

                if ((table.PrimaryKey.DataType == DataManager.DataTypeEnum.Autonumber) || (table.PrimaryKey.DataType == DataManager.DataTypeEnum.Integer))
                {
                    // Write Region 
                    line = "UpdateIdentity(int id)";
                }
                else if (table.PrimaryKey.DataType == DataManager.DataTypeEnum.String)
                {
                    // Write Region 
                    line = "UpdateIdentity(string id)";
                }
                
                // Write the region for this method
                BeginRegion(line);
                
                // Write Comments For This Method
                WriteComment("<summary>");
                WriteComment("This method provides a 'setter'");
                WriteComment("functionality for the ID field.");
                WriteComment("</summary>");

                if ((table.PrimaryKey.DataType == DataManager.DataTypeEnum.Autonumber) || (table.PrimaryKey.DataType == DataManager.DataTypeEnum.Integer))
                {
                    // write method declaration
                    line = "public void UpdateIdentity(int id)";
                }
                else if (table.PrimaryKey.DataType == DataManager.DataTypeEnum.String)
                {
                    // write method declaration
                    line = "public void UpdateIdentity(string id)";
                }
                
                // write method declaration
                WriteLine(line);
                
                // Write Open Bracket
                WriteOpenBracket();
                
                // Increase Indent
                indent++;
                
                // Write Comment 'Update The ID field'
                WriteComment("Update The ID field");
                
                // Update The ID field
                string fieldName = CapitalizeFirstChar(table.PrimaryKey.FieldName, true);
                line = "this." + fieldName + " = id;";
                
                // Write line to update the id field
                WriteLine(line);
                
                // Decrease Indent
                Indent--;
                
                // Write Close Bracket
                WriteCloseBracket();
                
                // Write End Region
                WriteLine("#endregion");
            } 
            #endregion
            		
		#endregion			
		
		#region Properties

            #region BusinessObjectPass
            /// <summary>
            /// Is this the pass for the DataObjects
            /// or the BusinessObjects tier.
            /// </summary>
            public bool BusinessObjectPass
            {
                get { return businessObjectPass; }
                set { businessObjectPass = value; }
            }
            #endregion
            
			#region Indent
			public int Indent
			{
				get
				{
					return indent;	
				}
				set
				{
					indent = value;
				}
			}
			#endregion
            
			#region FailedReason
			public string FailedReason
			{
				get
				{
					return failedreason;	
				}
				set
				{
					failedreason = value;
				}
			}
			#endregion

            #region FileManager
            /// <summary>
            /// This class is used to keep track of which files were
            /// created during a build.
            /// </summary>
            public ProjectFileManager FileManager
            {
                get { return fileManager; }
                set { fileManager = value; }
            } 
            #endregion

            #region HardCodeCreateTable
            public bool HardCodeCreateTable
            {
                get
                {
                    return hardcodecreatetable;
                }
                set
                {
                    hardcodecreatetable = value;
                }
            }
            #endregion

			#region CreateTableFromXml
			public bool CreateTableFromXml
			{
				get
				{
					return createtablefromxml;	
				}
				set
				{
					createtablefromxml = value;
				}
			}
			#endregion

			#region CreateTableFromDatabase
			public bool CreateTableFromDatabase
			{
				get
				{
					return createtablefromdatabase;	
				}
				set
				{
					createtablefromdatabase = value;
				}
			}
			#endregion
			
			#region Writer
			public StreamWriter Writer
			{
				get
				{
					return writer;	
				}
				set
				{
					writer = value;
				}
			}
			#endregion

		#endregion

	}
	#endregion
	
}
