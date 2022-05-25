using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    // Scripts
    [SerializeField]
    private PlayerScore ps;
    [SerializeField]
    private MoveByTouch player;
    
    // Game Over Canvas
    [SerializeField]
    private GameObject gameOver;
    private int highScore;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private Image panel;

    // Audio
    public AudioSource playerDeath;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = highScore.ToString();
        //PlayerPrefs.DeleteKey("highScore");   // rests highscore to 0
    }

    private void Update()
    {
        if (player.gameOver)
        {
            if (Time.timeScale > 0f)
                Time.timeScale -= Time.deltaTime / 7;

            // set up game over canvas
            scoreText.text = ps.score.ToString();
            if (ps.score > highScore)
            {
                PlayerPrefs.SetInt("highScore", ps.score);
                PlayerPrefs.Save();
                highScoreText.text = ps.score.ToString();
            }
            gameOver.SetActive(true);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a + 0.0001f);
        }
    }
}
