using UnityEngine;

public class ObstacleRandomizer : MonoBehaviour
{
    // Script
    [SerializeField]
    private ObjectPooler op;
    // Obst
    private GameObject obst;
    // Rand
    private float xRand, yRand, zRand;

    // Positions
    private Vector3 position;
    private float xPos, yPos, zPos;

    // Sizes
    private Vector3 size;
    private float xMinSize, xMaxSize;
    private float yMinSize, yMaxSize;
    private float zMinSize, zMaxSize;

    // Color
    [SerializeField]
    private Color color;

    // Timer
    private float timer;
    private float timeAllotted;

    // Special Obstacles
    // Rotations
    private Vector3 cubeRot0, cubeRot1, cubeRot2;
    private Vector3 recRot0, recRot1, recRot2;
    private Vector3 triRot0, triRot1, triRot2;
    private float eulers;
/*    public int zone;
*/
    private void Start()
    {
        // Positions
        xPos = 5f;
        yPos = 2f;
        zPos = 500f;
        // Sizes
        xMinSize = 300f;
        xMaxSize = 500f;
        yMinSize = 300f;
        yMaxSize = 500f;
        zMinSize = 300f;
        zMaxSize = 1000f;

        // Setting special obstacles
        // Cube transform
        cubeRot0 = new Vector3(90f, 0f, 0f);
        cubeRot1 = new Vector3(0f, 90f, 90f);
        cubeRot2 = new Vector3(90f, 90f, 90f);
        // Rectangle transform
        recRot0 = new Vector3(-90f, 0f, 0f);
        recRot1 = new Vector3(-90f, 90f, 90f);
        recRot2 = new Vector3(-90f, -90f, 90f);
        // Triangle transform
        triRot0 = new Vector3(0f, 230f, 45f);
        triRot1 = new Vector3(-169f, 347f, 45f);
        triRot2 = new Vector3(22f, 220f, 0f);
        // Angles
        eulers = 360f;
    }

    private Vector3 Randomizer(float x, float x2, float y, float y2, float z, float z2)
    {
        xRand = Random.Range(x, x2);
        yRand = Random.Range(y, y2);
        zRand = Random.Range(z, z2);
        return new Vector3(xRand, yRand, zRand);
    }

    public Color ObstColor()
    {
        color = UnityEngine.Random.ColorHSV();
        return color;
    }

    #region Special Obstacle
    private Vector3 SpecialObstacleRotation(int obst)
    {
        // 3 rotations each to select from if obst special
        int randRot = Random.Range(1, 3);
        if (obst >= 21 && obst <= 40)
        {
            switch (randRot)
            {
                case 0:
                    transform.eulerAngles = cubeRot0;
                    break;
                case 1:
                    transform.eulerAngles = cubeRot1;
                    break;
                case 2:
                    transform.eulerAngles = cubeRot2;
                    break;
                default:
                    break;
            }
        }
        else if (obst >= 41 && obst <= 60)
        {
            switch (randRot)
            {
                case 0:
                    transform.eulerAngles = recRot0;
                    break;
                case 1:
                    transform.eulerAngles = recRot1;
                    break;
                case 2:
                    transform.eulerAngles = recRot2;
                    break;
                default:
                    break;
            }
        }
        else if (obst >= 61 && obst <= 80)
        {
            switch (randRot)
            {
                case 0:
                    transform.eulerAngles = triRot0;
                    break;
                case 1:
                    transform.eulerAngles = triRot1;
                    break;
                case 2:
                    transform.eulerAngles = triRot2;
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.eulerAngles = Randomizer(-eulers, eulers, -eulers, eulers, -eulers, eulers);
        }
        return transform.eulerAngles;
    }

    private Vector3 SpecialObstacleSize()
    {
        // 2 size ranges to select from - small & large
        int randSizeRange = Random.Range(0, 2);
        int randSmallSize = Random.Range(100, 200);
        int randLargeSize = Random.Range(750, 1500);

        // small size obst
        switch (randSizeRange)
        {
            case 0:
                // size = small range size
                transform.localScale = new Vector3(randSmallSize, randSmallSize, randSmallSize);
                break;
            case 1:
                // size = big range size
                transform.localScale = new Vector3(randLargeSize, randLargeSize, randLargeSize);
                break;
            default:
                break;
        }
        return transform.localScale;
    }
    #endregion
    #region Side Obstacles
    private void SideObstacles(GameObject obj, int zone)
    {
        // three zones needed - a, b, c
        // switch statement for which zone will be spawned in next
        //Debug.Log(zone);
        switch (zone)
        {
            case 0:
                obj.transform.position = Randomizer(-80f, -20f, -30f, 30f, 700f, 700f);
                break;
            case 1:
                obj.transform.position = Randomizer(20f, 80f, -30f, 30f, 700f, 700f);
                break;
            default:
                break;
        }
        // make sure to set them as out of bounds  - don't want to add to score
    }
    #endregion

    private void Update()
    {
        // Time between spawn depending on size
        if (op.canBeLarge)
            timeAllotted = Random.Range(0.9f, 1.5f);
        else
            timeAllotted = Random.Range(0.05f, 0.5f);

        // Timer for spawn
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            timer = timeAllotted;
            if (CheckOrientation.portraitMode)
                position = Randomizer(-yPos, yPos, -xPos, xPos, zPos, zPos);
            else
                position = Randomizer(-xPos, xPos, -yPos, yPos, zPos, zPos);

            // Gets objects from pool
            obst = ObjectPooler.sharedInstance.GetPooledObject();
            // Sets size, euler angles, position, color, and active
            if (obst != null)
            {
                if (op.canBeLarge)
                    obst.transform.localScale = SpecialObstacleSize();
                else
                    size = Randomizer(xMinSize, xMaxSize, yMinSize, yMaxSize, zMinSize, zMaxSize);

                obst.transform.eulerAngles = SpecialObstacleRotation(op.randPooledObj);
                obst.transform.position = position;
                obst.GetComponent<MeshRenderer>().material.color = ObstColor();

                obst.SetActive(true);
            }
        }

        #region Side Obstacles
        //zone = Random.Range(0, 2);
        /*timeAllotted = Random.Range(0.01f, 0.02f);
        if (timer > 0)
        {
            // decrease timer
            timer -= Time.deltaTime;
        }
        else
        {
            timer = timeAllotted;
            //position = Randomizer(-xPos, xPos, -yPos, yPos, zPos, zPos);

            obst = ObjectPooler.sharedInstance.GetPooledObject();
            if (obst != null)
            {
                if (op.canBeLarge)
                {
                    // have position go on all 4 directions around player
                    //obst.transform.position = new Vector3(100f, 0f, 500f);
                    SideObstacles(obst, zone);
                    obst.transform.localScale = SpecialObstacleSize();
                }
                else
                {
                    //obst.transform.position = position;
                    size = Randomizer(xMinSize, xMaxSize, yMinSize, yMaxSize, zMinSize, zMaxSize);
                }
                obst.transform.eulerAngles = SpecialObstacleRotation(op.randPooledObj);

                // sets color
                obst.GetComponent<MeshRenderer>().material.color = ObstColor();
                // sets up color of light
                lightColor = ObstColor();
                light.color = new Color(lightColor.r + 0.050f, lightColor.g + 0.050f, lightColor.b + 0.050f, lightColor.a);

                obst.SetActive(true);
            }
        }*/
        #endregion
    }
}
