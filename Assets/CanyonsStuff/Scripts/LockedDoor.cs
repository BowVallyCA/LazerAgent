using UnityEngine;
using UnityEngine.InputSystem;

public class LockedDoor : MonoBehaviour
{
    public KeyCard.KeycardColor requiredKeyColor;

    private bool isOpen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.gameObject.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.HasKey(requiredKeyColor))
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        // Simple example: disable collider and animate or destroy door
        Debug.Log($"Door opened with {requiredKeyColor} keycard.");
        // Add animation, or:
        gameObject.SetActive(false);
    }
}
