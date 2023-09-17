using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Pauses the game
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }
    /// <summary>
    /// Resumes the game
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1;
    }
    /// <summary>
    /// Restarts the active Scene
    /// </summary>
    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
