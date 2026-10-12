using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControls : MonoBehaviour
{
    public PlayerMovement player;

    [Header("Swipe Settings")]
    public float swipeThreshold = 80f;

    private Vector2 touchStartPosition;
    private bool touching = false;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        // Finger touches screen
        if (touch.press.wasPressedThisFrame)
        {
            touchStartPosition = touch.position.ReadValue();
            touching = true;
        }

        // Finger released
        if (touch.press.wasReleasedThisFrame && touching)
        {
            Vector2 touchEndPosition = touch.position.ReadValue();
            Vector2 movement = touchEndPosition - touchStartPosition;

            touching = false;

            // First touch starts the game
            if (!player.gameStarted)
            {
                player.gameStarted = true;
                return;
            }

            // Swipe UP = Jump
            if (movement.y >= swipeThreshold &&
                Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                player.MobileJump();
            }

            // Tap = Shoot
            else if (movement.magnitude < swipeThreshold)
            {
                // Pass the actual finger position to the player
                player.MobileShoot(touchEndPosition);
            }
        }
    }
}