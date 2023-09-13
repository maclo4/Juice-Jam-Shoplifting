using UnityEngine;
using UnityEngine.Serialization;

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
    public bool usable;

    public GameObject gumTrap;
    public GameObject lampTrap;
    public void UseItem(PlayerCharacterController playerCharacterController)
    {
        switch (type)
        {
            case ItemType.Gum:
                PlaceGlue(playerCharacterController.transform.position);
                break;
            case ItemType.Lamp:
                PlaceLamp(playerCharacterController.transform.position);
                break;
            default:
                playerCharacterController.maxSpeed += speedBoost;
                playerCharacterController.IncreaseVisionRange(visionBoost);
                break;
        }
    }
    
    public void RemoveItemEffects(PlayerCharacterController playerCharacterController)
    {
        switch (type)
        {
            default:
                playerCharacterController.maxSpeed -= speedBoost;
                playerCharacterController.IncreaseVisionRange(-visionBoost);
                break;
        }
    }

    private void PlaceGlue(Vector3 spawnLocation)
    {
        Instantiate(gumTrap, spawnLocation, Quaternion.identity);
    }
    private void PlaceLamp(Vector3 spawnLocation)
    {
        var lamp = Instantiate(lampTrap, spawnLocation, Quaternion.identity);
        lamp.SetActive(true);
    }
}