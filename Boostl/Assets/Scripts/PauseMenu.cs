using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private MoveByTouch player;
    [SerializeField]
    private GameObject pauseMenuUI;
    public bool gameIsPaused;
    public TextMeshProUGUI text;
    private GameObject[] music;

    private void Start()
    {
        gameIsPaused = false;
        music = GameObject.FindGameObjectsWithTag("Music");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        text.color = Color.white;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
        player.gameOver = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
