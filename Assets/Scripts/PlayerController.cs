using UnityEngine;

public class PlayerController2d : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;

    private Rigidbody2D rb;
    private bool isFlipped = false;   
    private bool facingRight = true; 
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

       
        if (move > 0 && !facingRight)
        {
            facingRight = true;
            FlipHorizontal();
        }
        else if (move < 0 && facingRight)
        {
            facingRight = false;
            FlipHorizontal();
        }

        
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale *= -1;
            isFlipped = !isFlipped;
            FlipVertical();
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FlipHorizontal()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }

    void FlipVertical()
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1; 
        transform.localScale = scale;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.tag = "PlayerBullet";
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shotFromGround = !isFlipped;
            }
            
            if (bulletRb != null)
            {
                float angle = 45f * Mathf.Deg2Rad;
                float xVelocity = Mathf.Cos(angle) * bulletSpeed;
                float yVelocity = Mathf.Sin(angle) * bulletSpeed;

                if (isFlipped) yVelocity = -yVelocity;

                
                xVelocity *= facingRight ? 1 : -1;
                
                bulletRb.velocity = new Vector2(xVelocity, yVelocity);
                bulletRb.gravityScale = isFlipped ? -2f : 2f;
            }

            Destroy(bullet, 3f);
        }
    }
}