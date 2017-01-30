using System.Collections.Generic;
using System.Text;

public class GenericDex<T>
{
    private Dictionary<DexID, T> entries = new Dictionary<DexID, T>();

    public void AddEntry(DexID id, T entry)
    {
        //Throw an exception if there's a duplicate id
        if (entries.ContainsKey(id))
        {
            throw new DuplicateIDException();
        }

		//Add it
        entries.Add(id, entry);
    }

    public T GetEntry(DexID id)
    {
        return entries[id];
    }

	public string DebugPrintAllIds()
	{
		//Returns a string of all DexIDs in the dex, for debugging purposes

		StringBuilder builder = new StringBuilder();

		foreach (DexID id in entries.Keys)
		{
			builder.AppendLine(id.ToString());
		}

		return builder.ToString();
	}

    //Exceptions
    public class DuplicateIDException : System.Exception { }
}
