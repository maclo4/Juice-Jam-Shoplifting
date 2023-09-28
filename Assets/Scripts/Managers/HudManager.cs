using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HudManager : Singleton<HudManager>
{
    [FormerlySerializedAs("characterController")] 
    public PlayerCharacterController playerCharacterController;
    public TMP_Text moneyText;
    public TMP_Text additionalItemsText;
    public List<Image> itemImages;
    public Slider securityMeter;

    public GameObject itemCardsParent;
    public TMP_Text item1Name, item1Description, item1Price;
    public TMP_Text item2Name, item2Description, item2Price;
    public Image item1Image, item2Image;
    public Image item1Highlight, item2Highlight;
    public List<Button> itemButtons;

    private Item _item1, _item2;
    private ItemSpawner _itemSpawner;

    private void Start()
    {
        _itemSpawner = ItemSpawner.Instance;
        UpdateItemImages(new List<Item>());
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = playerCharacterController.valueStolen.ToString(CultureInfo.InvariantCulture);
    }

    public void UpdateItemImages(List<Item> items)
    {
        for(var i = 0; i < items.Count && i < itemImages.Count; i++)
        {
            itemImages[i].sprite = items[i].image;
            itemImages[i].color = Color.white;
        }
        if (items.Count > itemImages.Count)
        {
            itemImages.Last().color = Color.clear;
            itemImages.Last().sprite = null;
            additionalItemsText.text = "+" + (items.Count - itemImages.Count + 1);
        }

        if (items.Count == itemImages.Count)
            additionalItemsText.text = "";
        
        for(var i = items.Count; i < itemImages.Count; i++)
        {
            itemImages[i].sprite = null;
            itemImages[i].color = Color.clear;
        }
    }
    
    //So bad
    public void DisplayItems(Item item1, Item item2)
    {
        _item1 = item1;
        _item2 = item2;
        
        itemCardsParent.SetActive(true);
        itemButtons.First().Select();
        
        item1Image.sprite = _item1.image;
        item2Image.sprite = _item2.image;
        
        item1Name.text = _item1.name;
        item2Name.text = _item2.name;
        
        item1Description.text = _item1.description;
        item2Description.text = _item2.description;

        item1Price.text = "$" + _item1.price;
        item2Price.text = "$" + _item2.price;


        var alpha = item1Highlight.color.a;
        switch (_item1.type)
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

        switch (_item2.type)
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
    
    public void SelectLeftItem()
    {
        AddItemToInventory(_item1);
        Time.timeScale = 1;
        itemCardsParent.SetActive(false);
    }
    public void SelectRightItem()
    {
        AddItemToInventory(_item2);
        Time.timeScale = 1;
        itemCardsParent.SetActive(false);
    }

    private void AddItemToInventory(Item item)
    {
        _itemSpawner.AddSpawnLocation(transform);
        playerCharacterController.StealItem(item);
    }

    //This is so bad
    public void DisplayItemDetails(GameObject selectedItemObject)
    {
        if (selectedItemObject.GetInstanceID() == itemButtons[0].GetInstanceID())
        {
            item1Description.gameObject.SetActive(true);
            item2Description.gameObject.SetActive(false);

            item1Price.gameObject.SetActive(true);
            item2Price.gameObject.SetActive(false);

            item1Highlight.gameObject.SetActive(true);
            item2Highlight.gameObject.SetActive(false);
        }
        
        if (selectedItemObject.GetInstanceID() == itemButtons[1].GetInstanceID())
        {
            item1Description.gameObject.SetActive(false);
            item2Description.gameObject.SetActive(true);

            item1Price.gameObject.SetActive(false);
            item2Price.gameObject.SetActive(true);
            
            item1Highlight.gameObject.SetActive(false);
            item2Highlight.gameObject.SetActive(true);
        }
    }
}
