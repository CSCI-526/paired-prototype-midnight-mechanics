using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool shotFromGround = true; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                
                bool canDamage = false;

               
                if (!enemy.isOnCeiling && !shotFromGround)
                {
                    canDamage = true;
                }
                
                else if (enemy.isOnCeiling && shotFromGround)
                {
                    canDamage = true;
                }

                if (!canDamage)
                {
                    Debug.Log("Can't hit this enemy! Switch gravity first!");
                    
                    return;
                }
            }
        }
    }
}