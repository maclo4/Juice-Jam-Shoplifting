using System;
using System.Collections.Generic;
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


    private void Start()
    {
        UpdateItemImages(new List<Item>());
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Max speed: " + characterController.maxSpeed;;
        stealthText.text = "Stealth: " + characterController.visionRange;
        moneyText.text = "$$$: " + characterController.valueStolen;
        alarmLevelText.text = "# Guards: " + npcManager.GetSecurityLevel();
    }

    public void UpdateItemImages(List<Item> items)
    {
        int j = 0;
        for(int i = items.Count -1; i >= 0; i--)
        {
            itemImages[i].sprite = items[j].image;
            itemImages[i].color = Color.white;
            j++;
        }

        for(int i = items.Count; i < itemImages.Count; i++)
        {
            itemImages[i].color = Color.clear;
        }
    }
}
