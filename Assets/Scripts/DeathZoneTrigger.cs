using UnityEngine;

public class DeathZoneTrigger : MonoBehaviour
{
    public GameObject gameOverPanel; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            gameOverPanel.SetActive(true);

            
            Time.timeScale = 0f;
        }
    }
}
