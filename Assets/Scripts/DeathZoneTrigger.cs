using UnityEngine;

public class DeathZoneTrigger : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign your panel in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Show Game Over panel
            gameOverPanel.SetActive(true);

            // Stop the game (optional)
            Time.timeScale = 0f;
        }
    }
}
