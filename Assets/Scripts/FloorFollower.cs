using UnityEngine;

public class FloorFollower : MonoBehaviour
{
    public Transform player;
    public float groundY = -4f; // Tune this value up or down so the collider sits right under your player's feet

    void Start()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null) player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Locks onto player's horizontal position permanently
            transform.position = new Vector3(player.position.x, groundY, 0f);
        }
    }
}
