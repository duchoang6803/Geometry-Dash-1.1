using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : PlayerAgent,IVelocityPlayerPauseGame,IVelocityPlayerContinueGame
{
    [SerializeField]
    private float jumpPointDistance;
    [SerializeField]
    private float rotateVelocity;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private bool isOnSlope;

    private RaycastHit2D hitGrounded;


    public float angleOnSlope;

    private bool isJumping;


    private void Start()
    {
        JumpGravity = (int)CurrentGravity;
        _rb = GetComponentInParent<Rigidbody2D>();
        
    }


    private void Update()
    {
        PlayerGravity();
        PlayerMovement();
        Jump();
        IsOnSlope();
        JumpPointBehavior();
        JumpPointGravityBehavior();
        CheckParticleSystem();
        ScaleWhenTouchThePortal();
        CheckObstacle(this.gameObject, 0.85f, playerData.whatIsObstacle);
    }

    private void PlayerGravity()
    {
        _rb.gravityScale = (JumpGravity == 1) ? 12f : -12f;

    }

    public void OnVelocityPlayer()
    {
        isPlayerStop = true;
    }

    public void OnVelocityPlayerContinueGame()
    {
        isPlayerStop = false;
    }


    private void PlayerMovement()
    {
        if (isPlayerStop == true)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0f;
            this.transform.rotation = Quaternion.identity;
        }
        else
        {
            PlayerGravity();
            _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (OnGround(this.gameObject, hitGrounded))
        {
            Vector3 Rotation = this.transform.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            this.transform.rotation = Quaternion.Euler(Rotation);
            if (Input.GetMouseButtonDown(0))
            {
                isJumping = true;
                _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], 1.3f * playerData.allObjectJumpForce * JumpGravity);
            }
            isJumping = false;
        }
        else
        {
            this.transform.Rotate((JumpGravity == 1) ? Vector3.back * 3.5f : Vector3.forward * 3.5f);
        }
    }

    private void ScaleWhenTouchThePortal()
    {
        Vector3 scaleWhenTouchPortal = speedValues[(int)CurrentSpeed] >= 30f ? this.transform.localScale = new Vector3(0.8f, 0.8f, 0) : this.transform.localScale = Vector3.one;
    }


    private void CheckParticleSystem()
    {
        if (OnGround(this.gameObject, hitGrounded))
        {
            _particleSystem.gameObject.SetActive(true);
        }
        else
        {
            _particleSystem.gameObject.SetActive(false);
        }
    }

    private void JumpPointBehavior()
    {
        if (IsNearAirJumpPoint(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x * 1.5f, playerData.allObjectJumpForce * 1.8f);
            var afterImage = PlayerAfterImagePool.Instance.GetFormPool();
            afterImage.SetUp(spriteRenderer.sprite, spriteRenderer.transform.position, spriteRenderer.transform.rotation);
        }
        else if (IsNearGroundJumpPoint(this.gameObject))
        {
            _rb.velocity = new Vector2(_rb.velocity.x * 1.5f, playerData.allObjectJumpForce * 1.2f);
            var afterImage = PlayerAfterImagePool.Instance.GetFormPool();
            afterImage.SetUp(spriteRenderer.sprite, spriteRenderer.transform.position, spriteRenderer.transform.rotation);
        }
    }

    private void JumpPointGravityBehavior()
    {
        if (IsNearAirJumpGravity(this.gameObject) && Input.GetMouseButtonDown(0))
        {
            JumpGravity *= -1;
        }
    }


    private void IsOnSlope()
    {
        RaycastHit2D hitGroundForCheckSlope = Physics2D.Raycast(this.transform.position, (Vector2.down).normalized, playerData.checkGroundDistance, playerData.whatIsGround);
        Debug.DrawRay(this.transform.position, (Vector2.down).normalized, Color.red);
        Debug.DrawRay(hitGroundForCheckSlope.point, hitGroundForCheckSlope.normal, Color.yellow);
        if (Math.Abs(hitGroundForCheckSlope.normal.x) <= 0.5f && Math.Abs(hitGroundForCheckSlope.normal.y) == 1)
        {
            isOnSlope = false;
            angleOnSlope = 0f;
        }

        else if (!isJumping && Math.Abs(hitGroundForCheckSlope.normal.x) >= 0.5f && Math.Abs(hitGroundForCheckSlope.normal.y) != 1)
        {
            isOnSlope = true;
            angleOnSlope = Mathf.Atan2(hitGroundForCheckSlope.normal.y, hitGroundForCheckSlope.normal.x) * Mathf.Rad2Deg;
        }
        if (angleOnSlope != 0 && isOnSlope)
        {
            this.transform.up = hitGroundForCheckSlope.normal;
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector2.right * 1.5f);
        Gizmos.DrawRay(this.transform.position + Vector3.up, Vector2.down * playerData.checkGroundDistance);
        Gizmos.DrawWireSphere(this.transform.position, 0.85f);
    }

}
