using System.Collections.Generic;
using UnityEngine;

public class HeartUIManager : MonoBehaviour
{
    public GameObject heartUIPrefab;
    public Transform heartContainer;

    private List<HeartUI> hearts = new List<HeartUI>();
    private int currentHealth = 3;
    private int maxHealth = 5;

    private void OnEnable()
    {
        Heart.collectHeart += Heal;
        PlayerHurtState.playerHurt += Hurt;
    }

    private void OnDisable()
    {
        Heart.collectHeart -= Heal;
        PlayerHurtState.playerHurt -= Hurt;
    }

    private void Start()
    {
        InitializeHearts(currentHealth);
    }

    void InitializeHearts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddHeartUI();
        }
    }

    void AddHeartUI()
    {
        if (hearts.Count >= maxHealth) return;

        GameObject heartGO = Instantiate(heartUIPrefab, heartContainer);
        HeartUI heart = heartGO.GetComponent<HeartUI>();
        hearts.Add(heart);
    }

    void Hurt()
    {
        if (currentHealth <= 0) return;

        currentHealth--; 
        if (currentHealth < hearts.Count)
        {
            hearts[currentHealth].PlayHurtAnimation();
        }
    }


    void Heal()
    {
        if (currentHealth >= maxHealth) return;

        if (currentHealth == hearts.Count)
        {
            AddHeartUI();
        }

        hearts[currentHealth].PlayHealAnimation();
        currentHealth++;
    }
}
