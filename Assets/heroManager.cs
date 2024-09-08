using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heroManager : MonoBehaviour
{
    private toothManager[] toothmanagers;
    public float totalReceivedDamages;
    private bool isKhamirCame = false;
    private bool isNakhCame = false;
    private bool isKhamirUiCame = false;
    private bool isNakhUiCame = false;
    public GameObject khamir;
    public GameObject nakh;
    public GameObject Hobab;
    public GameObject khamirUi;
    public GameObject nakhUi;
    public Animator zaban;
    public float khamirUiCameAfterDamages;
    public float nakhUiCameAfterDamages;
    public float khamirWait;
    public int khamirUiShouldHitCount;
    public int nakhUiShouldHitCount;
    public int numberOfUsersHitKhamirUi;
    public int numberOfUsersHitNakhUi;
    private spawnManager spawnManager;
    private bool teethHealed;
    public bool isNakhIn = false;
    public float nakhRepeatRate;
    public float slowMotionFactor = 0.5f;
    public Text UserHitKhamir;
    public Text UserHitNakh;

    void Start()
    {
        teethHealed = false;
        spawnManager = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        toothmanagers = GameObject.Find("lase").GetComponents<toothManager>();
    }

    void Update()
    {
        int x = khamirUiShouldHitCount - numberOfUsersHitKhamirUi;
        UserHitKhamir.text = x.ToString();
        int y = nakhUiShouldHitCount - numberOfUsersHitNakhUi;
        UserHitNakh.text = y.ToString();
        if (isNakhIn)
        {
            // stop damaging
            foreach (var item in toothmanagers)
            {
                item.canDamage = false;
            }
            // stop spawn
            spawnManager.isSpawn = false;

        }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     bringNakhUi();
        // }

        if (totalReceivedDamages >= khamirUiCameAfterDamages && !isKhamirUiCame)
        {
            bringKhamirUi();
        }

        if (totalReceivedDamages >= nakhUiCameAfterDamages && !isNakhUiCame)
        {
            bringNakhUi();
        }

        if (numberOfUsersHitKhamirUi >= khamirUiShouldHitCount && !isKhamirCame)
        {
            StartCoroutine(bringKhamir());
        }

        if (numberOfUsersHitNakhUi >= nakhUiShouldHitCount && !isNakhCame)
        {
            bringNakh();
        }
    }

    public void damageReceived()
    {
        totalReceivedDamages = 0;
        foreach (var item in toothmanagers)
        {
            totalReceivedDamages += item.damageRecive;
        }
    }

    void bringKhamirUi()
    {
        // stop damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = false;
        }

        isKhamirUiCame = true;
        khamirUi.GetComponent<Animator>().Play("in");
        khamirUi.transform.GetChild(0).gameObject.SetActive(true);
        // slow motion
        slowMotion();
    }
    void bringNakhUi()
    {
        // stop damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = false;
        }

        isNakhUiCame = true;
        nakhUi.GetComponent<Animator>().Play("in");
        nakhUi.transform.GetChild(0).gameObject.SetActive(true);
        // slow motion
        slowMotion();
    }
    IEnumerator bringKhamir()
    {
        // start it
        normalScaleTime();
        isKhamirCame = true;
        khamirUi.GetComponent<Animator>().Play("out");
        khamirUi.transform.GetChild(0).gameObject.SetActive(false);
        Hobab.SetActive(true);
        khamir.SetActive(true);
        zaban.Play("zaban");
        // normal time scale
        // stop spawn
        spawnManager.isSpawn = false;
        // roll back teeth
        if (teethHealed == false)
        {
            teethHealed = true;
            foreach (var tooth in toothmanagers)
            {
                tooth.rollbackTheTeethAnimation();
            }
        }

        // dont count for enemies
        spawnManager.isBoss = true;

        yield return new WaitForSeconds(khamirWait);

        spawnManager.isBoss = false;

        foreach (var item in toothmanagers)
        {
            item.damageRecive = 0;
            item.numberOfTeethDamaged = 0;
        }

        totalReceivedDamages = 0;

        teethHealed = false;
        // kill enemies
        killEnemiesKhamir();
        // bring back the damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = true;
        }

        // bring back the spawn
        spawnManager.isSpawn = true;
        // bring the khamir and hobabs out
        khamir.GetComponent<Animator>().Play("back");
        GameObject[] hobabs = GameObject.FindGameObjectsWithTag("hobabs");
        foreach (var hobab in hobabs)
        {
            hobab.GetComponent<Animator>().Play("out");
        }
    }

    void bringNakh()
    {
        // start it
        nakhUi.GetComponent<Animator>().Play("out");
        nakhUi.transform.GetChild(0).gameObject.SetActive(false);
        isNakhCame = true;
        isNakhIn = true;
        zaban.Play("zaban");
        // normal time scale
        normalScaleTime();
        // bring the character in
        nakh.GetComponent<Animator>().Play("in");
        StartCoroutine(nakhKillingEnemiesNew());
    }

    IEnumerator nakhKillingEnemiesNew()
    {
        // count enemies
        if (isNakhIn)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
            bool enemiesNumber = false;
            foreach (var item in enemies)
            {
                if (item.GetComponent<theCharacter>().isDeath == false && item.GetComponent<theCharacter>().nooshabe == false && item.GetComponent<theCharacter>().isGhand == false)
                {
                    nakh.GetComponent<Animator>().Play("attack");
                    enemiesNumber = true;
                    item.GetComponent<theCharacter>().killByNakh();
                    break;
                }
            }

            if (enemiesNumber == false)
            {
                // heal the teeth
                foreach (var tooth in toothmanagers)
                {
                    tooth.DoRollbackTheTeethAnimation();
                }

                nakhKillingDone();
                isNakhIn = false;
            }
            else
            {
                yield return new WaitForSeconds(nakhRepeatRate);
                StartCoroutine(nakhKillingEnemiesNew());
            }
        }
    }

    void nakhKillingDone()
    {
        // bring back the damaging
        foreach (var item in toothmanagers)
        {
            item.canDamage = true;
            item.damageRecive = 0;
            item.numberOfTeethDamaged = 0;
        }

        // bring back the spawn
        spawnManager.isSpawn = true;
        nakh.GetComponent<Animator>().Play("out");
    }

    void killEnemiesKhamir()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemies");
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<theCharacter>().isDeath == false && enemy.GetComponent<theCharacter>().nooshabe == false && enemy.GetComponent<theCharacter>().isGhand == false)
            {
                enemy.GetComponent<theCharacter>().deathByKhamir();
            }
        }

        // GameObject theSlider = GameObject.Find("slider");

        // foreach (Transform child in theSlider.transform)
        // {
        //     Destroy(child.gameObject);
        // }
    }

    public void khamirUserClick()
    {
        numberOfUsersHitKhamirUi++;
    }

    public void nakhUserClick()
    {
        numberOfUsersHitNakhUi++;
    }

    void slowMotion()
    {
        Time.timeScale = slowMotionFactor;
    }

    void normalScaleTime()
    {
        Time.timeScale = 1;
    }
}
