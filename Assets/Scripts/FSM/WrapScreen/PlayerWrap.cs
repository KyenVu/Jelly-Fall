using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    // Variables to store the horizontal screen boundaries
    private float minX, maxX;

    void Start()
    {
        // Get the main camera
        Camera cam = Camera.main;

        // Calculate the screen boundaries in world space for the x-direction
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        minX = cam.transform.position.x - width / 2;
        maxX = cam.transform.position.x + width / 2;
    }

    void Update()
    {
        // Get the player's current position
        Vector3 pos = transform.position;

        // Wrap horizontally only
        if (pos.x > maxX)
        {
            pos.x = minX;
        }
        else if (pos.x < minX)
        {
            pos.x = maxX;
        }

        // Apply the new position, keeping y and z unchanged
        transform.position = pos;
    }
}