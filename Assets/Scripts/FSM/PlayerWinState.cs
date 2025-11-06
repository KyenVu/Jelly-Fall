using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.rb.velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 2, 0);
        player.rb.bodyType = RigidbodyType2D.Static;
    }

    public override void UpdateState(PlayerStateManager player)
    {
       
    }

    
}
