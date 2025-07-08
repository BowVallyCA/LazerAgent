using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    void Start()
    {
        Collider2D tilemapCollider = GetComponent<Collider2D>();
        if (tilemapCollider == null)
        {
            Debug.LogWarning("No Collider2D found on Tilemap.");
            return;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(tilemapCollider, playerCollider);
            }
        }
    }
}
