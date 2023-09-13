using UnityEngine;

public class CapturePlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") ) return;

        var characterController = other.GetComponent<PlayerCharacterController>();
        characterController.Caught();
    }
}
