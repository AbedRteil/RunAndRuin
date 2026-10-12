using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public Transform cameraTransform;
    [Tooltip("0 = moves perfectly with camera, 1 = stays still in the world, 0.5 = moving at half speed")]
    public float parallaxEffect = 0.5f;

    private float startPosX;
    private float cameraStartPosX;

    void Start()
    {
        // 1. Record the exact world X coordinate where this mountain was spawned
        startPosX = transform.position.x;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        // 2. Record where the camera was located at the moment of spawn
        cameraStartPosX = cameraTransform.position.x;
    }

    void LateUpdate()
    {
        // 3. Calculate how far the camera has traveled SINCE this specific prop spawned
        float cameraDeltaX = cameraTransform.position.x - cameraStartPosX;

        // 4. Multiply that relative distance by your parallax fraction
        float distanceX = cameraDeltaX * parallaxEffect;

        // 5. Shift the mountain smoothly based on its unique starting point
        transform.position = new Vector3(
            startPosX + distanceX,
            transform.position.y,
            transform.position.z
        );
    }
}
