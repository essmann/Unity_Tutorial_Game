using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10;

    private ItemsDictionary itemDictionary;

    private Key[] hotbarKeys;

    private Slot activeSlot;

    private string defaultColor = "#574D4D";
    private string activeColor = "#877777";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        itemDictionary = FindFirstObjectByType<ItemsDictionary>();

        hotbarKeys = new Key[slotCount];

        int i = 0;
        while (i < slotCount)
        {
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
            //Debug.Log($"AWAKE INITIATING HOTBAR SLOTS: {slotPrefab.name}");
            //GameObject slot = Instantiate(slotPrefab, hotbarPanel.transform);
            i++;
        }
    }

    private void Update()
    {
        int i = 0;
        while (i < slotCount)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                PressHotKey(i);
            }
            i++;
        }
    }
    private Slot GetHotKeySlotByIndex(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        return slot;

    }

    private void PressHotKey(int index)
    {
        Slot selectedSlot = GetHotKeySlotByIndex(index);
        //clear already active slot if it exists
        if (activeSlot && activeSlot != selectedSlot)
        {
            //reset to default color
            Color color;
            if (ColorUtility.TryParseHtmlString(defaultColor, out color))
            {
                activeSlot.GetComponent<Image>().color = color;
            }
            else
            {
                Debug.LogError("Invalid hex color string");
            }
        }

        Debug.Log($"You selected slot: {selectedSlot.name}");
        activeSlot = selectedSlot;
        Color newColor;
        if (ColorUtility.TryParseHtmlString(activeColor, out newColor))
        {
            activeSlot.GetComponent<Image>().color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color string");
        }
        if (selectedSlot.currentItem)
        {
            Item item = selectedSlot.currentItem.GetComponent<Item>();
            item.Equip();
        }
    }


    public List<InventorySaveData> GetHotbarItems() {
        List<InventorySaveData> inventoryData = new List<InventorySaveData>();
        int index = 0;
        foreach (Transform slotTransform in hotbarPanel.transform)
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
    public void SetHotbarItems(List<InventorySaveData> inventorySaveData)
    {
        // 1. Clear existing slots
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Destroy(slotTransform.gameObject);
        }

        // 2. Recreate empty slots
        for (int i = 0; i < slotCount; i++)
        {
            GameObject _prefab = Instantiate(slotPrefab, hotbarPanel.transform);
            _prefab.name += i;
            Debug.Log($"Instantiated hotbar slot: {slotPrefab.name}");

        }

        // 3. Populate with saved items
        foreach (InventorySaveData saveData in inventorySaveData)
        {
            // Null check and boundary check
            if (saveData != null && saveData.inventoryIndex >= 0 && saveData.inventoryIndex < slotCount)
            {
                // Get correct slot
                Transform slotTransform = hotbarPanel.transform.GetChild(saveData.inventoryIndex);
                Slot slot = slotTransform.GetComponent<Slot>();

                // Get item prefab from dictionary
                GameObject itemPrefab = itemDictionary.GetItem(saveData.ID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slotTransform);
                    Debug.Log($"slot transform: {slotTransform.GetComponent<Slot>().name}");
                    Debug.Log($"Instantiated hotbar item: {item.name}");
                    Debug.Log($"Instantiated item is in: {item.GetComponentInParent<Slot>().name}");
                    item.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
                    item.GetComponent<Image>().color = Color.white;
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

    public bool AddItem(int ID)
    {
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            {
                Slot slot = slotTransform.GetComponent<Slot>();
                if (!slot.currentItem)
                {
                    GameObject _item = itemDictionary.GetItem(ID);
                    GameObject item = Instantiate(_item, slotTransform);
                    Debug.Log($"slot transform: {slotTransform.GetComponent<Slot>().name}");
                    Debug.Log($"Instantiated hotbar item: {item.name}");
                    Debug.Log($"Instantiated item is in: {item.GetComponentInParent<Slot>().name}");
                    item.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 2);
                    item.GetComponent<Image>().color = Color.white;
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                    return true;


                }

               

            }
        }
        return false;
    }

}
