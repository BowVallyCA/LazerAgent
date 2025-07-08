using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;

    [SerializeField] public float rayLength = 5f;
    [SerializeField] public LineRenderer lineRenderer;
    [SerializeField] private int maxReflections = 3;
    [SerializeField] private LayerMask reflectiveLayer;
    [SerializeField] private GameObject laserOrgin;

    [SerializeField] private Transform fireOrgin;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float ammoCount = 20f;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private GameObject loseScreen;

    private int health = 3;
    public UnityEvent<int> damaged;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private Vector2 playerDirection;
   

    private bool invincible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerDirection();
        UpdateMouseRotation();

        Vector2 origin = laserOrgin.transform.position;
        Vector2 direction = transform.up;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, origin);

        int reflections = 0;
        float remainingLength = rayLength;

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }

        while (reflections < maxReflections)
        {
            // Raycast against ALL colliders
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, remainingLength);

            if (hit.collider != null)
            {
                Vector2 hitPoint = hit.point;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitPoint);

                // Check if the object hit is on the reflective layer
                if (((1 << hit.collider.gameObject.layer) & reflectiveLayer) != 0)
                {
                    // Reflect direction and continue
                    direction = Vector2.Reflect(direction, hit.normal);
                    remainingLength -= Vector2.Distance(origin, hitPoint);
                    origin = hitPoint + direction * 0.01f; // Small offset to avoid same-hit loop
                    reflections++;
                }
                else
                {
                    // Hit non-reflective wall — stop here
                    break;
                }
            }
            else
            {
                // No hit: draw the rest of the laser in current direction
                Vector2 endPoint = origin + direction * remainingLength;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(playerDirection.x * _playerSpeed, playerDirection.y * _playerSpeed);
    }

    private void UpdatePlayerDirection()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(directionX, directionY).normalized;
    }

    private void UpdateMouseRotation()
    {
        // Convert mouse position to world position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure it's in 2D plane (top-down)

        // Calculate direction from object to mouse
        Vector3 direction = mouseWorldPosition - transform.position;

        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation around Z axis
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    IEnumerator Hurt()
    {
        if (!invincible)
        {
            invincible = true;
            spriteRenderer.color = new Color(255,255,255,0.5f);
            health -= 1;
            damaged.Invoke(health);
            yield return null;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(3f);
            spriteRenderer.color = Color.white;
            invincible = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            StartCoroutine(Hurt());
        }
    }

    void FireBullet()
    {
        if(ammoCount > 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, fireOrgin.position, fireOrgin.rotation);
            ReflectingBullet bulletScript = bullet.GetComponent<ReflectingBullet>();
            bulletScript.Fire(fireOrgin.up); // Fire in the direction the player is facing

            ammoCount -= 1f;
            ammoText.SetText($"{ammoCount}/20");
        }
        else
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
