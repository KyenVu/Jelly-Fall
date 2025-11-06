using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ModeUI : MonoBehaviour
{
    public Button ArcadeModeButton;
    public Button SurvivalModeButton;
    public Button TimeAttackModeButton;

    public GameObject ArcadeLevelPanel;
    public GameObject TimeAttackLevelPanel;
    public GameObject ModePanel;


    public Animator animator;
    public float popDownDuration = 2f;

    private bool isClosing = false;

    void Start()
    {
        ArcadeModeButton.onClick.AddListener(OpenArcadePanel);
        SurvivalModeButton.onClick.AddListener(() => LoadSurvivalMode());
        TimeAttackModeButton.onClick.AddListener(OpenTimeAttackPanel);
        ArcadeLevelPanel.SetActive(false); 
        TimeAttackLevelPanel.SetActive(false);
    }

    private void OpenArcadePanel()
    {
        ArcadeLevelPanel.SetActive(true);
        StartCoroutine(PopDownAndDeactivate());
    }
    private void OpenTimeAttackPanel()
    {
        TimeAttackLevelPanel.SetActive(true);
        StartCoroutine(PopDownAndDeactivate());
    }


    void LoadSurvivalMode()
    {
        SceneManager.LoadScene("SurvivalMode");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isClosing)
        {
            if (!IsPointerOverUIElement(ModePanel))
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
        ModePanel.SetActive(false);
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
