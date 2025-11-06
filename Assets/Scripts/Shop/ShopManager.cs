using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel; 

    public Animator animator;

    public float popDownDuration = 2f;

    private bool isClosing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement(shopPanel))
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
        shopPanel.SetActive(false);
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
