using UnityEngine;

public class ReflectingBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int maxReflections = 3;
    [SerializeField] private LayerMask reflectiveLayer;
    [SerializeField] private float raycastDistance = 0.1f;

    private int currentReflections = 0;
    private Vector2 moveDirection;
    private bool isFired = false;

    public void Fire(Vector2 direction)
    {
        moveDirection = direction.normalized;
        isFired = true;
    }

    // Optional: Fire using transform.up of a shooter
    public void FireFrom(Transform shooter)
    {
        transform.position = shooter.position;
        transform.rotation = shooter.rotation;
        Fire(shooter.up);
    }

    void Update()
    {
        if (!isFired) return;

        Vector2 currentPosition = transform.position;
        Vector2 nextPosition = currentPosition + moveDirection * speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, moveDirection, raycastDistance);

        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & reflectiveLayer) != 0)
            {
                // Reflect the bullet
                if (currentReflections < maxReflections)
                {
                    moveDirection = Vector2.Reflect(moveDirection, hit.normal);
                    currentReflections++;
                    transform.position = hit.point + moveDirection * 0.01f;
                    return;
                }
                else
                {
                    Destroy(gameObject);
                    return;
                }
            }
            else
            {
                // Hit non-reflective wall
                Destroy(gameObject);
                return;
            }
        }

        // Move forward
        transform.position = nextPosition;
    }
}
