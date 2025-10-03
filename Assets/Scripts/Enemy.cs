using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int health = 5;
    public bool isOnCeiling = false;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public Transform enemyFirePoint;
    public float detectionRange = 10f;
    public float shootCooldown = 0.5f;  // Faster shooting (was 2f)
    public float enemyBulletSpeed = 12f;
    public float shootAngle = 35f;  // Adjustable angle

    private Transform player;
    private float lastShootTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Create fire point if not assigned
        if (enemyFirePoint == null)
        {
            GameObject fp = new GameObject("EnemyFirePoint");
            fp.transform.SetParent(transform);
            fp.transform.localPosition = new Vector3(0, 0, 0);
            enemyFirePoint = fp.transform;
        }
    }

    void Update()
    {
        if (player != null && enemyBulletPrefab != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);

            // Check if player is in range
            if (distance <= detectionRange)
            {
                // Check cooldown
                if (Time.time - lastShootTime >= shootCooldown)
                {
                    ShootAtPlayer();
                    lastShootTime = Time.time;
                }
            }
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, Quaternion.identity);
        bullet.tag = "EnemyBullet";
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Set bullet origin (enemies shoot from their terrain)
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shotFromGround = !isOnCeiling;
        }

        if (bulletRb != null)
        {
            // Shoot at 45 degree angle towards player
            float angle = 45f * Mathf.Deg2Rad;
            float xDirection = player.position.x > transform.position.x ? 1f : -1f;
            float xVelocity = Mathf.Cos(angle) * enemyBulletSpeed * xDirection;
            float yVelocity = Mathf.Sin(angle) * enemyBulletSpeed;

            // Flip Y if enemy is on ceiling
            if (isOnCeiling)
            {
                yVelocity = -yVelocity;
            }

            bulletRb.velocity = new Vector2(xVelocity, yVelocity);
            bulletRb.gravityScale = isOnCeiling ? -2f : 2f;
        }

        Destroy(bullet, 3f);
        Debug.Log("Enemy shooting at player!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            
            if (bullet != null)
            {
                // Check if bullet can damage this enemy
                bool canDamage = false;

                // Enemy on ground: can only be hit from ceiling
                if (!isOnCeiling && !bullet.shotFromGround)
                {
                    canDamage = true;
                }
                // Enemy on ceiling: can only be hit from ground
                else if (isOnCeiling && bullet.shotFromGround)
                {
                    canDamage = true;
                }

                if (!canDamage)
                {
                    Debug.Log("Bullet passed through! Wrong position to shoot this enemy.");
                    return; // Don't damage
                }

                // Apply damage
                health--;
                Debug.Log("Enemy hit! Health: " + health);

                if (health <= 0)
                {
                    Die();
                }

                Destroy(collision.gameObject); // Destroy the bullet
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject);
    }

    // Visualize detection range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}