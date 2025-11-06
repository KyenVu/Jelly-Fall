using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CollectCoin(int amount);
    public static event CollectCoin collectCoin;

    public ParticleSystem collectEffect;
    public AudioClip coinCollectSound;
    public AudioSource audioSource;
    private SpriteRenderer coinSprite;
    private bool isCollected = false; 

    void Start()
    {
        coinSprite = GetComponent<SpriteRenderer>();
        coinSprite.enabled = true;
        audioSource.clip = coinCollectSound;
        AudioSource vfx = GetComponent<AudioSource>();
        AudioManager.Instance.RegisterVFXSource(vfx);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true; 
            collectCoin?.Invoke(1);
            collectEffect.Play();
            audioSource.Play();
            coinSprite.enabled = false;
            Destroy(gameObject, 2);
        }
    }
}