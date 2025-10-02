using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool shotFromGround = true; // Set by player when shooting

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if bullet hit enemy
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                // CONSTRAINT: Can only damage enemy from opposite terrain
                bool canDamage = false;

                // Enemy on ground: can only be hit from ceiling (shotFromGround = false)
                if (!enemy.isOnCeiling && !shotFromGround)
                {
                    canDamage = true;
                }
                // Enemy on ceiling: can only be hit from ground (shotFromGround = true)
                else if (enemy.isOnCeiling && shotFromGround)
                {
                    canDamage = true;
                }

                if (!canDamage)
                {
                    Debug.Log("Can't hit this enemy! Switch gravity first!");
                    // Bullet passes through without triggering damage
                    return;
                }
            }
        }
    }
}