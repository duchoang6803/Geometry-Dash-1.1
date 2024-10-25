using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAgentFlyingMovement : PlayerAgent,ITransformOnPortalTouch
{
    [SerializeField]
    private float agentFlyingSpeed;
    [SerializeField]
    private float agentFlyingUpSpeed;
    public bool isFlying { get; set; }

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        FlyingAgentMovement();
        FlyingUpMovement();
    }

    private void FlyingAgentMovement()
    {
        _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
    }

    private void FlyingUpMovement()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, _rb.velocity.y * 2);
        if (isFlying)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, agentFlyingUpSpeed);
            _rb.gravityScale = -4.315f;
            isFlying = false;
        }
        _rb.gravityScale = 4.315f;
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
    //            break;
    //        case 2:
    //            _rb.gravityScale = Mathf.Abs(_rb.gravityScale) * (int)Gravity;
    //            break;
    //    }
    //}

    //public void OnPortalTouch(PortalScript portalScript)
    //{
    //    ChangeThroughPortal(portalScript.GameMode, portalScript.Speed, portalScript.Gravity, portalScript.State);
    //}
}
