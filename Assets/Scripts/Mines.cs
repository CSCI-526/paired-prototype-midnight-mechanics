using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage = 5;
    public bool destroyOnHit = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Mine hit player!");
                
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}