using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemType type;
    public int price;
    public float speedBoost;
    [FormerlySerializedAs("stealthBoost")] public float visionBoost;
    public int securityChange;
    public string description;
    public Sprite image;
    public int duration = 5;

    public void UseItem(CharacterController characterController)
    {
        switch (type)
        {
            default:
                characterController.maxSpeed += speedBoost;
                characterController.visionRange += visionBoost;
                characterController.valueStolen += price;
                characterController.IncreaseVisionRange(visionBoost);
                break;
        }
    }
    
    public void RemoveItemEffects(CharacterController characterController)
    {
        switch (type)
        {
            default:
                characterController.maxSpeed -= speedBoost;
                characterController.visionRange -= visionBoost;
                characterController.valueStolen -= price;
                characterController.IncreaseVisionRange(visionBoost);
                break;
        }
    }
}