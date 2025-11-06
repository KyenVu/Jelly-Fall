using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public delegate void CollectHealth();
    public static event CollectHealth collectHeart;

    public ParticleSystem heartEffect;
    public AudioClip heartAudio;
    public AudioSource audioSource;

    private SpriteRenderer heartSprite;
    private bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        heartSprite = GetComponent<SpriteRenderer>();
        heartSprite.enabled = true;
        audioSource.clip = heartAudio;
        AudioSource vfx = GetComponent<AudioSource>();
        AudioManager.Instance.RegisterVFXSource(vfx);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")&& !isCollected)
        {
            isCollected = true;
            collectHeart?.Invoke();
            heartEffect.Play();
            audioSource.Play();
            heartSprite.enabled = false;
            Destroy(heartSprite,2);
        }
    }
}
