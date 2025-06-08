using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetKeyDown(KeyCode.Tab));
        //Debug.Log(Input.GetKeyDown(KeyCode.V));
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //Debug.Log(Input.GetKeyDown(KeyCode.Tab));
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
