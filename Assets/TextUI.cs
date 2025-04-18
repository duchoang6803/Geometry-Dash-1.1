using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    private Text attemptText;

    private int countAttemp;



    private void OnEnable()
    {
        Debug.Log("OnEnable");
        Observer.Instance.AddObserver(EventID.OnLoadScenePlayerDead, CountAttemptWhenPlayerDead);
    }

    private void OnDisable()
    {
        if (Observer.Instance != null)
        {
            Observer.Instance.RemoveObserver(EventID.OnLoadScenePlayerDead, CountAttemptWhenPlayerDead);
        }
    }

    private void Start()
    {
        //countAttemp = PlayerPrefs.GetInt("Attempt", 1);
        attemptText.text = "Attempt: " + AttemptWhenDead.dieCount.ToString();


    }

    private void CountAttemptWhenPlayerDead(object data)
    {
        if (attemptText != null)
        {
            AttemptWhenDead.dieCount++;
            //Debug.Log("Text");
            attemptText.text = "Attempt: " + AttemptWhenDead.dieCount.ToString();
            //PlayerPrefs.SetInt("Attempt", countAttemp);

        }
    }

    private void ResetCounterAttempt()
    {
        PlayerPrefs.DeleteAll();
    }

}
