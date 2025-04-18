using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float respawnTime = 2f;
    private bool isRespawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SampleScene");
            Observer.Instance.Notify(EventID.OnLoadScenePlayerDead, null);
            //Debug.Log("kaofdho");
        }
    }
}
