using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerClickHandler
{
    public static AudioClip clickSound;
    public static AudioSource audioSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.volume = AudioManager.Instance.vfxVolume * AudioManager.Instance.masterVolume;
            audioSource.PlayOneShot(clickSound);

        }
    }
}
