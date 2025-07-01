using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField]
    private float numberScale;
    private void Start()
    {
        transform.DOScale(numberScale, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
