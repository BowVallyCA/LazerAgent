using UnityEngine;

public class KeyCard : MonoBehaviour
{
    public enum KeycardColor { Red, Green, Blue }
    public KeycardColor color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectKeycard(color);
                Destroy(gameObject); // Remove keycard from scene
            }
        }
    }
}
