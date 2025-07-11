using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
