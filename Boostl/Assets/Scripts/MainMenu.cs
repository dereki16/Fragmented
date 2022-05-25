using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField gt;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetUsername()
    {
        gt.characterLimit = 15;
        PlayerPrefs.SetString("gt", gt.text);
        PlayerPrefs.Save();
    }
}
