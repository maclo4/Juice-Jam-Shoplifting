using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HudManager : Singleton<HudManager>
{
    [FormerlySerializedAs("characterController")] 
    public PlayerCharacterController playerCharacterController;
    public TMP_Text moneyText;
    public List<Image> itemImages;
    public Slider securityMeter;

    public GameObject itemCardsParent;
    public TMP_Text item1Name, item1Description, item1Price;
    public TMP_Text item2Name, item2Description, item2Price;
    public Image item1Image, item2Image;
    public Image item1Highlight, item2Highlight;
    public Button firstButton;

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
        items.Reverse();
        for(var i = 0; i < items.Count; i++)
        {
            if (i >= itemImages.Count) break;

            var lastItem = items[i];
            itemImages[i].sprite = lastItem.image;
            itemImages[i].color = Color.white;
        }

        items.Reverse();

        for(var i = items.Count; i < itemImages.Count; i++)
        {
            itemImages[i].color = Color.clear;
        }
    }
    
    public void DisplayItems(Item item1, Item item2)
    {
        _item1 = item1;
        _item2 = item2;
        
        itemCardsParent.SetActive(true);
        firstButton.Select();
        
        item1Image.sprite = _item1.image;
        item2Image.sprite = _item1.image;
        
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
        playerCharacterController.StealItem(item);
    }
}
