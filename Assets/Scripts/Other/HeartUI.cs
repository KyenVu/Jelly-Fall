using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    private Animator animator;
    private Image image;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void PlayHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    public void PlayHealAnimation()
    {
        animator.SetTrigger("Heal");
    }

    public void SetVisible(bool isVisible)
    {
        image.enabled = isVisible;
    }
}
