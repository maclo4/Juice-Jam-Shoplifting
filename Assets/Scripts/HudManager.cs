using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public CharacterController characterController;
    public NpcManager npcManager;
    public TMP_Text speedText, stealthText, moneyText, alarmLevelText;
  

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Max speed: " + characterController.maxSpeed;;
        stealthText.text = "Stealth: " + characterController.stealth;
        moneyText.text = "Value Stolen: $" + characterController.valueStolen;
        alarmLevelText.text = "# Guards: " + npcManager.GetSecurityLevel();
    }
}
