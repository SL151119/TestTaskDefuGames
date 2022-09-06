using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private List<Transform> spawnPoints;

    [Header("Peoples To Spawn")]
    [SerializeField] private List<GameObject> objectsToSpawn;

    [Header("Holde Peoples")]
    [SerializeField] private Transform objectsHolder;

    [Header("Quantity Peoples To Spawn")]
    [SerializeField] private int minimumObjectsToSpawn;
    [SerializeField] private int maximumObjectsToSpawn;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int objectsCount = Random.Range(minimumObjectsToSpawn, maximumObjectsToSpawn); //generation of the random number of spawn points

        foreach (var item in _spawnedObjects) // removing objects that were spawned in advance
        {
            Destroy(item);
        }
        List<Transform> selectedSpawnPoints = new List<Transform>(); // create empty list for random points
        foreach (var item in spawnPoints) // try all points
        {
            if (selectedSpawnPoints.Count < objectsCount) //if the randomly selected points are less than the maximum number of points ->
            {
                var allAbleSpawnPoints = spawnPoints.Where(x => !selectedSpawnPoints.Contains(x)).ToList(); //create a list with available points.
                                                                                                            //select points by condition if the list with random points does not contain the current point.

                var currentPoint = allAbleSpawnPoints[Random.Range(0, allAbleSpawnPoints.Count)]; //take a random point from the available
                selectedSpawnPoints.Add(currentPoint); //add random point in list
                GameObject currentObject = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], objectsHolder); //create people in parent holder
                _spawnedObjects.Add(currentObject); // add people to the list of spawned 
                currentObject.transform.position = currentPoint.position; // set the current people to the position of our point
            }
        }
    }
}
