using UnityEngine;

public class PlayerController2d : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;

    private Rigidbody2D rb;
    private bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move left/right
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Flip gravity with G
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale *= -1;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, 1);
            isFlipped = !isFlipped;
        }

        // Shoot with Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            
            // Track where bullet was shot from
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shotFromGround = !isFlipped; // true if on ground, false if on ceiling
            }
            
            if (bulletRb != null)
            {
                // 45 degree angle: equal X and Y components
                float angle = 45f * Mathf.Deg2Rad;
                float xVelocity = Mathf.Cos(angle) * bulletSpeed;
                float yVelocity = Mathf.Sin(angle) * bulletSpeed;
                
                // Flip Y direction if player is upside down
                if (isFlipped)
                {
                    yVelocity = -yVelocity;
                }
                
                bulletRb.velocity = new Vector2(xVelocity, yVelocity);
                bulletRb.gravityScale = isFlipped ? -2f : 2f; // Gravity affects trajectory
            }

            Destroy(bullet, 3f); // Destroy bullet after 5 seconds
        }
    }
}