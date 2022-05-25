using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Scripts
    [SerializeField]
    private MoveByTouch player;
    [SerializeField]
    private Boost bst;
    [SerializeField]
    private PauseMenu pm;

    [SerializeField]
    private GameObject explosion;

    // Public
    public bool largeObst;
    public static int score;
    public bool gameOver;

    // Speed
    [SerializeField]
    private float speed;
    private float ogSpeed;
    private float minSpeed;
    private float maxSpeed;

    private float speedLevel1;
    private float speedLevel2;
    private float speedLevel3;

    // Boost/Game
    public bool obstOutBounds;

    // Light color
    [SerializeField]
    private Light light;
    private Color lightColor;

    private void Start()
    {
        ogSpeed = ObstacleSpeed();
        speed = ogSpeed;

        speedLevel1 = ogSpeed + 0.15f;
        speedLevel2 = ogSpeed + 0.3f;
        speedLevel3 = ogSpeed + 0.45f;
    }

    private float ObstacleSpeedLevel()
    {
        switch (bst.boostsPlayerHas)
        {
            case 0:
                speed = ogSpeed;
                break;
            case 1:
                speed = speedLevel1;
                break;
            case 2:
                speed = speedLevel2;
                break;
            case 3:
                speed = speedLevel3;
                break;
            default:
                speed = ogSpeed;
                break;
        }
        return speed;
    }

    private float ObstacleSpeed()
    {
        minSpeed = 0.2f;
        maxSpeed = 0.55f;
        speed = Random.Range(minSpeed, maxSpeed);
        return speed;
    }

    #region Large Obst
    private void ObstacleSizeCheck()
    {
        if (transform.localScale.x >= 750f)
            largeObst = true;
        else
            largeObst = false;
    }
    #endregion

    private void DequeueObstacle()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Sets size
        ObstacleSizeCheck();

        if (player.gameOver)
        {
            if (Time.timeScale > 0f)
                Time.timeScale -= Time.deltaTime / 7;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.timeScale);
        }

        // Sets speed ig game isn't paused
        speed = ObstacleSpeedLevel();
        if (!pm.gameIsPaused && !player.gameOver)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        else if (pm.gameIsPaused)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Sets up color of light
        light.color = new Color(lightColor.r + 0.050f, lightColor.g + 0.050f, lightColor.b + 0.050f, lightColor.a);

        // Adds to score and dequeues obst
        if (transform.position.z <= 5f && !player.gameOver)
        {
            score++;
            DequeueObstacle();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.explode)
        {
            explosion.SetActive(true);
            player.explode = false;
        }
    }
}
