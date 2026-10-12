using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float extraDespawnMargin = 2f; // extra buffer past the screen edge, just to be safe
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (mainCam != null)
        {
            float leftEdgeOfScreen = mainCam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

            if (transform.position.x < leftEdgeOfScreen - extraDespawnMargin)
            {
                ReturnToPool();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT AN OBSTACLE!");

            // Safely signal the player script that a crash happened
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.HandleObstacleHit(); 
            }
        }
    }

    private void ReturnToPool()
    {
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool("Obstacle", gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
