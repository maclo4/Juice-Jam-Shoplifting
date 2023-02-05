using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private CharacterController characterController;
    public Transform destinationTeleporter;
    public GameObject highlightText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        highlightText.SetActive(true);
        characterController = other.gameObject.GetComponent<CharacterController>();
        if (characterController.inputs.interact == InputStates.WasPressedThisFrame && !characterController.teleporting)
        {
            characterController.BeginTeleportPlayer(destinationTeleporter.position);
        }
        
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (characterController.inputs.interact == InputStates.WasPressedThisFrame && !characterController.teleporting)
        {
            characterController.BeginTeleportPlayer(destinationTeleporter.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        highlightText.SetActive(false);
    }

}
