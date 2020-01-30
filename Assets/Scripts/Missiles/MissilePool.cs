using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class MissilePool : MonoBehaviour
{
    [SerializeField] private GameObject missile = null;
    [SerializeField] private GameObject VFXDestroy = null;
    [SerializeField] private GameObject VFXCollision = null;

    private Queue<GameObject> availableMissiles = new Queue<GameObject>();
    private Queue<GameObject> availableVFXDestroy = new Queue<GameObject>();
    private Queue<GameObject> availableVFXCollision = new Queue<GameObject>();

    private int missileAmount = 64;
    private int VFXDestroyAmount = 64;
    private int VFXCollisionAmount = 64;

    public static MissilePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        GrowMissilePool();
        GrowDestroyPool();
        GrowCollisionPool();
    }

    private void GrowMissilePool()
    {
        for (int i = 0; i < missileAmount; i++)
        {
            var instancesToAdd = Instantiate(missile) as GameObject;
            instancesToAdd.transform.SetParent(transform);
            instancesToAdd.gameObject.SetActive(false);
            AddToMissilePool(instancesToAdd);
        }
    }

    private void GrowDestroyPool()
    {
        for (int i = 0; i < VFXDestroyAmount; i++)
        {
            var instancesToAdd = Instantiate(VFXDestroy) as GameObject;
            instancesToAdd.transform.SetParent(transform);
            instancesToAdd.gameObject.SetActive(false);
            AddToVFXDestroyPool(instancesToAdd);
        }
    }

    private void GrowCollisionPool()
    {
        for (int i = 0; i < VFXCollisionAmount; i++)
        {
            var instanceToAdd = Instantiate(VFXCollision) as GameObject;
            instanceToAdd.transform.SetParent(transform);
            instanceToAdd.gameObject.SetActive(false);
            AddToVFXCollisionPool(instanceToAdd);
        }
    }

    public GameObject GetMissileFromPool()
    {
        if (availableMissiles.Count == 0)
        {
            GrowMissilePool();
        }

        var instance = availableMissiles.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public GameObject GetVFXDestroyFromPool()
    {
        if (availableVFXDestroy.Count == 0)
        {
            GrowDestroyPool();
        }

        var instance = availableVFXDestroy.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public GameObject GetVFXCollisionFromPool()
    {
        if (availableVFXCollision.Count == 0)
        {
            GrowCollisionPool();
        }

        var instance = availableVFXCollision.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public void AddToMissilePool(GameObject instance)
    {
        availableMissiles.Enqueue(instance);
    }

    public void AddToVFXDestroyPool(GameObject instance)
    {
        instance.GetComponent<VisualEffect>().Stop();
        instance.gameObject.SetActive(false);
        availableVFXDestroy.Enqueue(instance);
    }

    public void AddToVFXCollisionPool(GameObject instance)
    {
        instance.GetComponent<VisualEffect>().Stop();
        instance.gameObject.SetActive(false);
        availableVFXCollision.Enqueue(instance);
    }
}