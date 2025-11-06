using UnityEngine;
using static PlayerDeathState;

public class PlatformSpawner : MonoBehaviour
{
    public PlatformPool platformPool;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnRangeX = 5f;
    public float spawnPositionY = -3f;
    public float verticalGap = 2f;
    public Vector2 fixedSpawnPosition;

    [Header("Platform Chances")]
    public float movingPlatformChance = 0.3f;
    public float temporaryPlatformChance = 0.2f;
    public float shortPlatformChance = 0.25f;

    [Header("Item Spawn Settings")]
    [Range(0f, 1f)] public float coinSpawnChance = 0.3f;
    [Range(0f, 1f)] public float potionSpawnChance = 0.1f;
    public GameObject coinPrefab;
    public GameObject potionPrefab;

    private float timer;
    private float currentSpawnY;
    private bool isEndGame = false;
    private bool isPlayerHurt = false;

    private void OnEnable()
    {
        PlatformManager.increaseDifficulty += DecreaseSpawnTime;
        PlayerHurtState.playerHurt += SpawnNormalPlatform;
        PostGameState.playerLose += OnEndGame;
        PostGameState.playerWin += OnEndGame;
    }

    private void OnDisable()
    {
        PlatformManager.increaseDifficulty -= DecreaseSpawnTime;
        PlayerHurtState.playerHurt -= SpawnNormalPlatform;
        PostGameState.playerLose -= OnEndGame;
        PostGameState.playerWin -= OnEndGame;
    }

    void Start()
    {
        currentSpawnY = spawnPositionY - verticalGap;
    }

    private void OnEndGame()
    {
        isEndGame = true;
    }

    void Update()
    {
        if (isEndGame || isPlayerHurt) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPlatform();
            timer = 0f;
        }
    }

    void SpawnPlatform()
    {
        PlatformType platformType = GetRandomPlatformType();
        GameObject platform = platformPool.GetPlatform(platformType);

        if (platform != null)
        {
            float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector2 platformPos = new Vector2(spawnX, currentSpawnY);
            platform.transform.position = platformPos;

            TrySpawnItemOnPlatform(platform.transform);
        }
    }

    private void SpawnNormalPlatform()
    {
        if (isEndGame) return;

        isPlayerHurt = true;

        GameObject platform = platformPool.GetPlatform(PlatformType.Normal);
        if (platform != null)
        {
            platform.transform.position = fixedSpawnPosition;
        }

        timer = 0f;
        isPlayerHurt = false;
    }

    private void TrySpawnItemOnPlatform(Transform platform)
    {
        float itemRoll = Random.value;

        if (itemRoll < potionSpawnChance && potionPrefab != null)
        {
            GameObject potion = Instantiate(potionPrefab, platform.position + Vector3.up * 0.5f, Quaternion.identity);
            potion.transform.SetParent(platform);
        }
        else if (itemRoll < potionSpawnChance + coinSpawnChance && coinPrefab != null)
        {
            GameObject coin = Instantiate(coinPrefab, platform.position + Vector3.up * 0.5f, Quaternion.identity);
            coin.transform.SetParent(platform);
        }
    }

    PlatformType GetRandomPlatformType()
    {
        float randomValue = Random.value;
        if (randomValue < movingPlatformChance)
        {
            return PlatformType.Moving;
        }
        else if (randomValue < movingPlatformChance + shortPlatformChance)
        {
            return PlatformType.Short;
        }
        else if (randomValue < movingPlatformChance + temporaryPlatformChance + shortPlatformChance)
        {
            return PlatformType.Breakable;
        }

        return PlatformType.Normal;
    }

    private void DecreaseSpawnTime()
    {
        spawnInterval -= 0.2f;
        if (spawnInterval < 1f)
        {
            spawnInterval = 1f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 topLeft = new Vector3(-spawnRangeX, spawnPositionY, 0);
        Vector3 topRight = new Vector3(spawnRangeX, spawnPositionY, 0);
        Vector3 bottomLeft = new Vector3(-spawnRangeX, spawnPositionY - 10f, 0);
        Vector3 bottomRight = new Vector3(spawnRangeX, spawnPositionY - 10f, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomLeft, bottomRight);
    }
}
