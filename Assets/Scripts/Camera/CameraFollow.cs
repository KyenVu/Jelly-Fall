using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector2 offset;
    public float speed = 10f;

    [Header("Camera Bounds")]
    public Bounds cameraBounds;

    void Update()
    {

        //Vector3 desiredPos = new Vector3(
        //    player.position.x - offset.x,
        //    player.position.y - offset.y,
        //    transform.position.z
        //);
        Vector3 targetPosition = player.position;

        // Clamp the position within the camera bounds
            targetPosition.x = Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y);

        // Smoothly move the camera towards the player
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        smoothedPos.z = transform.position.z;
        transform.position = smoothedPos;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cameraBounds.center, cameraBounds.size);
    }
}
