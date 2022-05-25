using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    // Scripts
    [SerializeField]
    private PauseMenu pm;
    [SerializeField]
    private Boost bst;

    // Screen
    private Vector3 position;
    private float width;
    private float height;

    // Player hit
    public bool playerHit;
    private int counter;
    private SphereCollider col;
    public bool gameOver;
    public bool explode;

    // Color
    private Color ogColor;
    [SerializeField]
    private MeshRenderer mr;
    public GameObject obj;

    // Timer
    private float timer;
    private float timeAllotted;

    // Audio
    [SerializeField]
    private AudioSource pickup;
    [SerializeField]
    private AudioSource hit;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        position = new Vector3(0.0f, 0.0f, 0.0f);

        pickup.volume = OptionsMenu.volumeLevel;
        hit.volume = OptionsMenu.volumeLevel;
    }

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<SphereCollider>();
        timer = 1f;
        timeAllotted = 1f;
        counter = 0;
        ogColor = mr.material.color;
    }

    // Mobile
   /* private void CheckInput()
    {
        // Get touch position
        Vector2 pos = Input.GetTouch(0).position;
        if (CheckOrientation.portraitMode)
        {
            pos.x = (pos.x - width) / width * 3.5f; // portrait 3.5f // landscape 9.5f
            pos.y = (pos.y - height) / height * 5.5f; // portrait 5.5f // landscape 5f
        }
        else
        {
            pos.x = (pos.x - width) / width * 9.5f;
            pos.y = (pos.y - height) / height * 5f;
        }
        position = new Vector3(pos.x, pos.y, 20f);

        // Position the player
        transform.position = position;
    }*/

/*    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && !pm.gameIsPaused && !gameOver) CheckInput();
    }*/
    // end Mobile

    // PC
    private void CheckMouseInput()
    {
        // Get touch position
        Vector2 pos = Input.mousePosition;
        pos.x = (pos.x - width) / width * 12f; // value changes with aspect ratio
        pos.y = (pos.y - height) / height * 5f;
        position = new Vector3(pos.x, pos.y, 20f);

        // Position the player
        transform.position = position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !pm.gameIsPaused && !gameOver)
        {
            CheckMouseInput();
        }
        // end PC

        // if player is hit
        if (playerHit)
        {
            bst.boostsPlayerHas = 0;
            if (timer > 0)
            {
                // decrease timer
                timer -= Time.deltaTime * 1.5f;
                mr.material.color = new Color(1f - timer * 1.5f, 0f, 0f);
                col.isTrigger = false;
            }
            else if (timer <= 0)
            {
                mr.material.color = new Color(255f, 0f, 0f);
                counter++;
                timer = timeAllotted;
            }
            if (counter == 1)
            {
                //Handheld.Vibrate();
            }
            if (counter == 3)
            {
                mr.material.color = ogColor;
                col.isTrigger = true;
                counter = 0;
                playerHit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerHit = true;
            hit.Play();
            if (bst.boostsPlayerHas == 0)
            {
                gameOver = true;
            }
            else
            {
                explode = true;
                obj = other.gameObject;
            }
        }

        if (other.gameObject.CompareTag("Boost"))
        {
            bst.boostsPlayerHas++;
            pickup.Play();
        }
    }
}