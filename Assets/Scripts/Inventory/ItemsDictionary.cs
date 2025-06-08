using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    public Dictionary<int, GameObject> itemDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();
        int i = 0;
        while (i < itemPrefabs.Count)
        {
            if (itemPrefabs[i])
            {
                itemPrefabs[i].ID = i + 1;  
            }
            i++;
        }
        foreach (Item item in itemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItem(int itemID) {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);

        if (!prefab) {
            Debug.LogWarning($"Item with ID: {itemID} not found in dictionary.");
        }
            return prefab;
        
    
    }

    
}
        