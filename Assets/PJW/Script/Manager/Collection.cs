using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Collection : MonoBehaviour
{
    /*
    [SerializeField] private List<string> _collectedEntryIDs = new List<string>();

    public IReadOnlyList<string> CollectedEntryIDs => _collectedEntryIDs;

    public int CollectedCount => _collectedEntryIDs.Count;

    // Constructor
public Collection()
{
    _collectedEntryIDs = new List<string>();
}

/// <summary>
/// Adds a specific entry to the collection list.
/// </summary>
/// <param name="entryId">The unique ID of the entry to add</param>
/// <returns>Returns true if newly added; false if it already exists
public bool AddEntry(string entryId)
{
    if (string.IsNullOrEmpty(entryId))
    {
        return false;
    }

    // Check if the entry has already been collected.
    if (!_collectedEntryIDs.Contains(entryId))
    {
        _collectedEntryIDs.Add(entryId); 
        return true;
    }
    else
    {
        return false;
    }
}

/// <summary>
/// Checks whether a specific entry has been collected.
/// </summary>
/// <param name="entryId">The unique ID of the entry to check</param>
/// <returns>Returns true if collected; otherwise, false.</returns>
public bool HasEntry(string entryId)
{
    return _collectedEntryIDs.Contains(entryId);
}

/// <summary>
/// Clears all collected entries. (e.g., when resetting the game)
/// </summary>
public void ClearCollection()
{
    _collectedEntryIDs.Clear();
    Debug.Log("Collection: All collected entries have been cleared.");
}

/// <summary>
/// Sets the collected entry list using saved data, etc.
/// </summary>
/// <param name="ids">A new list of entry IDs to set</param>
public void SetCollectedEntries(List<string> ids)
{
    if (ids != null)
    {
        _collectedEntryIDs = new List<string>(ids); 
    }
    else
    {
        _collectedEntryIDs = new List<string>();
    }
}*/
}
