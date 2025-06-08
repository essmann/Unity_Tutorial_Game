using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    private ItemsDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemsDictionary>();
        //int i = 0;
        //while (i < slotCount)
        //{
        //    Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //    slot.name += " " + i;
        //    //Get component actually gets the slot 

        //    //Similar to ctrl + D, we simply instantiate new instance of the slot and supply the prefab and its position (parent i guess).

        //    if (i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        //    i++;
        //    //}

        //}
    }
    /// <summary>
    ///Method <c>GetInventoryItems</c> loops through inventory slots, finds the item in the slot, creates an InventorySaveData object and stores its ID and its index
    ///and adds all those objects to a List and returns it.
    /// </summary>
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> inventoryData = new List<InventorySaveData>();
        int index = 0;
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            {
                Slot slot = slotTransform.GetComponent<Slot>();
                if (slot.currentItem)
                {
                    Item item = slot.currentItem.GetComponent<Item>();

                    inventoryData.Add(new InventorySaveData { ID = item.ID, inventoryIndex = index });
                    
                }
                
                index++;

            }
        }
        return inventoryData;


    }
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        // 1. Clear existing slots
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Destroy(slotTransform.gameObject);
        }

        // 2. Recreate empty slots
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        // 3. Populate with saved items
        foreach (InventorySaveData saveData in inventorySaveData)
        {
            // Null check and boundary check
            if (saveData != null && saveData.inventoryIndex >= 0 && saveData.inventoryIndex < slotCount)
            {
                // Get correct slot
                Transform slotTransform = inventoryPanel.transform.GetChild(saveData.inventoryIndex);
                Slot slot = slotTransform.GetComponent<Slot>();

                // Get item prefab from dictionary
                GameObject itemPrefab = itemDictionary.GetItem(saveData.ID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slotTransform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
                else
                {
                    Debug.LogWarning($"Item with ID {saveData.ID} not found.");
                }
            }
            else
            {
                Debug.LogWarning("Invalid save data entry: null or index out of bounds.");
            }
        }
    }

}
