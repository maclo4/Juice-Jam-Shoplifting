using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButtonOnHighlight : MonoBehaviour, IPointerEnterHandler
{
    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
       _eventSystem.SetSelectedGameObject(gameObject); 
    }
}