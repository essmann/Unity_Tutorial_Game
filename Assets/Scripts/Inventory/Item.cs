using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public float minDropDistance = 2f;
    public float MaxDropDistance = 3f;
    public float dropScale = 0.5f;
    private HotbarController hotbarController;
    private InventoryController inventoryController;
    public CircleCollider2D collider;
    private void Awake()
    {
        
        inventoryController = GameObject.FindFirstObjectByType<InventoryController>();
        hotbarController = GameObject.FindFirstObjectByType<HotbarController>();
    }

    public virtual void Pickup()
    {
        //test
        //inventoryController.AddItem(GetComponent())
        Debug.Log($"You picked up an item: {gameObject.name}");
        if (gameObject)
        {
            inventoryController.AddItem(ID);
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            ItemPopupController.Instance.ShowPopup(gameObject.name, sprite);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {

            if (!hotbarController.AddItem(gameObject.GetComponent<Item>().ID))
            {
                Pickup();

            }
            Destroy(gameObject);
        }
    }
    public virtual void Equip() {

        Debug.Log($"You equipped {this.name}");
    }
    public virtual void Drop()
    {
        Debug.Log($"You dropped {this.name}");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player not found when attempting to drop item at player position.");
            return;
        }
        Vector3 playerPosition = player.transform.position;

        //random drop pos
        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, MaxDropDistance);
        Vector2 dropPosition = (Vector2)playerPosition + dropOffset;

        //instantiate

        //scale to smaller size
        gameObject.transform.localScale = new Vector3(dropScale, dropScale, dropScale);
        Instantiate(gameObject, dropPosition, Quaternion.identity);
    }
    
}
