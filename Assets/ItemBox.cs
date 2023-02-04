using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private Item item1, item2;
    public List<GameObject> itemPrefabs;
    private CharacterController characterController;
    private Animator animator;
    public Canvas itemCardCanvas;
    public TMP_Text item1Name, item1Description, item1Stats;
    public TMP_Text item2Name, item2Description, item2Stats;
    public Image item1Image, item2Image;
    public Button firstButton;
    public float buttonHoldTime;
    private float startTime;
    private static readonly int Load = Animator.StringToHash("Load");
    
    public ItemSpawner itemSpawner;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        characterController.StealItem(item);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            animator.SetBool(Load, false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (characterController.inputs.interact != InputStates.WasPressedThisFrame)
        {
            animator.SetBool(Load, false);
            return;
        }
        animator.SetBool(Load, true);
    }

    public void DisplayItems()
    {
        Time.timeScale = 0;
        itemCardCanvas.gameObject.SetActive(true);
        firstButton.Select();

        item1Image.sprite = item1.image;
        item2Image.sprite = item2.image;
        
        item1Name.text = item1.name;
        item2Name.text = item2.name;
        
        item1Description.text = item1.description;
        item2Description.text = item2.description;

        item1Stats.text += "Speed Boost: " + item1.speedBoost + System.Environment.NewLine;
        item2Stats.text += "Speed Boost: " + item2.speedBoost + System.Environment.NewLine;
        
        
        item1Stats.text += "Stealth Boost: " + item1.visionBoost + System.Environment.NewLine;
        item2Stats.text += "Stealth Boost: " + item2.visionBoost + System.Environment.NewLine;
        
        
        item1Stats.text += "Security: " + item1.securityChange + System.Environment.NewLine;
        item2Stats.text += "Security: " + item2.securityChange + System.Environment.NewLine;
    }
}
