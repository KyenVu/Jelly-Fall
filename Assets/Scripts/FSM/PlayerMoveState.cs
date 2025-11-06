using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private Camera _camera;

    public override void EnterState(PlayerStateManager player)
    {
        _camera = player.cameraCheck;
        player.animator.SetBool("isWalk", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        float moveInput = GetInputDirection();
        Debug.Log(moveInput);

        if (moveInput == 0)
        {
            player.SwitchState(player.IdleState);
        }
        else if (IsOutOfCameraView(player))
        {
            player.SwitchState(player.HurtState);
        }
        else
        {
            player.spriteRenderer.flipX = moveInput < 0;
            player.rb.velocity = new Vector2(moveInput * player.moveSpeed, player.rb.velocity.y);
        }
    }

    private bool IsOutOfCameraView(PlayerStateManager player)
    {
        if (_camera == null) return false;
        Vector3 viewportPos = _camera.WorldToViewportPoint(player.transform.position);
        return viewportPos.y > 1 || viewportPos.y < 0;
    }

    private float GetInputDirection()
    {
        //Keyboard input(for PC)
            float input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
            return input;

        float screenMiddle = Screen.width / 2f;

        // 1. Prefer touch (works on real mobile devices and some mobile browsers)
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (touchPos.x < screenMiddle)
            {
                Debug.Log("Touch: Move Left");
                return -1f;
            }
            else
            {
                Debug.Log("Touch: Move Right");
                return 1f;
            }
        }

        // 2. Fallback: Mouse (works on desktop & in WebGL, also on mobile browsers as a fallback)
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x;
            if (mouseX < screenMiddle)
            {
                Debug.Log("Mouse: Move Left");
                return -1f;
            }
            else
            {
                Debug.Log("Mouse: Move Right");
                return 1f;
            }
        }

        return 0f;
    }

}
