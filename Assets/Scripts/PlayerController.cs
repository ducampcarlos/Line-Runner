using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float playerYPosition;
    [SerializeField] int lives = 3;
    public int currentLives;
    public List<PlayerStateConfig> availableStates = new List<PlayerStateConfig>();
    private Dictionary<PlayerStates, PlayerState> stateDictionary;
    private SpriteRenderer spriteRenderer;
    private PlayerState playerState;

    private void Awake()
    {
        playerYPosition = transform.position.y;
        currentLives = lives;
        UIController.Instance.UpdateLives(currentLives);
        spriteRenderer = GetComponent<SpriteRenderer>();

        stateDictionary = new Dictionary<PlayerStates, PlayerState>();
        foreach (var config in availableStates)
        {
            if (!stateDictionary.ContainsKey(config.state))
            {
                stateDictionary.Add(config.state, config.data);
            }
        }

    }

    private void Start()
    {
        SetPlayerState(PlayerStates.White);
    }

    public void OnFlipButtonPressed()
    {
        playerYPosition = -playerYPosition;
        var newPos = new Vector3(transform.position.x, playerYPosition, transform.position.z);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            if (obstacle.GetObstacleType() == playerState.playerState)
            {

            }
            else
            {

                HitPlayer();
            }
        }
    }


    private void HitPlayer()
    {
        GameManager.Instance.Shake();
        currentLives--;
        UIController.Instance.UpdateLives(currentLives);
        if (currentLives <= 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            StartCoroutine(InvincibleCoroutine());
        }
    }

    IEnumerator InvincibleCoroutine()
    {
        float duration = 0.5f; // Duration of invincibility
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Change the player's opacity between 0.5 and 1
            float alpha = Mathf.PingPong(elapsedTime / duration, 1);
            SetPlayerOpacity(alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the player's opacity to 1 after invincibility ends
        SetPlayerOpacity(1);
    }

    private void SetPlayerOpacity(float alpha)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = alpha;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetPlayerState(PlayerState ps)
    {
        playerState = ps;
        spriteRenderer.color = playerState.playerColor;

        if (playerState.playerSprite != null)
        {
            spriteRenderer.sprite = playerState.playerSprite;
        }
    }

    public void SetPlayerState(PlayerStates state)
    {
        if (stateDictionary != null && stateDictionary.ContainsKey(state))
        {
            SetPlayerState(stateDictionary[state]);
        }
    }

    public void SetPlayerStateByIndex(int index)
    {
        if (index >= 0 && index < availableStates.Count)
        {
            SetPlayerState(availableStates[index].data);
        }
    }
}

[System.Serializable]
public class PlayerState
{
    public PlayerStates playerState;
    public Color playerColor;
    public Sprite playerSprite; // Nuevo
}

public enum PlayerStates
{
    White,
    Blue,
    Yellow,
    Red // This one it's not accesible for the player
}