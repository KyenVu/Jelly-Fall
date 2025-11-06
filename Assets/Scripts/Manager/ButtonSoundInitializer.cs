using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundInitializer : MonoBehaviour
{
    public AudioClip buttonClickSound;

    void Awake()
    {
        // Set global sound
        UIButtonSound.clickSound = buttonClickSound;

        // Create a dedicated AudioSource (or reuse one)
        GameObject soundObj = new GameObject("UIButtonSoundPlayer");
        AudioSource source = soundObj.AddComponent<AudioSource>();
        source.playOnAwake = false;
        DontDestroyOnLoad(soundObj);

        UIButtonSound.audioSource = source;

        // Add UIButtonSound to all buttons in the scene
        Button[] allButtons = FindObjectsOfType<Button>(true);
        foreach (var btn in allButtons)
        {
            if (btn.GetComponent<UIButtonSound>() == null)
            {
                btn.gameObject.AddComponent<UIButtonSound>();
            }
        }
    }
}
