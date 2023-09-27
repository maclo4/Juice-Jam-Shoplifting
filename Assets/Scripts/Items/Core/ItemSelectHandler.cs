using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ItemSelectHandler : MonoBehaviour, ISelectHandler
{
    private HudManager _hudManager;
    private void Start()
    {
        _hudManager = HudManager.Instance;
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject.name == "Item Card")
        {
            _hudManager.item1Description.gameObject.SetActive(true);
            _hudManager.item2Description.gameObject.SetActive(false);

            _hudManager.item1Price.gameObject.SetActive(true);
            _hudManager.item2Price.gameObject.SetActive(false);

            _hudManager.item1Highlight.gameObject.SetActive(true);
            _hudManager.item2Highlight.gameObject.SetActive(false);
        }
        
        if (eventData.selectedObject.name == "Item Card 2")
        {
            _hudManager.item1Description.gameObject.SetActive(false);
            _hudManager.item2Description.gameObject.SetActive(true);

            _hudManager.item1Price.gameObject.SetActive(false);
            _hudManager.item2Price.gameObject.SetActive(true);
            
            _hudManager.item1Highlight.gameObject.SetActive(false);
            _hudManager.item2Highlight.gameObject.SetActive(true);
        }
    }
    
}
