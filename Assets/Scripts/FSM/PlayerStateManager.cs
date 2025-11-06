using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;

    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerMoveState MoveState = new PlayerMoveState();
    public PlayerHurtState HurtState = new PlayerHurtState();
    public PlayerDeathState DeathState = new PlayerDeathState();
    public PlayerWinState WinState = new PlayerWinState();

    public Rigidbody2D rb;
    [Header("Move Setting")]
    public float moveSpeed = 5f;

    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Health Setting")]
    public int HP = 3;
    public int maxHP = 5;

    [Header("Camera Setting")]
    public Camera cameraCheck;

    [Header("Particles")]
    public ParticleSystem landingEffect;

    [Header("Sound")]
    public AudioClip landAudio;
    public AudioClip hurtAudio;
    public AudioSource audioSource;

    public bool isDead = false;

    private void OnEnable()
    {
        Heart.collectHeart += HealthUpdate;
        PostGameState.playerWin += ToWinState;
    }
    private void OnDisable()
    {
        Heart.collectHeart -= HealthUpdate;
        PostGameState.playerWin -= ToWinState;
    }

    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
        audioSource.clip = landAudio;
        AudioSource vfx = GetComponent<AudioSource>();
        AudioManager.Instance.RegisterVFXSource(vfx);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        if (isDead) return;
        currentState = state;
        state.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            audioSource.Play();
            landingEffect.Play();
        }
    }
    
    private void HealthUpdate()
    {
        HP++;
        if(HP > maxHP)
        {
            HP = maxHP;
        }
    }
    private void ToWinState()
    {
        SwitchState(WinState);
    }

}