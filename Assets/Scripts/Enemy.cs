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
    public float shootCooldown = 0.5f;  
    public float enemyBulletSpeed = 12f;
    public float shootAngle = 35f;  

    private Transform player;
    private float lastShootTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        
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

            
            if (distance <= detectionRange)
            {
                
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

        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.shotFromGround = !isOnCeiling;
        }

        if (bulletRb != null)
        {
            
            float angle = 45f * Mathf.Deg2Rad;
            float xDirection = player.position.x > transform.position.x ? 1f : -1f;
            float xVelocity = Mathf.Cos(angle) * enemyBulletSpeed * xDirection;
            float yVelocity = Mathf.Sin(angle) * enemyBulletSpeed;

            
            if (isOnCeiling)
            {
                yVelocity = -yVelocity;
            }

            bulletRb.velocity = new Vector2(xVelocity, yVelocity);
            bulletRb.gravityScale = isOnCeiling ? -2f : 2f;
        }

        Destroy(bullet, 3f);
    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            
            if (bullet != null)
            {
               
                bool canDamage = false;

                
                if (!isOnCeiling && !bullet.shotFromGround)
                {
                    canDamage = true;
                }
               
                else if (isOnCeiling && bullet.shotFromGround)
                {
                    canDamage = true;
                }

                if (!canDamage)
                {
                    
                    return;
                }

                
                health--;
              

                if (health <= 0)
                {
                    Die();
                }

                Destroy(collision.gameObject); 
            }
        }
    }

    void Die()
    {
      
        Destroy(gameObject);
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}