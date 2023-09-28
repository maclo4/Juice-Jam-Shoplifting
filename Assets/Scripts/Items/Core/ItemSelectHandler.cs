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
        HudManager.Instance.DisplayItemDetails(eventData.selectedObject);
    }
    
}
