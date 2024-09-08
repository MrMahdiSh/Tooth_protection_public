using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fin : MonoBehaviour
{
    public GameObject UI;

    public void done()
    {
        UI.SetActive(true);
        spawnManager theSpawn = GameObject.Find("spawnManager").GetComponent<spawnManager>();
        theSpawn.isSpawn = false;
        Time.timeScale = .1f;
    }
    public void reset()
    {
        Time.timeScale = 1;
        GameObject.Find("theActualGameManger").GetComponent<theActualGameManger>().startTheGame();
        SceneManager.LoadScene("animation");
    }
    public void home()
    {
        Destroy(GameObject.Find("theActualGameManger").gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene("main_menu");
    }
}
