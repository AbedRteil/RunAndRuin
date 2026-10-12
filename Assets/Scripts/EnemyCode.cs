using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float extraDespawnMargin = 2f;
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
            Debug.Log("PLAYER HIT BY ENEMY!");

            // Safely hand over the death sequence sequence to the Player script
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
            ObjectPool.Instance.ReturnToPool("Enemy", gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
