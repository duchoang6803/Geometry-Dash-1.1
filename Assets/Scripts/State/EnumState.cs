using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4, SuperFast = 5, Demon = 6 };
public enum GameModes { Cube = 0, Ship = 1, UFO = 2, Ball = 3, ShipZigZag = 4, Spider = 5 };
public enum Gravity { Upside = 1, UpsideDown = -1 };

public enum EventID
{
    OnPlayerTransform,
    OnVelocityWhenTouchPortal,
    OnMusicPlayerDead,
    OnSFXDeadSound,
    OnLoadScenePlayerDead
}

