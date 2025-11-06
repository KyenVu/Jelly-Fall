using System.Collections;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    public delegate void PlayerHurt();
    public static event PlayerHurt playerHurt;


    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("Hurt", true);
        player.audioSource.clip = player.hurtAudio;
        player.audioSource.Play();
        player.rb.velocity = Vector2.zero;
        player.transform.position = new Vector3(0,2,0);
        player.rb.bodyType = RigidbodyType2D.Static;
        player.HP--;
        OnHurt();

        player.StartCoroutine(TransitBack(player));
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(player.HP <= 0)
        {
            Debug.Log("You lose");
            player.SwitchState(player.DeathState);
        }
    }

    private void OnHurt()
    {
        playerHurt?.Invoke();
    }

    private IEnumerator TransitBack(PlayerStateManager player)
    {
        yield return new WaitForSeconds(1f); 
        player.animator.SetBool("Hurt", false);
        player.SwitchState(player.IdleState);
        player.audioSource.clip = player.landAudio;
    }
}
