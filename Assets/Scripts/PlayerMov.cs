using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool gameStarted = false;
    private bool isDead = false;

    public float runSpeed = 5f;
    public float jumpForce = 10f;

    public float maxRunSpeed = 12f;
    public float speedRampRate = 0.03f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    // Attack sound
    public AudioSource attackSound;
    public AudioSource jumpSound;
    public AudioSource deathSound;

    // Death menu backup
    public GameObject deathMenuPanel;

    // Current running speed
    public float CurrentSpeed { get; private set; }

    private Animator animator;
    private Rigidbody2D rb;
    private Camera mainCam;

    private int groundContacts = 0;

    public float shootCooldown = 0.5f;
    private float nextShootTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        // Don't allow inputs if game hasn't started or player is dead
        if (!gameStarted || isDead)
        {
            if (!gameStarted && !isDead)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    gameStarted = true;

                    // Prevent instantly shooting from the same click
                    nextShootTime = Time.time + 0.1f;
                }
            }

            return;
        }

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && groundContacts > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }

        // SHOOT WITH MOUSE / PC
        if ((Input.GetMouseButtonDown(0) ||
             Input.GetKeyDown(KeyCode.LeftControl))
            && Time.time >= nextShootTime)
        {
            ShootMagic(Input.mousePosition);

            nextShootTime = Time.time + shootCooldown;
        }
    }

    void FixedUpdate()
    {
        // Only run forward if game is active and player isn't dead
        if (gameStarted && !isDead)
        {
            // Speed ramps up with distance traveled
            CurrentSpeed = Mathf.Min(
                runSpeed + (transform.position.x * speedRampRate),
                maxRunSpeed
            );

            rb.linearVelocity = new Vector2(
                CurrentSpeed,
                rb.linearVelocity.y
            );
        }
        else
        {
            CurrentSpeed = 0f;

            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground detection
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContacts++;
        }

        // Obstacle / enemy collision
        if (collision.gameObject.CompareTag("Obstacle") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            HandleObstacleHit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundContacts--;
            groundContacts = Mathf.Max(groundContacts, 0);
        }
    }

    // Public method for external death signals
    public void HandleObstacleHit()
    {
        if (!isDead)
        {
            StartCoroutine(PlayDeathSequence());
        }
    }

    // Death sequence
    IEnumerator PlayDeathSequence()
    {
        isDead = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }

        // Stop movement immediately
        rb.linearVelocity = Vector2.zero;

        // Death animation
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }

        // Death sound
        if (deathSound != null)
        {
            deathSound.Play();
        }

        // Wait for death animation
        yield return new WaitForSeconds(1.5f);

        // Get final score
        int finalScore = ScoreManager.Instance != null
            ? ScoreManager.Instance.GetCurrentScore()
            : 0;

        // Show death menu
        if (DeathMenuManager.Instance != null)
        {
            DeathMenuManager.Instance.ShowDeathMenu(finalScore);
        }
        else if (deathMenuPanel != null)
        {
            deathMenuPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning(
                "Could not find DeathMenuManager or deathMenuPanel reference!"
            );
        }
    }

    // =========================================================
    // SHOOTING
    // =========================================================

    // This now accepts a SCREEN position.
    // PC sends mouse position.
    // Mobile sends touch position.
    void ShootMagic(Vector2 screenPosition)
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Play attack sound
        if (attackSound != null)
        {
            attackSound.Play();
        }

        // Check required references
        if (mainCam == null ||
            firePoint == null ||
            projectilePrefab == null)
        {
            Debug.LogWarning(
                "Missing Camera, FirePoint, or ProjectilePrefab reference!"
            );

            return;
        }

        // Convert screen position to world position
        Vector3 targetPosition =
            mainCam.ScreenToWorldPoint(
                new Vector3(
                    screenPosition.x,
                    screenPosition.y,
                    Mathf.Abs(
                        mainCam.transform.position.z -
                        firePoint.position.z
                    )
                )
            );

        // Calculate direction from FirePoint to tap/mouse
        Vector2 direction =
            targetPosition - firePoint.position;

        direction.Normalize();

        GameObject magicBall;

        // Use object pool if available
        if (ObjectPool.Instance != null)
        {
            magicBall = ObjectPool.Instance.SpawnFromPool(
                "Projectile",
                firePoint.position,
                Quaternion.identity
            );
        }
        else
        {
            magicBall = Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity
            );
        }

        // Configure projectile
        Projectile projectile =
            magicBall.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.SetDirection(direction);
            projectile.SetSpeed(CurrentSpeed);
        }
    }

    // =========================================================
    // MOBILE CONTROLS
    // =========================================================

    public void MobileJump()
    {
        if (!gameStarted || isDead)
            return;

        if (groundContacts > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }
    }

    public void MobileShoot(Vector2 touchPosition)
    {
        if (!gameStarted || isDead)
            return;

        if (Time.time >= nextShootTime)
        {
            ShootMagic(touchPosition);

            nextShootTime = Time.time + shootCooldown;
        }
    }
}