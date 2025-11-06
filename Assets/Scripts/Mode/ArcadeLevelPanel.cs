using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;

public class ArcadeLevelPanel : MonoBehaviour
{
    public Button[] levelButtons;
    public GameObject ArcadePanel;
    public Animator animator;
    public float popDownDuration = 1f;

    private bool isClosing = false;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("ArcadeMode_UnlockedLevel", 1);

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

    void SelectLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("ArcadeMode_SelectedLevel", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ArcadeMode");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClosing)
        {
            if (!IsPointerOverUIElement(ArcadePanel))
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
        ArcadePanel.SetActive(false);
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
