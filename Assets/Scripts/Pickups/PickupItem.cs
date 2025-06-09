using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int ID;
    public string Name;
    public CircleCollider2D collider;
    private GameObject inventoryPanel;
    public InventoryController inventoryController;
    public GameObject pickupPrefab;
    private void Awake()
    {
        if(inventoryPanel == null)
        {
            inventoryPanel = GameObject.FindWithTag("InventoryPanel");
        }
        inventoryController = GameObject.FindFirstObjectByType<InventoryController>();
    }
    public virtual void Pickup()
    {
        //test
        //inventoryController.AddItem(GetComponent())
        Debug.Log($"You picked up an item: {pickupPrefab.name}");
        if (pickupPrefab)
        {
            inventoryController.AddItem(ID);
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            ItemPopupController.Instance.ShowPopup(pickupPrefab.name, sprite);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if(collision.gameObject.tag == "Player")
        {
            Pickup();
            Destroy(pickupPrefab);
        }
    }

    
}
