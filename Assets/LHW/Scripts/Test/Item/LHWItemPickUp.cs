using UnityEngine;

/// <summary>
/// (For Test) Item Interaction test script - by Trigger
/// </summary>
public class LHWItemPickUp : MonoBehaviour
{
    public LHWTestItem Data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.GetComponent<Inventory>();

        if(!inventory) return;

        if(inventory.InventorySystem.AddItem(Data, 1))
        {
            Destroy(gameObject);
        }
    }
}
