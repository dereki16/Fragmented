using UnityEngine;

public class FloatingObstacles : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float speed;
    private float timer;
    private float timeAllotted;

    private float xRand, yRand, zRand;
    private float xPos, yPos, zPos;


    void Start()
    {
        timeAllotted = 3f;
        timer = timeAllotted;
        xPos = 5f;
        yPos = 5f;
        zPos = 5f;
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
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeAllotted;
            target.transform.position = Randomizer(-xPos, xPos, -yPos, yPos, -zPos, zPos);
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target.position, step * Time.deltaTime);
    }
}