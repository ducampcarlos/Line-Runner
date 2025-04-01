using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float playerYPosition;

    private void Start()
    {
        playerYPosition = transform.position.y;
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
}
