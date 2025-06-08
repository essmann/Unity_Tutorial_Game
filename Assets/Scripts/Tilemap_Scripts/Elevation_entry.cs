using UnityEngine;
using UnityEngine.Tilemaps;

public class Elevation_entry : MonoBehaviour
{

    public Collider2D mountainCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mountainCollider.enabled = false;
        mountainCollider.gameObject.GetComponent<TilemapRenderer>().sortingOrder = 0;
        Debug.Log(mountainCollider);
        Debug.Log("collision" + collision);
        Debug.Log($"The gameobject which collided with the box was: {collision.gameObject.tag}" );
        if(collision.gameObject.tag  == "Player")
        {

        }
    }

}
