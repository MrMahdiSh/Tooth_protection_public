using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text timerText;
    private theActualGameManger theActualGameManger;

    // Start is called before the first frame update
    void Start()
    {
        theActualGameManger = GameObject.Find("theActualGameManger").GetComponent<theActualGameManger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theActualGameManger == null)
            return;
        float time = theActualGameManger.timer;
        int totalSeconds = (int)time;
        int minutesInt = totalSeconds / 60;
        int secondsInt = totalSeconds % 60;

        string minutes = minutesInt.ToString("00");
        string seconds = secondsInt.ToString("00");

        timerText.text = minutes + ":" + seconds;
    }
}
