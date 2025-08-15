using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    [SerializeField]
    private MenuDOTween menuDOTween;
    [SerializeField]
    private SoundManager soundManager;
    public void OnClickPauseGame()
    {
        Observer.Instance.Notify(EventID.OnPlayerVelocityPauseGame, null);
        Observer.Instance.Notify(EventID.OnTurnOfMusic, null);
        menuDOTween.gameObject.SetActive(true);
        menuDOTween.MenuStartAnim();
    }

    public void OnClickContinueGame()
    {
        soundManager.PlayMusicTheme();
        Observer.Instance.Notify(EventID.OnPlayerVelocityContinueGame, null);
        menuDOTween.gameObject.SetActive(false);
    }

}
