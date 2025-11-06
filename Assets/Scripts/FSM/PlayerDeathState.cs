using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState

{
    public delegate void PlayerDeath();
    public static event PlayerDeath playerDeath;
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("Hurt", true);
        player.rb.velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 2, 0);
        player.rb.bodyType = RigidbodyType2D.Static;
        playerDeath?.Invoke();
        player.isDead = true;
    }

    public override void UpdateState(PlayerStateManager player)
    {
       
    }

}
