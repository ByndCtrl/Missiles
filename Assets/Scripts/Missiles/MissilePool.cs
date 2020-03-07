using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MissilePool : MonoBehaviour
{
    [SerializeField] private GameObject missile = null;
    [SerializeField] private GameObject VFXDestroy = null;
    [SerializeField] private GameObject VFXCollision = null;

    private Queue<GameObject> availableMissiles = new Queue<GameObject>();
    private Queue<GameObject> availableVFXDestroy = new Queue<GameObject>();
    private Queue<GameObject> availableVFXCollision = new Queue<GameObject>();

    private int missileAmount = 64;
    private int destroyVFXAmount = 64;
    private int collisionVFXAmount = 64;

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
            AddMissileToPool(instancesToAdd);
        }
    }

    private void GrowDestroyPool()
    {
        for (int i = 0; i < destroyVFXAmount; i++)
        {
            var instancesToAdd = Instantiate(VFXDestroy) as GameObject;
            instancesToAdd.transform.SetParent(transform);
            instancesToAdd.gameObject.SetActive(false);
            AddDestroyVFXToPool(instancesToAdd);
        }
    }

    private void GrowCollisionPool()
    {
        for (int i = 0; i < collisionVFXAmount; i++)
        {
            var instanceToAdd = Instantiate(VFXCollision) as GameObject;
            instanceToAdd.transform.SetParent(transform);
            instanceToAdd.gameObject.SetActive(false);
            AddCollisionVFXToPool(instanceToAdd);
        }
    }

    public GameObject GetMissile()
    {
        if (availableMissiles.Count == 0)
        {
            GrowMissilePool();
        }

        var instance = availableMissiles.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public GameObject GetDestroyVFX()
    {
        if (availableVFXDestroy.Count == 0)
        {
            GrowDestroyPool();
        }

        var instance = availableVFXDestroy.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public GameObject GetCollisionVFX()
    {
        if (availableVFXCollision.Count == 0)
        {
            GrowCollisionPool();
        }

        var instance = availableVFXCollision.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public void AddMissileToPool(GameObject instance)
    {
        availableMissiles.Enqueue(instance);
    }

    public void AddDestroyVFXToPool(GameObject instance)
    {
        instance.GetComponent<VisualEffect>().Stop();
        instance.gameObject.SetActive(false);
        availableVFXDestroy.Enqueue(instance);
    }

    public void AddCollisionVFXToPool(GameObject instance)
    {
        instance.GetComponent<VisualEffect>().Stop();
        instance.gameObject.SetActive(false);
        availableVFXCollision.Enqueue(instance);
    }

    private void OnDestroy()
    {
        availableMissiles.Clear();
        availableVFXCollision.Clear();
        availableVFXDestroy.Clear();
    }
}