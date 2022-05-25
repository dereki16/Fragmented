using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public int score;

    private void Awake()
    {
        Obstacle.score = 0;
    }

    void Start()
    {
        PlayerPrefs.DeleteKey("score");   // resets score to 0
    }

    void Update()
    {
        score = Obstacle.score;
        pointsText.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            score++;
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
        }
    }
}
