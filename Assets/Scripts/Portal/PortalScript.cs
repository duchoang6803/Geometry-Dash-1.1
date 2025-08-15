using System;
using UnityEngine;

public interface ITransformOnPortalTouch
{
    public void OnPortalTouch(PortalScript portalScript);

}




public class PortalScript : MonoBehaviour
{
    public Speeds Speed;
    public GameModes GameMode;
    public Gravity Gravity;
    public int State;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var transformOnTouch = collision.gameObject.GetComponent<ITransformOnPortalTouch>();
        transformOnTouch.OnPortalTouch(this);
    }
}
