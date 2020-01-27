using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab = null;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static MissilePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 64; i++)
        {
            var instancesToAdd = Instantiate(missilePrefab) as GameObject;
            instancesToAdd.transform.SetParent(transform);
            instancesToAdd.gameObject.SetActive(false);
            AddToPool(instancesToAdd);
        }
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
            GrowPool();

        var Instance = availableObjects.Dequeue();
        Instance.SetActive(true);
        return Instance;
    }

    public void AddToPool(GameObject Instance)
    {
        availableObjects.Enqueue(Instance);
    }
}