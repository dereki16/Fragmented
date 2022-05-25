using UnityEngine;

public class WhenOver : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject pauseCanvas;

    private void Update()
    {
        if (gameOverCanvas.active == true)
            pauseCanvas.SetActive(false);
    }
}
