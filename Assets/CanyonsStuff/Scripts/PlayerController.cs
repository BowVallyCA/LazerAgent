using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] public float rayLength = 5f;
    [SerializeField] public LineRenderer lineRenderer;

    private Rigidbody2D rigidBody;
    private Vector2 playerDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerDirection();
        UpdateMouseRotation();

        Vector3 direction = transform.up; // Or transform.up depending on character facing
        Vector3 origin = transform.position;
        Vector3 endPoint = origin + direction * rayLength;

        // Update LineRenderer points
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, endPoint);
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
}
