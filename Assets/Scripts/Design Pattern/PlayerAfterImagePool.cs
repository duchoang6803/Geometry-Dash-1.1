using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : Singleton<PlayerAfterImagePool>
{
    [SerializeField]
    private PlayerAfterImageSprite playerAfterImagePrefab;
    private Queue<PlayerAfterImageSprite> playerAfterImages = new Queue<PlayerAfterImageSprite>();

    protected override bool ShouldDestroyOnLoad => true;

    public PlayerAfterImageSprite GetFormPool()
    {
        if (playerAfterImages.Count == 0)
        {
            GrowPool();
        }
        PlayerAfterImageSprite playerImage = playerAfterImages.Dequeue();
        playerImage.gameObject.SetActive(true);
        return playerImage;
    }

    private void GrowPool()
    {
        PlayerAfterImageSprite playerImageGrow = Instantiate(playerAfterImagePrefab);
        AddToPool(playerImageGrow);
    }

    public void AddToPool(PlayerAfterImageSprite playerImageToAdd)
    {
        if(playerAfterImages == null) return;
        playerImageToAdd.gameObject.SetActive(false);
        playerAfterImages.Enqueue(playerImageToAdd);
    }
}
