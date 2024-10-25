using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerAgent, ITransformOnPortalTouch
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float checkGroundDistance;
    [SerializeField]
    private float jumpPointDistance;
    [SerializeField]
    private float rotateVelocity;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsJumpPoint;
    [SerializeField]
    private LayerMask whatIsGroundJumpPoint;
    [SerializeField]
    private Transform Sprite;

    private RaycastHit2D hitGround;

    public float angleOnSlope;

    public bool IsJumping { get; set; }
    public bool isOnSlope;


    private int gravity;


    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMovement();
        CheckWhenNearGroundJumpPoint();
        Jump();
        IsOnSlope();
    }

    private void PlayerMovement()
    {
        _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
    }

    private void Jump()
    {
        if (OnGrounded() || IsNearJumpPoint())
        {
            Vector3 Rotation = this.transform.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            this.transform.rotation = Quaternion.Euler(Rotation);
            if (IsJumping)
            {
                gravity = (int)CurrentGravity;
                _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], 1.3f * jumpForce * gravity);
            }
        }
        else
        {
            this.transform.Rotate((gravity == 1) ? Vector3.back * 2f : Vector3.forward * 2f);
        }
        IsJumping = false;
    }


    private void CheckWhenNearGroundJumpPoint()
    {
        if (IsNearGroundJumpPoint())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 1.5f * jumpForce);
        }
    }

    public bool OnGrounded()
    {
        hitGround = Physics2D.Raycast(this.transform.position + Vector3.up, Vector2.down, checkGroundDistance, whatIsGround);
        return hitGround.collider != null;
    }

    public bool IsNearJumpPoint()
    {
        var hit = Physics2D.CircleCast(this.transform.position, jumpPointDistance, Vector2.zero, 0, whatIsJumpPoint);
        return hit.collider != null;
    }

    public bool IsNearGroundJumpPoint()
    {
        var hit = Physics2D.CircleCast(this.transform.position, jumpPointDistance, Vector2.zero, 0, whatIsGroundJumpPoint);
        return hit.collider != null;
    }

    public void IsOnSlope()
    {
        Debug.DrawRay(hitGround.point, hitGround.normal, Color.yellow);
        if (hitGround.normal.x == 0 && hitGround.normal.y == 1)
        {
            isOnSlope = false;
        }
        else
        {
            isOnSlope = true;
            angleOnSlope = Mathf.Abs(Mathf.Atan2(hitGround.normal.y, hitGround.normal.x) * Mathf.Rad2Deg);
        }

        if (angleOnSlope != 0)
        {
            this.transform.Rotate(Vector3.back * Time.deltaTime, 180 - angleOnSlope);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position + Vector3.up, Vector2.down * checkGroundDistance);
        //Gizmos.DrawWireSphere(this.transform.position, jumpPointDistance);
    }

    //private void ChangeThroughPortal(GameModes GameMode, Speeds Speed, Gravity Gravity, int State)
    //{
    //    switch (State)
    //    {
    //        case 0:
    //            CurrentSpeed = Speed;
    //            break;
    //        case 1:
    //            CurrentGameMode = GameMode;
    //            GameModes[] gameModes = { GameModes.Cube, GameModes.Ship, GameModes.UFO, GameModes.Ball, GameModes.Spider, GameModes.Wave };
    //            foreach(GameModes gameMode in gameModes)
    //            {
    //                if(gameMode == CurrentGameMode)
    //                {
    //                    Observer.Instance.Notify(EventID.OnPlayerTransform, (GameModes)gameMode);
    //                }
    //            }

    //            //if (CurrentGameMode == GameModes.Ship)
    //            //{
    //            //    Observer.Instance.Notify(EventID.OnPlayerTransform, GameModes.Ship);
    //            //}
    //            //else if (CurrentGameMode == GameModes.Cube)
    //            //{
    //            //    Observer.Instance.Notify(EventID.OnPlayerTransform, GameModes.Cube);
    //            //}
    //            break;
    //        case 2:
    //            _rb.gravityScale = Mathf.Abs(_rb.gravityScale) * (int)Gravity;
    //            CurrentGravity = Gravity;
    //            break;
    //    }
    //}

    //public void OnPortalTouch(PortalScript portalScript)
    //{
    //    ChangeThroughPortal(portalScript.GameMode, portalScript.Speed, portalScript.Gravity, portalScript.State);
    //}
}
