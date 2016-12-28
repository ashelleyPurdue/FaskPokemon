using System.Collections.Generic;

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


    //Exceptions
    public class DuplicateIDException : System.Exception { }
}
