using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toothManager : MonoBehaviour
{
    public float damageRecive = 0;
    public int numberOfTeethDamaged;
    public float teethMultiplire = 20;
    public int numberOfteeth;
    public Image[] theTeeth;
    public int id;
    private heroManager theHeroManager;
    public bool canDamage = true;
    public float rollbackWait = 7f;

    void Start()
    {
        theHeroManager = GameObject.Find("heroManager").GetComponent<heroManager>();
    }


    void Update()
    {
        if ((teethMultiplire * (numberOfTeethDamaged + 1)) <= damageRecive)
        {
            if (numberOfteeth > numberOfTeethDamaged)
            {
                numberOfTeethDamaged++;
            }
        }

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     damageRecived(10);
        // }
    }

    public void damageRecived(float damage)
    {
        if (canDamage)
        {
            damageRecive += damage;

            for (int i = 0; i < numberOfTeethDamaged; i++)
            {
                theTeeth[i].GetComponent<teethProfile>().recieveDamage();
            }

            theHeroManager.damageReceived();
        }
    }

    public void rollbackTheTeethAnimation()
    {
        Invoke("DoRollbackTheTeethAnimation", rollbackWait);
    }
    public void DoRollbackTheTeethAnimation()
    {
        for (int i = 0; i < numberOfTeethDamaged; i++)
        {
            theTeeth[i].GetComponent<teethProfile>().healTeeth();
        }

    }
}
