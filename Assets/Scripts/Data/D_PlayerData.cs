using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/PlayerData/BaseData")]
public class D_PlayerData : ScriptableObject
{
    public float allObjectJumpPointDistance;
    public float allObjectJumpForce = 30f;
    public float checkGroundDistance = 1.5f;
    public float checkWallDistance = 1.5f;

    public LayerMask whatIsGround;
    public LayerMask whatIsGroundJumpPoint;
    public LayerMask whatIsAirJumpPoint;
    public LayerMask whatIsWall;
    public LayerMask whatIsObstacle;
    public LayerMask whatIsAirJumpGravity;
}
