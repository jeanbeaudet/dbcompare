

#region using statements

using System.Collections;

#endregion

namespace DataJuggler.Net
{

    #region class Reference
	public class Reference
	{

		#region Private Variables
        private int referenceID;
		private string referencename;
		#endregion

	    #region Constructor
	    public Reference(string referenceName, int referenceID)
	    {
		    this.ReferenceName = referenceName;
		    this.ReferenceID = referenceID;
	    }
        #endregion

        #region Methods
        
            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                // return the referenceName when ToString() is called
                return this.ReferenceName;
            }
            #endregion
            
        #endregion

        #region Properties

        #region ReferenceID
        public int ReferenceID
            {
                get { return referenceID; }
                set { referenceID = value; }
            }
            #endregion

			#region ReferenceName
			public string ReferenceName
			{
				get
				{
					return referencename;
				}
				set
				{
					referencename = value;
				}
			}
			#endregion

		#endregion
		
	}
	#endregion

	#region class ReferencesSet
	public class ReferencesSet : CollectionBase
	{

		#region Private Variables
		private string setname;
		#endregion

		#region Constructor +1 override

			#region ReferencesSet()
			public ReferencesSet()
			{
			}
			#endregion

			#region ReferencesSet(string setName)
			public ReferencesSet(string setName)
			{
				this.SetName = setName;
			}
			#endregion

		#endregion

		#region Methods
			
			#region Add()
			public void Add(Reference component)
			{
				this.List.Add(component);
			}
			#endregion

		#endregion

		#region Properties
		
			#region Index
			public Reference this[int index]
			{
				get
				{
					return (Reference)this.List[index];
				}
			}
			#endregion

			#region SetName
			public string SetName
			{
				get
				{	
					return setname;
				}
				set
				{
					setname = value;
				}
			}
			#endregion

		#endregion

	}
	#endregion

    #region class ReferencesSetCollection
    /// <summary>
    /// I think I first wrote this back in .Net Framework 1.1
    /// where I couldn't use generic collections, or I would have 
    /// probably done it that way now.
    /// </summary>
    public class ReferencesSetCollection : CollectionBase
	{

		#region Constructor
		public ReferencesSetCollection()
		{
		}
		#endregion

		#region Methods
			
			#region Add()
			public void Add(ReferencesSet Set)
			{
				this.List.Add(Set);
			}
			#endregion

		#endregion

		#region Properties
		
			#region Index
			public ReferencesSet this[int Index]
			{
				get
				{
					return (ReferencesSet)this.List[Index];
				}
			}
			#endregion

		#endregion
		
	}
	#endregion
    
}

