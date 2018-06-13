using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Directions;
using Mapbox.Unity.Map;

public class WayPointController : MonoBehaviour {

    private List<Waypoint> waypointsToSpawn;
    private List<Waypoint> waypointsSpawned;
    private List<Route> routes;


    public AbstractMap map;
    public LineRenderer lineRenderer;
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
        if (map != null)
        {
            Debug.Log("Map is NOT NULL");
            if (waypointsToSpawn != null && waypointsToSpawn.Count > 0 && (waypointsSpawned.Count != waypointsToSpawn.Count))
            {
                Debug.Log("Waypoints to spawn:" + waypointsToSpawn.Count);


                //map.GetComponent<AbstractMap>()
                //if (map.GetComponent<>)
                foreach (Waypoint waypoint in waypointsToSpawn)
                {
                    if (!waypointsSpawned.Contains(waypoint))
                    {
                        Debug.Log("Waypoint to Render | Name: " + waypoint.Name + " - Latitude: " + waypoint.Location.x + " - Longitude: " + waypoint.Location.y);
                        //map.SpawnPrefabAtGeoLocation(waypointPrefab, new Vector2d(waypoint.Location.x, waypoint.Location.y), locationItemName: waypoint.Name);

                        Vector3 point3D = map.GeoToWorldPosition(new Vector2d(waypoint.Location.x, waypoint.Location.y), true);
                        Debug.Log("Waypoint position 3D: " + point3D);

                        var instance = Instantiate(waypointPrefab);
                        instance.transform.localPosition = point3D;


                        waypointsSpawned.Add(waypoint);
                        Debug.Log("WayPointsToSpawn: " + waypointsToSpawn.Count + " |  WayPointsSpawned: " + waypointsSpawned.Count);


                    }
                }
            }

            if (routes != null && routes.Count > 0)
            {
                Debug.Log("Routes to spawn:" + routes.Count);

                if (map != null)
                {
                    List<Vector3> routePoints = new List<Vector3>();
                    foreach (Route route in routes)
                    {
                        //Debug.Log("Route to Render | Distance: " + route.Distance + " | Legs amount: " + route.Legs.Count);

                        foreach (Leg leg in route.Legs)
                        {
                            //Debug.Log("Steps in Leg: " + leg.Steps.Count);
                            foreach (Step step in leg.Steps)
                            {
                                foreach (Vector2d point in step.Geometry)
                                {
                                    Vector3 point3D = map.GeoToWorldPosition(point);
                                    //Debug.Log("Point 3D: " + point3D + " for Step: " + step.Name + " (" + step.Maneuver + ")");
                                    routePoints.Add(point3D);
                                }
                            }
                        }
                    }
                    if (routePoints != null && routePoints.Count > 0)
                    {
                        lineRenderer.SetPositions(routePoints.ToArray());
                        lineRenderer.positionCount = routePoints.Count;
                    }
                }
            }
        }
    }
    public void InstantiateController(DirectionsResponse res)
    {
        Debug.Log("Instantiate waypoint | Count: " + res.Waypoints.Count);

        this.waypointsToSpawn = res.Waypoints;

        Debug.Log("Instantiate routes | Count: " + res.Routes.Count);

        this.routes = res.Routes;
        
    }
}
