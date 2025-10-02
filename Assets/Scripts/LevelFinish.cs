// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class LevelFinish : MonoBehaviour
// {
//     public GameObject finishPanel; // Optional: reuse Game Over panel
//     public bool pauseGame = true; // Whether to freeze the game

//     void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             Debug.Log("Level Completed!");

//             if (finishPanel != null)
//                 finishPanel.SetActive(true);

//             if (pauseGame)
//                 Time.timeScale = 0f;

//             // Optionally, you can load next level instead of showing panel
//             // SceneManager.LoadScene("NextLevelSceneName");
//         }
//     }
// }



using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public GameObject winPanel; // Assign your You Win panel here
    public bool pauseGame = true; // Freeze the game when level is finished

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Level Completed!");

            if (winPanel != null)
                winPanel.SetActive(true); // Show You Win panel

            if (pauseGame)
                Time.timeScale = 0f; // Pause game

            // Optional: load next level instead of showing panel
            // SceneManager.LoadScene("NextLevelSceneName");
        }
    }

    // Optional: Method for the button to reload current level
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
