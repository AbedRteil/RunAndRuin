using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float baseSpeedBoost = 10f;
    public float lifetime = 3f;

    private float speed;
    private Vector2 direction;
    private float spawnTime;

    private void OnEnable()
    {
        // Runs every time this object is re-activated from the pool, not just once
        spawnTime = Time.time;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetSpeed(float playerCurrentSpeed)
    {
        speed = playerCurrentSpeed + baseSpeedBoost;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Time.time - spawnTime >= lifetime)
        {
            ReturnToPool();
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
       if (SoulManager.Instance != null)
{
    SoulManager.Instance.SpawnSoulPickup(collision.transform.position);
}

        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool("Enemy", collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }

        ReturnToPool();
    }
}

    private void ReturnToPool()
    {
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool("Projectile", gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}