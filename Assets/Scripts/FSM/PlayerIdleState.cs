using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private Camera _camera;

    public override void EnterState(PlayerStateManager player)
    {
        _camera = player.cameraCheck;
        player.rb.bodyType = RigidbodyType2D.Dynamic;
        player.rb.velocity = Vector2.zero;
        player.animator.SetBool("isWalk", false);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        float moveInput = GetInputDirection();
        if (moveInput != 0)
        {
            player.SwitchState(player.MoveState);
        }
        else if (IsOutOfCameraView(player))
        {
            player.SwitchState(player.HurtState);
        }
    }

    private float GetInputDirection()
    {
        float screenMiddle = Screen.width / 2f;

        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (touchPos.x < screenMiddle)
                return -1f;
            else
                return 1f;
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.mousePosition.x;
            if (mouseX < screenMiddle)
                return -1f;
            else
                return 1f;
        }

        // Keyboard fallback (optional, if you want PC input here too)
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
            return input;

        return 0f;
    }


    private bool IsOutOfCameraView(PlayerStateManager player)
    {
        if (_camera == null) return false;
        Vector3 viewportPos = _camera.WorldToViewportPoint(player.transform.position);
        return viewportPos.y > 1 || viewportPos.y < 0;
    }
}
