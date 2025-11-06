using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostGameState : MonoBehaviour
{
    public static PostGameState instance {  get; private set; }
    public GameObject EndPanel;

    public delegate void OnPostGame();
    public static event OnPostGame playerLose;
    public static event OnPostGame playerWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        PlayerDeathState.playerDeath += Lose;
        ArcadeGameManager.doneLevelArcade += Win;
        TimeAttackGameModeManager.doneLevelTimeAttack += Win;
    }

    private void OnDisable()
    {
        PlayerDeathState.playerDeath -= Lose;
        ArcadeGameManager.doneLevelArcade -= Win;
        TimeAttackGameModeManager.doneLevelTimeAttack -= Win;
    }

    void Start()
    {
        EndPanel.SetActive(false);
    }
    // Win scenario
    private void Win()
    {
        playerWin?.Invoke();
        EndPanel.SetActive(true);
        Debug.Log("Win");
    }
    // Lose scenario
    private void Lose()
    {
        playerLose?.Invoke();
        EndPanel.SetActive(true);
    }

}
