using System.Collections.Generic;
using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    public static BoostSpawner sharedInstance;
    [SerializeField]
    private Boost bst;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountInPool;

    [SerializeField]
    private GameObject boost;

    private float xPos;
    private float yPos;
    private float zPos;

    public int boostCounter;

    private float timer;
    private float timeAllotted;

    GameObject obj;

    private float xRandPos;
    private float yRandPos;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void ObjectPool(GameObject objectPool, int amountInPool)
    {
        for (int ii = 0; ii < amountInPool; ii++)
        {
            obj = (GameObject)Instantiate(objectPool);
            obj.SetActive(false);
            obj.transform.position = new Vector3(transform.position.x + xRandPos, transform.position.y + yRandPos, transform.position.z + zPos);
            pooledObjects.Add(obj);
        }
    }
    private void Start()
    {
        timer = timeAllotted;
        xPos = 7f;
        yPos = 3f;
        zPos = 500f;
        ObjectPool(boost, 3);
    }

    public GameObject GetPooledObject()
    {
        for (int ii = 0; ii < pooledObjects.Count; ii++)
        {
            if (!pooledObjects[ii].activeInHierarchy)
            {
                return pooledObjects[ii];
            }
        }
        return null;
    }

    void Update()
    {
        xRandPos = Random.Range(-xPos, xPos);
        yRandPos = Random.Range(-yPos, yPos);
        Vector3 position;
        if (CheckOrientation.portraitMode)
            position = new Vector3(yRandPos, xRandPos, zPos);
        else
            position = new Vector3(xRandPos, yRandPos, zPos);

        timeAllotted = Random.Range(5f, 25f);


        if (timer > 0)
        {
            // decrease timer
            timer -= Time.deltaTime;
        }
        else
        {
            // if there is 1 boost in game and player has 3 boosts, don't spawn anymore
            if (boostCounter <= 1 && bst.boostsPlayerHas < 3)
            {
                // when timer ends, spawn a boost
                timer = timeAllotted;
                GameObject obj = sharedInstance.GetPooledObject();
                if (obj != null)
                {
                    obj.transform.position = position;
                    obj.SetActive(true);
                    boostCounter++;
                }
            }
        }
    }
}
