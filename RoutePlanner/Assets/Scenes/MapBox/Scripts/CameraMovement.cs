using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Wrld;
using Wrld.Space;

public class CameraMovement : MonoBehaviour {

    private GameObject waypointController;

    private int index, maxIndex;
    private List<Vector3> waypoint3DPoints;
    private List<Vector2d> waypoint2DPoints;
    // Use this for initialization
    void Start()
    {
        waypointController = GameObject.Find("WaypointController");

        if (waypointController != null && waypointController.GetComponent<WayPointController>() != null
            && waypointController.GetComponent<WayPointController>().GetStartWaypoint() != null
            && waypointController.GetComponent<WayPointController>().GetRoute2DPoints() != null)
        {
            Debug.Log("Camera Movement Start WaypointController found.");
            //waypoint3DPoints = waypointController.GetComponent<WayPointController>().GetRoute3DPoints();
            //Debug.Log(waypoint3DPoints[0]);
            //Camera.main.transform.position = waypoint3DPoints[0];
            //maxIndex = waypoint3DPoints.Count;
            //Debug.Log("maxindex: " + maxIndex);
            index = 0;

            waypoint2DPoints = waypointController.GetComponent<WayPointController>().GetRoute2DPoints();
            var startLocation = LatLong.FromDegrees(waypoint2DPoints[0].x, waypoint2DPoints[0].y);
            //var startLocation = LatLong.FromECEF(waypoint3DPoints[0]);
            //Debug.Log("Camera start point: " + waypoint3DPoints[0]);
            maxIndex = waypoint2DPoints.Count;

            Api.Instance.CameraApi.MoveTo(startLocation, distanceFromInterest: 800, headingDegrees: 0, tiltDegrees: 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Camera Movement Update.");
        if (Input.GetKey("a"))
        {
            if (
                //waypoint3DPoints != null && waypoint3DPoints.Count > 0
                waypoint2DPoints != null && waypoint2DPoints.Count > 0
                )
            {
                Debug.Log("Camera Movement Update waypoint2DPoints found.");

                if (index < maxIndex)
                {
                    index++;
                    //Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(waypoint3DPoints[index]);
                    //Debug.Log("Index (" + index + " / " + maxIndex + ") 3D Point: " + waypoint3DPoints[index]);

                    // Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, viewportPoint, 5f);
                    Debug.Log("Index (" + index + " / " + maxIndex + ") 2D Point: " + waypoint2DPoints[index]);

                    var destLocation = LatLong.FromDegrees(waypoint2DPoints[index].x, waypoint2DPoints[index].y);
                   // var destLocation = LatLong.FromECEF(waypoint3DPoints[index]);
                    Debug.Log("Camera destination point: " + destLocation);

                    Api.Instance.CameraApi.AnimateTo(destLocation, distanceFromInterest: 500, transitionDuration: 5);
                }
            }
        }
    }
}
