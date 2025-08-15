using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuDOTween : MonoBehaviour
{
    private RectTransform rectTrasnform;
    private CanvasGroup groupCanvas;
    [SerializeField]
    private float duration;

  

    private void Awake()
    {
        rectTrasnform = GetComponent<RectTransform>();
        groupCanvas = GetComponent<CanvasGroup>();
    

    }


    public void MenuStartAnim()
    {
        if (rectTrasnform == null && groupCanvas == null)
        {
            Debug.Log("Null");
        }
        groupCanvas.alpha = 0f;
        rectTrasnform.transform.localPosition = new Vector3(0f, 1000f, 0f);
        Sequence sequence = DOTween.Sequence();
        // Smooth Doawn
        sequence.Append(rectTrasnform.DOAnchorPos(Vector2.zero, duration * 0.2f, false).SetEase(Ease.OutQuad));
        // Bouncing when stop
        sequence.Append(rectTrasnform.DOAnchorPos(new Vector2(0f, -40f), duration * 0.1f, false).SetEase(Ease.OutQuad));
        sequence.Append(rectTrasnform.DOAnchorPos(new Vector2(0f, 30f), duration * 0.1f, false).SetEase(Ease.InQuad));
        sequence.Append(rectTrasnform.DOAnchorPos(new Vector2(0f, -20f), duration * 0.08f, false).SetEase(Ease.OutQuad));
        sequence.Append(rectTrasnform.DOAnchorPos(new Vector2(0f, 10f), duration * 0.08f, false).SetEase(Ease.InQuad));
        sequence.Append(rectTrasnform.DOAnchorPos(new Vector2(0f, -5f), duration * 0.08f, false).SetEase(Ease.OutQuad));
        sequence.Append(rectTrasnform.DOAnchorPos(Vector2.zero, duration * 0.05f, false).SetEase(Ease.InQuad));

        groupCanvas.DOFade(1f, duration);
    }


}
