using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipZigZagMovement : PlayerAgent, IVelocityPlayerPauseGame
{
    [SerializeField]
    private float forceGoingUp;
    [SerializeField]
    private float forceGoingDown;
    [SerializeField]
    private SpriteRenderer spriteRendererForShipZigZag;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }
    private void Update()
    {
        ShipZigZagMovementBehavior();
        ShipZigZagRotate();
        CheckObstacle(this.gameObject, 0.7f, playerData.whatIsGround); // Ground is the Obstacle for this gameObject
    }

    public void OnVelocityPlayer()
    {
        isPlayerStop = true;

    }

    private void ShipZigZagMovementBehavior()
    {
        if (isPlayerStop)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0f;
        }
        else
        {
            _rb.gravityScale = 0;
            _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], speedValues[(int)CurrentSpeed] * (Input.GetMouseButton(0) ? 1 : -1));
            var afterImage = PlayerAfterImagePool.Instance.GetFormPool();
            afterImage.SetUp(spriteRendererForShipZigZag.sprite, spriteRendererForShipZigZag.transform.position, spriteRendererForShipZigZag.transform.rotation);
        }
    }


    private void ShipZigZagRotate()
    {
        this.transform.rotation = Input.GetMouseButton(0) ? Quaternion.Euler(0, 0, 45) : Quaternion.Euler(0, 0, -45);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.7f);
    }


}
