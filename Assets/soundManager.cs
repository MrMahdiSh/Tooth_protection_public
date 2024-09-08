using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theActualGameManger myManager = null;
        GameObject managerObject = GameObject.Find("theActualGameManger");

        if (managerObject == null)
            return;

        myManager = managerObject.GetComponent<theActualGameManger>();

        if (myManager == null)
            return;

        if (!myManager.IsMusic || !myManager.isSound)
            gameObject.SetActive(false);
    }
}
