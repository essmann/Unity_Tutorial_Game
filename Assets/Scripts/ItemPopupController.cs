using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupController : MonoBehaviour
{
   public static ItemPopupController Instance { get; private set; }
    
   public int maxPopups = 5;
    public float duration;
    public GameObject popupPrefab;
    public Sprite testSprite;


    private readonly Queue<GameObject> activePopups = new Queue<GameObject>();

        private void Awake()
    {
        
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ItemPopupController instances detected");
            Destroy(gameObject);
        }
        //int i = 0;
        //while (i < 4)
        //{
        //    ShowPopup($"Test {i}", testSprite);
        //    i++;
        //}
        

    }

    public void ShowPopup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();

        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        activePopups.Enqueue(newPopup);
        if(activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }

        StartCoroutine(FadeOutAndDestroy(newPopup));


    }
    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(duration);
        if (popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        for(float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;
            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }
        Destroy(popup);
    }
}
