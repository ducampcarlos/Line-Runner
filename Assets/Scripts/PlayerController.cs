using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float playerYPosition;
    [SerializeField] int lives = 3;
    public int currentLives;

    private void Awake()
    {
        playerYPosition = transform.position.y;
        currentLives = lives;
        UIController.Instance.UpdateLives(currentLives);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        // Detects if the screen was touched, and inverts player position.
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            playerYPosition = -playerYPosition;
            var newPos = new Vector3(transform.position.x, playerYPosition, transform.position.z);
            transform.position = newPos;
        }
#elif UNITY_STANDALONE || UNITY_EDITOR
        // Detects if the left mouse button was pressed, and inverts player position.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            playerYPosition = -playerYPosition;
            var newPos = new Vector3(transform.position.x, playerYPosition, transform.position.z);
            transform.position = newPos;
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            HitPlayer();
        }
    }

    private void HitPlayer()
    {
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
}
