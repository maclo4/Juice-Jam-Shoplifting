using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemBox : MonoBehaviour
{
    private Item item1, item2;
    public List<GameObject> itemPrefabs;
    private CharacterController characterController;
    public Canvas itemCardCanvas;
    public TMP_Text item1Name, item1Description, item1Stats;
    public TMP_Text item2Name, item2Description, item2Stats;
    public Button firstButton;
    public float buttonHoldTime;
    private float startTime;
    private void Start()
    {
        item1 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        item2 = itemPrefabs[Random.Range(0, itemPrefabs.Count)].GetComponent<Item>();
        
        itemCardCanvas.worldCamera = Camera.current;
        itemCardCanvas.gameObject.SetActive(false);
    }

    public void SelectLeftItem()
    {
        UseItem(item1);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void SelectRightItem()
    {
        UseItem(item2);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    private void UseItem(Item item)
    {
        characterController.maxSpeed += item.speedBoost;
        characterController.stealth += item.stealthBoost;
        characterController.valueStolen += item.price;
        characterController.StealItem(item);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            startTime = Time.time; 
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        //var characterController = other.gameObject.GetComponent<CharacterController>();

        if (characterController.inputs.interact != InputStates.WasPressedThisFrame)
        {
            startTime = Time.time; 
            return;
        }
        
        if (Time.time - startTime < buttonHoldTime) return;
        
        Time.timeScale = 0;
        itemCardCanvas.gameObject.SetActive(true);
        firstButton.Select();

        item1Name.text = item1.name;
        item2Name.text = item2.name;
        
        item1Description.text = item1.description;
        item2Description.text = item2.description;

        item1Stats.text += "Speed Boost: " + item1.speedBoost + System.Environment.NewLine;
        item2Stats.text += "Speed Boost: " + item2.speedBoost + System.Environment.NewLine;
        
        
        item1Stats.text += "Stealth Boost: " + item1.stealthBoost + System.Environment.NewLine;
        item2Stats.text += "Stealth Boost: " + item2.stealthBoost + System.Environment.NewLine;
        
        
        item1Stats.text += "Security: " + item1.securityChange + System.Environment.NewLine;
        item2Stats.text += "Security: " + item2.securityChange + System.Environment.NewLine;
    }
}
