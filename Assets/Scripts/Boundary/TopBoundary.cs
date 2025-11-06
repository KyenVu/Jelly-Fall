using UnityEngine;

public class TopBoundary : MonoBehaviour
{
    private PlatformPool pool;


    void Start()
    {
        pool = FindObjectOfType<PlatformPool>(); // Find the PlatformPool instance
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the correct tag
        if (collision.CompareTag("Platform"))
        {
            pool.ReturnPlatform(collision.gameObject); // Return platform to the pool
        }
    }
}
