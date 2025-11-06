using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseSettingPanel : MonoBehaviour
{
    public GameObject settingsPanel;
    public Animator animator;

    public float popDownDuration = 2f;

    private bool isClosing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement(settingsPanel))
            {
                StartCoroutine(CloseSettings());
            }
        }
    }

    IEnumerator CloseSettings()
    {
        isClosing = true;
        animator.SetTrigger("Pop Down");
        yield return new WaitForSeconds(popDownDuration);
        settingsPanel.SetActive(false);
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
