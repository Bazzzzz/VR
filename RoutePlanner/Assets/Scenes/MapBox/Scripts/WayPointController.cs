using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Directions;
using Mapbox.Unity.Map;

public class WayPointController : MonoBehaviour {

    private List<Waypoint> waypointsToSpawn;
    private List<Waypoint> waypointsSpawned;

    public AbstractMap map;

    public GameObject waypointPrefab;

    void Start()
    {
        waypointsSpawned = new List<Waypoint>();
    }
    void Update()
    {
        if (waypointsToSpawn == null || waypointsToSpawn.Count == 0)
        {
            //Debug.Log("No waypoints to spawn");
        }
        if (waypointsToSpawn != null && waypointsToSpawn.Count > 0 && (waypointsSpawned.Count != waypointsToSpawn.Count))
        {
            Debug.Log("Waypoints to spawn:" + waypointsToSpawn.Count);

            if (map != null)
            {
                Debug.Log("Map is NOT NULL");
                //map.GetComponent<AbstractMap>()
                //if (map.GetComponent<>)
                foreach(Waypoint waypoint in waypointsToSpawn)
                {
                    if (!waypointsSpawned.Contains(waypoint))
                    {
                        Debug.Log("Waypoint to Render | Name: " + waypoint.Name + " - Latitude: " + waypoint.Location.x + " - Longitude: " + waypoint.Location.y);
                        map.SpawnPrefabAtGeoLocation(waypointPrefab, new Vector2d(waypoint.Location.x, waypoint.Location.y), locationItemName: waypoint.Name);
                       

                        waypointsSpawned.Add(waypoint);
                        Debug.Log("WayPointsToSpawn: " + waypointsToSpawn.Count + " |  WayPointsSpawned: " + waypointsSpawned.Count);
                    }
                }
            }
        }
    }
    public void InstantiateWaypoints(List<Waypoint> waypoints)
    {
        Debug.Log("Instantiate waypoint | Count: " + waypoints.Count);

        this.waypointsToSpawn = waypoints;
    }
}
