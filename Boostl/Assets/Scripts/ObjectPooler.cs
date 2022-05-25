using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountInPool;

    [SerializeField]
    private GameObject obstacle0;
    [SerializeField]
    private GameObject obstacle1;
    [SerializeField]
    private GameObject obstacle2;
    [SerializeField]
    private GameObject obstacle3;
    [SerializeField]
    private GameObject obstacle4;
    [SerializeField]
    private GameObject obstacle5;

    GameObject obj;
    public bool canBeLarge;
    public int randPooledObj;

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
            pooledObjects.Add(obj);
        }
    }
    private void Start()
    {
        ObjectPool(obstacle0, 20);
        ObjectPool(obstacle1, 20);
        ObjectPool(obstacle2, 20);
        ObjectPool(obstacle3, 20);
        ObjectPool(obstacle4, 20);
        ObjectPool(obstacle5, 20);
    }

    public GameObject GetPooledObject()
    {
        for (int ii = 0; ii < pooledObjects.Count; ii++)
        {
            if (!pooledObjects[ii].activeInHierarchy)
            {
                randPooledObj = Random.Range(0, pooledObjects.Count);
                if (randPooledObj >= 21 && randPooledObj <= 80)
                    canBeLarge = true;
                else
                    canBeLarge = false;
                return pooledObjects[randPooledObj];
            }
        }
        return null;
    }
}
