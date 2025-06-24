using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class LHWItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public LHWTestItem Data;

    private CircleCollider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
    }

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
