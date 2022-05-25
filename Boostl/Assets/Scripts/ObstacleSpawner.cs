using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obst;
    public float timer;
    public float timeAllotted;

    private float xRand, yRand, zRand;
    private float xPos, yPos, zPos;


    void Start()
    {
        timeAllotted = 1f;
        timer = timeAllotted;

        xPos = 7f;
        yPos = 4f;
        zPos = 500f;
    }

    private Vector3 Randomizer(float x, float x2, float y, float y2, float z, float z2)
    {
        xRand = Random.Range(x, x2);
        yRand = Random.Range(y, y2);
        zRand = Random.Range(z, z2);
        return new Vector3(xRand, yRand, zRand);
    }

    void Update()
    {
        if (timer > 0)
        {
            // decrease timer
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeAllotted;
            obst = ObjectPooler.sharedInstance.GetPooledObject();
            if (obst != null)
            {
                obst.transform.position = Randomizer(-xPos, xPos, -yPos, yPos, zPos, zPos);
                obst.SetActive(true);
            }
        }
    }
}
