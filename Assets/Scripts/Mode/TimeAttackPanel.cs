using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeAttackPanel : MonoBehaviour
{
    public Button[] levelButtons;

    public float popDownDuration = 1f;

    public Animator animator;

    public GameObject TAPanel;

    private bool isClosing = false;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("TimeAttackMode_UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            Button btn = levelButtons[i];
            btn.interactable = levelIndex <= unlockedLevel;

            int capturedIndex = levelIndex;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => SelectLevel(capturedIndex));
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClosing)
        {
            if (!IsPointerOverUIElement(TAPanel))
            {
                StartCoroutine(PopDownAndDeactivate());
            }
        }
    }

    void SelectLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("TimeAttackMode_SelectedLevel", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("TimeAttackMode");
    }
    IEnumerator PopDownAndDeactivate()
    {
        isClosing = true;
        animator.SetTrigger("Pop Down");
        yield return new WaitForSeconds(popDownDuration);
        TAPanel.SetActive(false);
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
}