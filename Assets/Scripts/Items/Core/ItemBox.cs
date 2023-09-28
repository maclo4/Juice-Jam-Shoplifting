using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{ 
    private Item _item1;
    private Item _item2;
    public List<GameObject> itemPrefabs;
    private PlayerCharacterController _playerCharacterController;
    private Animator _itemBoxAnimator;
    
    public GameObject helperText;
    private float _startTime;
    

    private void Start()
    {
        SelectRandomItems();
    }

    private void SelectRandomItems()
    {
        _item1 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        _item2 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        while (_item2.type == _item1.type)
        {
            _item2 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        helperText.SetActive(true);
        var playerCharacterController = other.gameObject.GetComponent<PlayerCharacterController>();
        if (playerCharacterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            DisplayItems();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        var playerCharacterController = other.gameObject.GetComponent<PlayerCharacterController>();
        if (playerCharacterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            DisplayItems();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        helperText.SetActive(false);
    }

    private void DisplayItems()
    {
        HudManager.Instance.DisplayItems(_item1, _item2);
        gameObject.SetActive(false);
    }
    
}
