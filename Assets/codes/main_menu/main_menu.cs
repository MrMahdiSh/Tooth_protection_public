using UnityEngine.SceneManagement;
using UnityEngine;

public class main_menu : MonoBehaviour
{

    public string sceneName;
    public GameObject[] buttonsTouchGivers;

    void Start()
    {
        Invoke("activeButtons", 4);
    }

    void activeButtons()
    {
        foreach (var item in buttonsTouchGivers)
        {
            item.SetActive(true);
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
