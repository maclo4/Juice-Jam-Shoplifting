using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private PlayerCharacterController _playerCharacterController;
    public Transform destinationTeleporter;
    public GameObject highlightText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        highlightText.SetActive(true);
        _playerCharacterController = other.gameObject.GetComponent<PlayerCharacterController>();
        if (_playerCharacterController.inputs.interact == InputStates.WasPressedThisFrame && !_playerCharacterController.teleporting)
        {
            _playerCharacterController.BeginTeleportPlayer(destinationTeleporter.position);
        }
        
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (_playerCharacterController.inputs.interact == InputStates.WasPressedThisFrame && !_playerCharacterController.teleporting)
        {
            _playerCharacterController.BeginTeleportPlayer(destinationTeleporter.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        highlightText.SetActive(false);
    }

}
