using System.IO;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.AI;


public class SaveController : MonoBehaviour
{
    private InventoryController inventoryController;
    private HotbarController hotbarController;
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
        hotbarController = FindFirstObjectByType<HotbarController>();
        Debug.Log("Start function called in SaveController.");
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        Debug.Log($"save location: {saveLocation}");
        LoadGame();
    }
    public void OpenSaveLocation()
    {
        string path = Application.persistentDataPath;

        try
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{path}\"",
                UseShellExecute = true
            };

            System.Diagnostics.Process.Start(startInfo);

            Debug.Log($"Opening save location: {path}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to open path: {path}\n{ex.Message}");
        }
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = GameObject.FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name,
            inventoryData = inventoryController.GetInventoryItems(),
            hotbarData = hotbarController.GetHotbarItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        
        if (File.Exists(saveLocation))
        {
            Debug.Log("Save found. Saving Game...");
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            GameObject.FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            

            inventoryController.SetInventoryItems(saveData.inventoryData);
            hotbarController.SetHotbarItems(saveData.hotbarData);

        }
        else
        {
            Debug.Log("Save not found. Saving Game...");

            SaveGame();
        }
    }
}
