using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public CharacterController characterController;
    public NpcManager npcManager;
    public TMP_Text speedText, stealthText, moneyText, alarmLevelText;
    public List<Image> itemImages;
    public Slider securityMeter;
    public Slider speedMeter;


    private void Start()
    {
        UpdateItemImages(new List<Item>());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedMeter(characterController.maxSpeed);
        moneyText.text = characterController.valueStolen.ToString(CultureInfo.InvariantCulture);
    }

    public void UpdateItemImages(List<Item> items)
    {
        items.Reverse();
        for(int i = 0; i < items.Count; i++)
        {
            if (i >= itemImages.Count) break;

            var lastItem = items[i];
            itemImages[i].sprite = lastItem.image;
            itemImages[i].color = Color.white;
        }

        items.Reverse();

        for(int i = items.Count; i < itemImages.Count; i++)
        {
            itemImages[i].color = Color.clear;
        }
    }

    public void UpdateSpeedMeter(float currSpeed)
    {
        if (currSpeed > 15)
            speedMeter.value = 1;
        else
        {
            speedMeter.value = currSpeed / 15f;
        }
    }
}
