using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameObject winPanel; 
    public bool pauseGame = true; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Level Completed!");

            if (winPanel != null)
                winPanel.SetActive(true);

            if (pauseGame)
                Time.timeScale = 0f; 
        }
    }

    
    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
