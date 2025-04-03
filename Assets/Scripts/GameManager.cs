using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public ObstacleSpawner obstacleSpawner;
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
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        obstacleSpawner.StartSpawner();
        player.GetComponent<PlayerController>().enabled = true;
        UIController.Instance.HideTitle();
    }

    public void GameOver()
    {
        player.SetActive(false);
        obstacleSpawner.StopSpawner();

        Invoke("RestartGame", 2f); // Restart the game after 2 seconds
    }

    public void UpdateScore()
    {
        score++;
        UIController.Instance.UpdateScore(score);
    }
}
