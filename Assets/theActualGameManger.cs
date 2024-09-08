using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class theActualGameManger : MonoBehaviour
{
    public GameObject settingsWin;
    public bool isSound = true;
    public bool IsMusic = true;
    public float selectedMinutes;
    private float selectedSeconds;
    public GameObject mainMenu;
    public GameObject[] firstThreeTouchGivers;
    public Animator musicIcon;
    public Animator soundIcon;
    public bool isGameDone;
    private bool bossCalled = false;

    // timer
    private bool startTimer;
    public float timer;
    private bool isTimeUp;
    public int numberOfBoss;
    public int NumberOfDeathBoss;

    void Start()
    {
        if (PlayerPrefs.HasKey("selectedMinutes"))
        {
            selectedMinutes = PlayerPrefs.GetFloat("selectedMinutes");
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     timeUp();
        //     Debug.Log("times UP!");
        // }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fin();
        }

        if (SceneManager.GetActiveScene().name == "game" && !bossCalled)
        {
            bossCalled = true;
            spawnTheBossHelper();
        }

        // Timer
        if (startTimer)
        {
            timer = selectedSeconds - Time.time;
            if (timer <= 0)
            {
                startTimer = false;
                timeUp();
            }
        }

        // check the end of the game
        if (isTimeUp && !isGameDone)
        {
            // count the enemies
            GameObject[] enemiesCount = GameObject.FindGameObjectsWithTag("enemies");

            if (enemiesCount.Length <= 0)
            {
                if (numberOfBoss == 1)
                {
                    // GameObject.Find("spawnManager").GetComponent<spawnManager>().bringBoss();
                }
                else if (numberOfBoss == 2)
                {
                    fin();
                }
            }
        }

    }
    public void handleClicksOnOne()
    {
        float selected = 1.5f;
        saveSelectedMinutes(selected);
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(selected);
    }
    public void handleClicksThree()
    {
        float selected = 3;
        saveSelectedMinutes(selected);
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(selected);
    }
    public void handleClicksFive()
    {
        float selected = 5;
        saveSelectedMinutes(selected);
        Invoke("settingsOutAnimation", .5f);
        selectMinutes(selected);
    }

    void saveSelectedMinutes(float minute)
    {
        PlayerPrefs.SetFloat("selectedMinutes", minute);
    }

    public void handleClickOnSound()
    {
        if (isSound)
            soundIcon.GetComponent<Animator>().Play("offSound");
        else
            soundIcon.GetComponent<Animator>().Play("onSound");

        isSound = !isSound;

    }

    public void handleClickOnMusic()
    {
        if (IsMusic)
            musicIcon.GetComponent<Animator>().Play("offMusic");
        else
            musicIcon.GetComponent<Animator>().Play("onMusic");

        IsMusic = !IsMusic;

    }

    public void clickOnSettings()
    {
        if (isSound)
        {
            soundIcon.Play("onSound");
        }
        else
        {
            soundIcon.Play("offSound");
        }

        if (IsMusic)
        {
            musicIcon.Play("onMusic");
        }
        else
        {
            musicIcon.Play("offMusic");
        }
    }

    // private voids
    void settingsOutAnimation()
    {
        soundIcon.GetComponent<Animator>().Play("soundOut");
        musicIcon.GetComponent<Animator>().Play("musicOut");
        settingsWin.GetComponent<Animator>().Play("out");
        mainMenu.GetComponent<Animator>().Play("itemsIn");
        inActiveSettingsWindow();
    }

    void inActiveSettingsWindow()
    {
        settingsWin.SetActive(false);
        settingsWin.SetActive(false);
        foreach (var item in firstThreeTouchGivers)
        {
            item.SetActive(true);
        }
    }

    void selectMinutes(float target)
    {
        selectedMinutes = target;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void startTheGame()
    {
        selectedSeconds = 0;
        selectedSeconds = selectedMinutes * 60;
        selectedSeconds += Time.time;
        startTimer = true;
    }

    public void spawnTheBossHelper()
    {
        spawnManager theSpawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
    }

    public void fin()
    {
        isGameDone = true;
        GameObject finUI = GameObject.Find("finUI");
        finUI.GetComponent<fin>().done();
        resetTheVariables();
    }

    public void timeUp()
    {
        isTimeUp = true;
        // don't spawn
        spawnManager SM = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        SM.isSpawn = false;
        // ready for boss
        SM.readyForBoss = true;
    }

    void resetTheVariables()
    {
        isGameDone = false;
        isTimeUp = false;
        bossCalled = false;
        numberOfBoss = 0;
    }

    public void bossCame()
    {
        numberOfBoss++;
    }
}
