using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public int score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UIController.Instance.UpdateScore(score);
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {

    }

    public void GameOver()
    {
        player.SetActive(false);

        Invoke("RestartGame", 2f); // Restart the game after 2 seconds
    }

    public void UpdateScore()
    {
        score++;
        UIController.Instance.UpdateScore(score);
    }
}
