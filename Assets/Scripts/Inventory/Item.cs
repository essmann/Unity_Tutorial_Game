using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public int ID;
    public string Name;


    public virtual void Pickup()
    {

    }
    public virtual void Equip() {

        Debug.Log($"You equipped {this.name}");
    }
    
}
