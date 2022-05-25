using UnityEngine;

public class Boost : MonoBehaviour
{
    // Scripts
    [SerializeField]
    public BoostSpawner bs;
    [SerializeField]
    private PauseMenu pm;

    // Speed
    private float minSpeed;
    private float maxSpeed;
    private float randSpeed;

    // Boost
    [SerializeField]
    private Transform boost;
    public int boostsPlayerHas;

    private void Start()
    {
        minSpeed = 0.25f;
        maxSpeed = 0.6f;

        boost.GetComponent<ParticleSystem>();
    }

    private void DequeueBoost()
    {
        bs.boostCounter = 0;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Rand speed
        randSpeed = Random.Range(minSpeed, maxSpeed);
        if (!pm.gameIsPaused)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - randSpeed);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Dequeue boost
        if (transform.position.z <= 5f)
            DequeueBoost();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DequeueBoost();
        }
    }
}
