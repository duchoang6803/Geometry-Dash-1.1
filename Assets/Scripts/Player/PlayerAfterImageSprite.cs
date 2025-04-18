using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private float alpha;
    private float alphaMutiplier = 0.85f;
    private float alphaSet = 0.8f;

    [SerializeField]
    private float activeTime = 1f;
    private float timeActivated;

    private Color color;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    //private void OnEnable()
    //{
    //    foreach (GameObject gameObject in gameObjects)
    //    {
    //        if (gameObject != null && gameObject.activeSelf)
    //        {
    //            playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    //            this.transform.position = gameObject.transform.position;
    //            this.transform.rotation = gameObject.transform.rotation;
    //        }
    //    }
    //    spriteRenderer = GetComponent<SpriteRenderer>();

    //    alpha = alphaSet;
    //    spriteRenderer.sprite = playerSpriteRenderer.sprite;
    //    timeActivated = Time.time;

    //}

    private void OnEnable()
    {
        alpha = alphaSet;
    }

    public void SetUp(Sprite sprite,Vector3 position,Quaternion rotation)
    {
        spriteRenderer.sprite = sprite;
        timeActivated = Time.time;
        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    private void OnDisable()
    {
        spriteRenderer.sprite = null;

    }
    private void Update()
    {
        AfterImageSetUp();
    }
   

    private void AfterImageSetUp()
    {
        alpha *= alphaMutiplier;
        color = new Color(1, 1, 1,alpha);
        spriteRenderer.color = color;
        if (Time.time > timeActivated + activeTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(this);
        }
    }
}
