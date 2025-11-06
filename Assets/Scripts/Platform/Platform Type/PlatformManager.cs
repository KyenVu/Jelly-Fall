using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

    public delegate void IncreaseDifficulty();
    public static event IncreaseDifficulty increaseDifficulty;

    public float platformSpeed = 3f; 

    private const float UPDATE_INTERVALS = 10f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(TriggerDifficultyIncrease), UPDATE_INTERVALS, UPDATE_INTERVALS);
    }

    void TriggerDifficultyIncrease()
    {
        platformSpeed += 0.1f;
        increaseDifficulty?.Invoke();
    }
}
