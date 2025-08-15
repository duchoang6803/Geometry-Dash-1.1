using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAgentFlyingMovement : PlayerAgent, IVelocityPlayerPauseGame
{
    [SerializeField]
    private float agentFlyingSpeed;
    [SerializeField]
    private float agentFlyingUpSpeed;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        FlyingAgentMovement();
        FlyingUpMovement();
        CheckJumpPointBehavior();
        CheckObstacle(this.gameObject, 1.2f, playerData.whatIsObstacle);
    }

    public void OnVelocityPlayer()
    {
        isPlayerStop = true;
    }
    private void FlyingAgentMovement()
    {
        if (isPlayerStop)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0f;
        }
        else
        {
            _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
        }
    }


    private void FlyingUpMovement()
    {
        if ((int)CurrentGravity == 1 || (int)CurrentGravity == -1)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, _rb.velocity.y * 1.1f);
        }
        if ((int)CurrentGravity == 1)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = (Vector2.right + new Vector2(0, 1.15f)) * agentFlyingUpSpeed;
            }

        }
        else
        {
            this.transform.localScale = new Vector3(1, -1, 1);
            if (Input.GetMouseButtonDown(0))
            {
                _rb.velocity = (Vector2.right + new Vector2(0, -1.15f)) * agentFlyingUpSpeed;
            }

        }
        _rb.gravityScale = (int)CurrentGravity == 1 ? 5f : -5f;
    }


    private void CheckJumpPointBehavior()
    {
        if (IsNearGroundJumpPoint() || (IsNearAirJumpPoint() && Input.GetMouseButtonDown(0)))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, playerData.allObjectJumpForce);
        }
    }

    private bool IsNearGroundJumpPoint()
    {
        var hit = Physics2D.CircleCast(this.transform.position, playerData.allObjectJumpPointDistance, Vector2.zero, 0, playerData.whatIsGroundJumpPoint);
        return hit.collider != null;
    }

    private bool IsNearAirJumpPoint()
    {
        var hit = Physics2D.CircleCast(this.transform.position, playerData.allObjectJumpPointDistance, Vector2.zero, 0, playerData.whatIsAirJumpPoint);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.2f);
    }


}
