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
    public void UseItem(CharacterController characterController)
    {
        switch (type)
        {
            case ItemType.Gum:
                PlaceGlue(characterController.transform.position);
                break;
            case ItemType.Lamp:
                PlaceLamp(characterController.transform.position);
                break;
            default:
                characterController.maxSpeed += speedBoost;
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
                characterController.IncreaseVisionRange(-visionBoost);
                break;
        }
    }

    private void PlaceGlue(Vector3 spawnLocation)
    {
        Instantiate(gumTrap, spawnLocation, Quaternion.identity);
    }
    private void PlaceLamp(Vector3 spawnLocation)
    {
        Instantiate(lampTrap, spawnLocation, Quaternion.identity);
    }
}