using System;
using UnityEngine;

public interface ITransformOnPortalTouch
{
    public void OnPortalTouch(PortalScript portalScript);

}

//public interface IVelocityOnPortalTouch
//{
//    public void VelocityOnPortalTouch();
//}


public class PortalScript : MonoBehaviour
{
    public Speeds Speed;
    public GameModes GameMode;
    public Gravity Gravity;
    public int State;

    public Action OnTransform;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var transformOnTouch = collision.gameObject.GetComponent<ITransformOnPortalTouch>();
        //var velocityOnTouch = collision.gameObject.GetComponent<IVelocityOnPortalTouch>();
        transformOnTouch.OnPortalTouch(this);
        //velocityOnTouch.VelocityOnPortalTouch();

        
    }
}
