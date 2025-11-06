using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    protected PlatformPool pool;
    private Camera mainCamera;

    private void OnEnable()
    {
        PlayerHurtState.playerHurt += ClearPlatform;
        PostGameState.playerLose += ClearPlatform;
        PostGameState.playerWin += ClearPlatform;

    }
    private void OnDisable()
    {
        PlayerHurtState.playerHurt -= ClearPlatform;
        PostGameState.playerLose -= ClearPlatform;
        PostGameState.playerWin -= ClearPlatform;

    }
    protected virtual void Start()
    {
        pool = FindObjectOfType<PlatformPool>();
        mainCamera = Camera.main;
    }

    protected virtual void Update()
    {
        transform.position += PlatformManager.Instance.platformSpeed * Time.deltaTime * Vector3.up;

        if (IsOutOfCameraView())
        {
            ClearChildren();
            pool.ReturnPlatform(gameObject);
        }
    }

    private bool IsOutOfCameraView()
    {
        if (mainCamera == null) return false;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPos.y > 1;
    }
    protected void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            Destroy(child.gameObject); 
        }
    }

    private void ClearPlatform()
    {
        pool.ReturnPlatform(gameObject);
    }
    public virtual void SpecialAbility() { }
}
