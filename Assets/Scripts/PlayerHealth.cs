using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    
    
    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("EnemyBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            
            if (bullet != null)
            {
                TakeDamage(1);
                Destroy(collision.gameObject); 
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(2);
            Debug.Log("Player touched enemy!");

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        
      
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        
        Time.timeScale = 0f;
        
       
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}