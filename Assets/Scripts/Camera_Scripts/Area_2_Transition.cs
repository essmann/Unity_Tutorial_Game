using Unity.Cinemachine;
using UnityEngine;

public class Area_2_Transition : MonoBehaviour
{
    public PolygonCollider2D destinationMapBoundary;
    
      CinemachineConfiner2D confiner;
     [SerializeField] Direction direction;
    enum Direction { Up, Down, Left, Right};
    

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Someone just entered?");
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(confiner);
            
            Debug.Log("New boundary set: " + confiner.BoundingShape2D);
            confiner.BoundingShape2D = destinationMapBoundary;
            UpdatePlayerPosition(collision.gameObject);
            confiner.InvalidateBoundingShapeCache();


        }
    }

    private void UpdatePlayerPosition(GameObject player) {

        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 7;
                break;
            case Direction.Down:
                newPos.y -= 7;
                break;
            case Direction.Left:
                newPos.x -= 7;
                break;
            case Direction.Right:
                newPos.x += 7;
                break;
        }
        player.transform.position = newPos;
    }
    
}
