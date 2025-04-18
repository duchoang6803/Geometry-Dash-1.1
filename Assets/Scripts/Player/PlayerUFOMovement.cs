using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerUFOMovement : PlayerAgent
{
    [SerializeField] private float hightOfFlying;
    [SerializeField] private float amplificateXVelocity;
    [SerializeField] private float amplificateYVelocity;
    [SerializeField] private float ufoFlyingSpeed;
    [SerializeField] private float ufoRotateSpeed;

    private int timeClickToRotate;

    private RaycastHit2D hitGround;

    private Quaternion targetRotation;

    private float currentAngle;
    private float targetAngle;
    private float velocityRotate;
    private float smoothTime;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();

    }

    private void Update()
    {
        UFOMovement();
        UFORotate();
        CheckJumpPointBehavior();
        CheckObstacle(this.gameObject, 1.1f, playerData.whatIsObstacle);
    }

    private void UFOMovement()
    {
        JumpGravity = (int)CurrentGravity;
        if (JumpGravity == 1)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = (Vector2.right * 1.25f + Vector2.up * 1.1f) * speedValues[(int)CurrentSpeed];
            }
        }
        else
        {
            this.transform.localScale = new Vector3(1, -1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = (Vector2.right * 1.25f + Vector2.down * 1.1f) * speedValues[(int)CurrentSpeed];
            }
        }
        _rb.gravityScale = JumpGravity == 1 ? 10f : -10f;
        if (OnGround(this.gameObject, hitGround))
        {
            _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
        }
    }

    private void UFORotate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            targetAngle = JumpGravity == 1 ? -10f : 10f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            targetAngle = 0f;
        }

        float ufoRotate = Mathf.SmoothDamp(currentAngle, targetAngle, ref velocityRotate, smoothTime);
        this.transform.rotation = Quaternion.Euler(0, 0, ufoRotate);
    }

    public void CheckJumpPointBehavior()
    {
        if (IsNearGroundJumpPoint(this.gameObject) || (IsNearAirJumpPoint(this.gameObject) && Input.GetMouseButtonDown(0)))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, playerData.allObjectJumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.1f);
    }

}
