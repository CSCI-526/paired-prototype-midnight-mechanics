using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public TextMeshProUGUI healthText;
    
    // Game Over UI
    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        
        // Make sure game over panel is hidden at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if hit by enemy bullet
        if (collision.CompareTag("EnemyBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            // Make sure it's an enemy bullet (check if it came from enemy terrain)
            if (bullet != null)
            {
                TakeDamage(1);
                Destroy(collision.gameObject); // Destroy bullet
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
        
        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        // Pause the game
        Time.timeScale = 0f;
        
        // Optionally destroy the player
        // Destroy(gameObject);
    }

    // Method to restart the game (call from Play Again button)
    public void RestartGame()
    {
        // Resume time
        Time.timeScale = 1f;
        
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}