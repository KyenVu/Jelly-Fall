using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LeaderboardUI : MonoBehaviour
{
    public TMP_Text score1, score2, score3, score4, score5;
    public GameObject LeaderboardPanel;
    public Animator animator;

    public float popDownDuration = 2f; 
    private bool isClosing = false;
    private void OnEnable()
    {
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            float score = PlayerPrefs.GetFloat("HighScore" + i, 0f);
            GetScoreText(i).text = $"{i + 1}. {score}";
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement(LeaderboardPanel))
            {
                StartCoroutine(PopDownAndDeactivate());
            }
        }
    }

    IEnumerator PopDownAndDeactivate()
    {
        isClosing = true;
        animator.SetTrigger("Pop Down");
        yield return new WaitForSeconds(popDownDuration);
        LeaderboardPanel.SetActive(false);
        isClosing = false;
    }

    bool IsPointerOverUIElement(GameObject panel)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == panel || result.gameObject.transform.IsChildOf(panel.transform))
                return true;
        }

        return false;
    }
    TMP_Text GetScoreText(int index)
    {
        switch (index)
        {
            case 0: return score1;
            case 1: return score2;
            case 2: return score3;
            case 3: return score4;
            case 4: return score5;
            default: return null;
        }
    }
}
