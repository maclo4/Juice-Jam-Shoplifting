using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    [SerializeField] public Item item1;
    [SerializeField] public Item item2;
    public List<GameObject> itemPrefabs;
    private PlayerCharacterController _playerCharacterController;
    private Animator _animator;
    private Animator _itemBoxAnimator;
    public Canvas itemCardCanvas;
    public TMP_Text item1Name, item1Description, item1Stats, item1Price;
    public TMP_Text item2Name, item2Description, item2Stats, item2Price;
    public Image item1Image, item2Image;
    public Image item1Highlight, item2Highlight;
    public GameObject highlightedImage;
    public Button firstButton;
    public float buttonHoldTime;
    private float _startTime;
    private static readonly int Load = Animator.StringToHash("Load");
    
    public ItemSpawner itemSpawner;
    private static readonly int Highlighted = Animator.StringToHash("Highlighted");

    private void Start()
    {
        //itemBoxAnimator = GetComponent<Animator>();
        _animator = GetComponent<Animator>();
        item1 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        item2 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        
        itemCardCanvas.worldCamera = Camera.current;
        itemCardCanvas.gameObject.SetActive(false);
    }

    public void SelectLeftItem()
    {
        AddItemToInventory(item1);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void SelectRightItem()
    {
        AddItemToInventory(item2);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void AddItemToInventory(Item item)
    {
        itemSpawner.AddSpawnLocation(transform);
        _playerCharacterController.StealItem(item);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        //animator.SetBool(Highlighted, true);
        highlightedImage.SetActive(true);
        _playerCharacterController = other.gameObject.GetComponent<PlayerCharacterController>();
        if (_playerCharacterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            _animator.SetBool(Load, false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (_playerCharacterController.inputs.interact != InputStates.WasPressedThisFrame)
        {
            _animator.SetBool(Load, false);
            return;
        }
        _animator.SetBool(Load, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        highlightedImage.SetActive(false);
        _animator.SetBool(Load, false);
    }

    public void DisplayItems()
    {
        Debug.Log("display items");
        itemCardCanvas.gameObject.SetActive(true);
        firstButton.Select();
        highlightedImage.SetActive(false);
        
        item1Image.sprite = item1.image;
        item2Image.sprite = item2.image;
        
        item1Name.text = item1.name;
        item2Name.text = item2.name;
        
        item1Description.text = item1.description;
        item2Description.text = item2.description;

        item1Price.text = "$" + item1.price;
        item2Price.text = "$" + item2.price;


        var alpha = item1Highlight.color.a;
        switch (item1.type)
        {
            case ItemType.Money:
                item1Highlight.color = Color.green;
                item1Highlight.color = new Color(item1Highlight.color.r, item1Highlight.color.g,
                    item1Highlight.color.b,alpha);
                break;
            default:
                item1Highlight.color = Color.white;
                item1Highlight.color = new Color(item1Highlight.color.r, item1Highlight.color.g,
                    item1Highlight.color.b,alpha);
                break;
        }

        switch (item2.type)
        {
            case ItemType.Money:
                item2Highlight.color = Color.green;
                item2Highlight.color = new Color(item2Highlight.color.r, item2Highlight.color.g,
                    item2Highlight.color.b,alpha);
                break;
            default:
                item2Highlight.color = Color.white;
                item2Highlight.color = new Color(item2Highlight.color.r, item2Highlight.color.g,
                    item2Highlight.color.b,alpha);
                break;
        }

        Time.timeScale = 0;
    }
    
}
