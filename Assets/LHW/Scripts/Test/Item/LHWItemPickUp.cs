using UnityEngine;

/// <summary>
/// (For Test) Item Interaction test script - by Trigger
/// </summary>
public class LHWItemPickUp : MonoBehaviour
{
    public LHWTestItem Data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.GetComponent<PlayerInventoryHolder>();

        if(!inventory) return;

        if(inventory.AddItem(Data, 1))
        {
            Destroy(gameObject);
        }
    }
}
