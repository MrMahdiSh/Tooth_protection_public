using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class animation : MonoBehaviour
{
    public float countdownTime = 3f; // Adjust as needed
    private float currentTime;

    public string nextSceneText;

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     ChangeScene();
        // }
    }
    void Start()
    {
        currentTime = countdownTime;
        InvokeRepeating("Countdown", 1f, 1f); // Start counting down every second
    }

    void Countdown()
    {
        currentTime -= 1f;

        if (currentTime <= 0)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        GameObject.Find("theActualGameManger").GetComponent<theActualGameManger>().startTheGame();
        SceneManager.LoadScene(nextSceneText);
    }
}
