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
    
    private ItemSpawner _itemSpawner;

    private void Start()
    {
        _itemSpawner = ItemSpawner.Instance;
        _item1 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        _item2 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
    }

    public void SelectLeftItem()
    {
        AddItemToInventory(_item1);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void SelectRightItem()
    {
        AddItemToInventory(_item2);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void AddItemToInventory(Item item)
    {
        _itemSpawner.AddSpawnLocation(transform);
        _playerCharacterController.StealItem(item);
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
        helperText.SetActive(false);
        HudManager.Instance.DisplayItems(_item1, _item2);
    }
    
}
