using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    protected Rigidbody2D _rb { get;  set;}
    protected float[] speedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Speeds CurrentSpeed;
    public GameModes CurrentGameMode { get;set; }
    public Gravity CurrentGravity;


    private static PlayerAgent instance;
    public static PlayerAgent Instance => instance;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        Observer.Instance.AddObserver(EventID.OnPlayerTransform, PlayerTransform);
    }


    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayerTransform(object transform)
    {
        CurrentGameMode = (GameModes)transform;
        for(int i = 0;i< gameObjects.Length; i++)
        {
            Debug.Log(CurrentGameMode);
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
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                CurrentGameMode = GameMode;
                GameModes[] gameModes = { GameModes.Cube, GameModes.Ship, GameModes.UFO, GameModes.Ball, GameModes.Spider, GameModes.Wave };
                foreach (GameModes gameMode in gameModes)
                {
                    if (gameMode == CurrentGameMode)
                    {
                        Observer.Instance.Notify(EventID.OnPlayerTransform, (GameModes)gameMode);
                    }
                }

                //if (CurrentGameMode == GameModes.Ship)
                //{
                //    Observer.Instance.Notify(EventID.OnPlayerTransform, GameModes.Ship);
                //}
                //else if (CurrentGameMode == GameModes.Cube)
                //{
                //    Observer.Instance.Notify(EventID.OnPlayerTransform, GameModes.Cube);
                //}
                break;
            case 2:
                _rb.gravityScale = Mathf.Abs(_rb.gravityScale) * (int)Gravity;
                CurrentGravity = Gravity;
                break;
        }
    }

    public void OnPortalTouch(PortalScript portalScript)
    {
        ChangeThroughPortal(portalScript.GameMode, portalScript.Speed, portalScript.Gravity, portalScript.State);
    }




}
