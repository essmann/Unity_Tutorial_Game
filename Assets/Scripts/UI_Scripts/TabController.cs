using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabIndex) {

        int i = 0;
        Debug.Log($"Tab index: {tabIndex}");
        Debug.Log($"Pages length: {pages.Length}");

        while (i < pages.Length)
        {
            Debug.Log(i);
            Debug.Log(pages.Length);
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
            i++;
        }
        pages[tabIndex].SetActive(true);
        tabImages[tabIndex].color = Color.white;
    
    }
}
