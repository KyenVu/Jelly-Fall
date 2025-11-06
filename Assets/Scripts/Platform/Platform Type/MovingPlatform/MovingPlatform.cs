using UnityEngine;

public class MovingPlatform : BasePlatform
{
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

    private Vector3 startPos;
    private bool movingRight = true;
    private Transform player;
    private Vector3 lastPlatformPosition;

    protected override void Start()
    {
        base.Start();
        moveRange = Random.Range(1f, 2f);
        startPos = transform.position;
        lastPlatformPosition = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        SpecialAbility();

        if (player != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;

            player.position += platformMovement;
        }

        lastPlatformPosition = transform.position;
    }

    public override void SpecialAbility()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;
            if (transform.position.x >= startPos.x + moveRange)
                movingRight = false;
        }
        else
        {
            transform.position -= Vector3.right * horizontalSpeed * Time.deltaTime;
            if (transform.position.x <= startPos.x - moveRange)
                movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;
            lastPlatformPosition = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            movingRight = !movingRight;
            float nudgeAmount = 0.1f;
            transform.position += (movingRight ? Vector3.right : Vector3.left) * nudgeAmount;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
