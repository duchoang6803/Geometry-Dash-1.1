using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerAgent : MonoBehaviour, ITransformOnPortalTouch
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] protected D_PlayerData playerData;
    [SerializeField] private SoundManager soundManager;

    private PlayerController _playerCube;
    [HideInInspector]
    public Rigidbody2D _rb;
    protected float[] speedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.2f, 21f, 32f };

    public Speeds CurrentSpeed;
    public GameModes CurrentGameMode;
    public Gravity CurrentGravity;

    private int state { get; set; }
    protected int JumpGravity { get; set; }


    private void Start()
    {
        Observer.Instance.AddObserver(EventID.OnPlayerTransform, PlayerTransform);
    }

    private void OnDestroy()
    {
        if (Observer.Instance)
        {
            Observer.Instance.RemoveObserver(EventID.OnPlayerTransform, PlayerTransform);
        }

    }


    private void PlayerTransform(object transform)
    {
        CurrentGameMode = (GameModes)transform;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if ((GameModes)i == CurrentGameMode)
            {
                gameObjects[i].SetActive(true);
            }
            else
            {
                gameObjects[i].SetActive(false);
            }
        }
    }


    private void ChangeThroughPortal(GameModes GameMode, Speeds Speed, Gravity Gravity, int State)
    {
        this.state = State;
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                CurrentGameMode = GameMode;
                GameModes[] gameModes = { GameModes.Cube, GameModes.Ship, GameModes.UFO, GameModes.Ball, GameModes.ShipZigZag };
                foreach (GameModes gameMode in gameModes)
                {
                    if (gameMode == CurrentGameMode)
                    {
                        Observer.Instance.Notify(EventID.OnPlayerTransform, (GameModes)gameMode);
                    }
                }
                break;
            case 2:
                CurrentGravity = Gravity;
                JumpGravity = (int)Gravity;
                _rb.gravityScale = Mathf.Abs(_rb.gravityScale) * (int)Gravity;
                break;
        }
    }

    public void CheckObstacle(GameObject gameObject, float radiusCheckCollider, LayerMask whatIsObstacle)
    {
        Collider2D checkHitObstacle = Physics2D.OverlapCircle(gameObject.transform.position, radiusCheckCollider, whatIsObstacle);
        if (checkHitObstacle == null) return;
        Destroy(gameObject);
        Observer.Instance.Notify(EventID.OnMusicPlayerDead, null);
        Observer.Instance.Notify(EventID.OnSFXDeadSound, null);
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0f;
    }

    public bool OnGround(GameObject gameObject, RaycastHit2D hitGrounded)
    {
        hitGrounded = Physics2D.Raycast(gameObject.transform.position + Vector3.up, Vector2.down, playerData.checkGroundDistance, playerData.whatIsGround);
        return hitGrounded.collider != null;
    }

    public bool IsNearGroundJumpPoint(GameObject gameObject)
    {
        var hit = Physics2D.CircleCast(gameObject.transform.position, playerData.allObjectJumpPointDistance, Vector2.zero, 0, playerData.whatIsGroundJumpPoint);
        return hit.collider != null;
    }

    public bool IsNearAirJumpPoint(GameObject gameObject)
    {
        var hit = Physics2D.CircleCast(gameObject.transform.position, playerData.allObjectJumpPointDistance, Vector2.zero, 0, playerData.whatIsAirJumpPoint);
        return hit.collider != null;
    }

    public bool IsNearAirJumpGravity(GameObject gameObject)
    {
        var hit = Physics2D.CircleCast(gameObject.transform.position, playerData.allObjectJumpPointDistance, Vector2.zero, 0, playerData.whatIsAirJumpGravity);
        return hit.collider != null;
    }

    public void OnPortalTouch(PortalScript portalScript)
    {
        ChangeThroughPortal(portalScript.GameMode, portalScript.Speed, portalScript.Gravity, portalScript.State);
    }

}
