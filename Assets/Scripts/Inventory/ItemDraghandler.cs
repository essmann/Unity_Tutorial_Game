using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDraghandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent; //item
    CanvasGroup canvasGroup;
    Slot currentSlot;
    Slot dropSlot;
    
    
    public void OnBeginDrag(PointerEventData eventData)
    {

        currentSlot = GetComponentInParent<Slot>();
        Debug.Log($"Current Slot is: {currentSlot?.name}");
        Debug.Log($"transform.root is : {transform.root}"); //UI GameObject
        if (!currentSlot)
        {

        }
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
        canvasGroup.alpha = 0.6f;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //drop the item back into the slot gameobject in hierarchy
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(currentSlot.transform);
        
        canvasGroup.alpha = 1f;


        dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        Debug.Log($"dropSlot is {dropSlot?.name}");
        if (!dropSlot)
        {
            Debug.Log("No Slot found. ");
            //check if its on top of another item
            GameObject item = eventData?.pointerEnter;

            //if the GameObject isn't another item or another slot, then we should return it to its original position

            if(!item?.GetComponent<Slot>() && !item?.GetComponentInParent<Slot>())
            {
                Debug.Log("You did NOT click a slot or an item. Resetting...");
                currentSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 
            }
            if (item)
            {
                dropSlot = item.GetComponentInParent<Slot>();
            }
        }

        if (dropSlot)
        {
            if (dropSlot.currentItem)
            {
                //Put Item B into Slot A
                //var tempItem = currentSlot.currentItem;

                //currentSlot.currentItem = dropSlot.currentItem;
                //dropSlot.currentItem = tempItem;

                //currentSlot.currentItem.transform.SetParent(currentSlot.transform);
                //dropSlot.currentItem.transform.SetParent(dropSlot.transform);
                var tempItem = currentSlot.currentItem;

                dropSlot.currentItem.transform.SetParent(currentSlot.transform);
                currentSlot.currentItem.transform.SetParent(dropSlot.transform);

                currentSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem = tempItem;
                currentSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


            }
            else
            {
                currentSlot.currentItem.transform.SetParent(dropSlot.transform);
                dropSlot.currentItem = currentSlot.currentItem;

                // Snap to center
                var test = dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                //.position = new Vector3(0, 0, 0);
                currentSlot.currentItem = null;
            }
        }
        
    void Start()
    {
     //   currentSlot = this.gameObject.GetComponentInParent<Slot>();

     //   Debug.Log($"currentslot: {currentSlot.name}");
     //canvasGroup = GetComponent<CanvasGroup>();
    }

   
}
}
