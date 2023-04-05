using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelectHandler : MonoBehaviour, ISelectHandler
{
    public ItemBox _itemBox;

    public void OnSelect(BaseEventData eventData)
    {
        
        if (eventData.selectedObject.name == "Item Card")
        {
            //_itemBox.item1Name.gameObject.SetActive(true);
            //_itemBox.item2Name.gameObject.SetActive(false);
        
            _itemBox.item1Description.gameObject.SetActive(true);
            _itemBox.item2Description.gameObject.SetActive(false);

            _itemBox.item1Price.gameObject.SetActive(true);
            _itemBox.item2Price.gameObject.SetActive(false);

            _itemBox.item1Highlight.gameObject.SetActive(true);
            _itemBox.item2Highlight.gameObject.SetActive(false);
        }
        
        if (eventData.selectedObject.name == "Item Card 2")
        {
            //_itemBox.item1Name.gameObject.SetActive(false);
            //_itemBox.item2Name.gameObject.SetActive(true);
        
            _itemBox.item1Description.gameObject.SetActive(false);
            _itemBox.item2Description.gameObject.SetActive(true);

            _itemBox.item1Price.gameObject.SetActive(false);
            _itemBox.item2Price.gameObject.SetActive(true);
            
            _itemBox.item1Highlight.gameObject.SetActive(false);
            _itemBox.item2Highlight.gameObject.SetActive(true);
        }
    }
    
}
