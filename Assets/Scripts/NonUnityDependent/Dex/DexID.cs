public struct DexID
{
    public string nameSpace;
    public int id;

    public DexID(string nameSpace, int id)
    {
        this.nameSpace = nameSpace;
        this.id = id;

		//Throw an error if nameSpace is null
		if (nameSpace == null)
		{
			throw new System.Exception("ERROR: a DexID's namespace cannot be null.  Consider using the empty string instead.");
		}
    }

	public override bool Equals(object obj)
	{
		if (!(obj is DexID))
		{
			return false;
		}

		DexID other = (DexID)obj;
		return other.nameSpace.Equals(nameSpace) && other.id == id;
	}

	public override string ToString()
	{
		string output = "<";

		//Append the namespace
		if (nameSpace != null)
		{
			output += "\"" + nameSpace + "\"";
		}
		else
		{
			output += "null";
		}
		output += ", ";

		//Append the id
		output += id + ">";

		return output;
	}
}
