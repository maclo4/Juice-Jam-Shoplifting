using DefaultNamespace;
using UnityEngine;

public enum ItemType
{
    EnergyDrink,
    Shoes,
    Glue
}

[RequireComponent(typeof(Item))]
public class ItemManager : MonoBehaviour
{
    private Item item;

    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        
        var characterController = other.gameObject.GetComponent<CharacterController>();
        
        switch (item.type)
        {
            case ItemType.Glue:
                UseItem(characterController);
                break;
            default:
                UseItem(characterController);
                break;
        }
    }

    private void UseItem(CharacterController characterController)
    {
        if (characterController.inputs.interact == InputStates.WasPressedThisFrame)
        {
            characterController.maxSpeed += item.speedBoost;
            characterController.stealth += item.stealthBoost;
            characterController.valueStolen += item.price;
            characterController.StealItem(item);
            gameObject.SetActive(false);
        }
    }
}
