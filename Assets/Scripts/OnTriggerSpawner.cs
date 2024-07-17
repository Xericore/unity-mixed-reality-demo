using System.Collections.Generic;
using UnityEngine;

public class OnTriggerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;

    private readonly List<GameObject> _spawnedObjects = new();

    /// <summary>
    /// Multiple colliders will trigger OnTriggerEnter multiple times.
    /// </summary>
    private int _lastInstantiatedFrame;

    private void OnTriggerEnter(Collider other)
    {
        if (_lastInstantiatedFrame == Time.frameCount)
        {
            return;
        }

        var collidableObject = other.GetComponent<CollidableObject>();

        if (collidableObject == null)
        {
            return;
        }

        _spawnedObjects.Add(objectToSpawn);
        InstantiateAsync(objectToSpawn, collidableObject.transform.position, Quaternion.identity);
        _lastInstantiatedFrame = Time.frameCount;
    }

    public void DeleteAllObjects()
    {
        foreach (var spawnedObject in _spawnedObjects)
        {
            Destroy(spawnedObject);
        }

        _spawnedObjects.Clear();
    }
}