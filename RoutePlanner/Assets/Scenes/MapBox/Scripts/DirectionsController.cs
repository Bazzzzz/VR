using Mapbox.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Json;
using Mapbox.Directions;
using Mapbox.Utils;
using Mapbox.Utils.JsonConverters;
using Mapbox.Geocoding;

public class DirectionsController : MonoBehaviour {

    [SerializeField]
    Text _resultsText;

    [SerializeField]
    ForwardGeocodeUserInput _startLocationGeocoder;

    [SerializeField]
    ForwardGeocodeUserInput _endLocationGeocoder;

    Directions _directions;

    Vector2d[] _coordinates;

    DirectionResource _directionResource;

    public WayPointController wayPointController;
    void Start()
    {
        Debug.Log(MapboxAccess.Instance.Directions.ToString());
        _directions = MapboxAccess.Instance.Directions;
        _startLocationGeocoder.OnGeocoderResponse += StartLocationGeocoder_OnGeocoderResponse;
        _endLocationGeocoder.OnGeocoderResponse += EndLocationGeocoder_OnGeocoderResponse;

        _coordinates = new Vector2d[2];

        // Can we make routing profiles an enum?
        _directionResource = new DirectionResource(_coordinates, RoutingProfile.Driving);
        _directionResource.Steps = true;
    }

    void OnDestroy()
    {
        if (_startLocationGeocoder != null)
        {
            _startLocationGeocoder.OnGeocoderResponse -= StartLocationGeocoder_OnGeocoderResponse;
        }

        if (_startLocationGeocoder != null)
        {
            _startLocationGeocoder.OnGeocoderResponse -= EndLocationGeocoder_OnGeocoderResponse;
        }
    }

    /// <summary>
    /// Start location geocoder responded, update start coordinates.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void StartLocationGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
    {
        Debug.Log("StartLocationGeoCoder: " + response.Type);
        _coordinates[0] = _startLocationGeocoder.Coordinate;
        if (ShouldRoute())
        {
            Debug.Log("ShouldRoute = True");
            Route();
        }
    }

    /// <summary>
    /// End location geocoder responded, update end coordinates.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    void EndLocationGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
    {
        Debug.Log("StartLocationGeoCoder: " + response.Type);
        _coordinates[1] = _endLocationGeocoder.Coordinate;
        if (ShouldRoute())
        {
            Debug.Log("ShouldRoute = True");
            Route();
        }
    }

    /// <summary>
    /// Ensure both forward geocoders have a response, which grants access to their respective coordinates.
    /// </summary>
    /// <returns><c>true</c>, if both forward geocoders have a response, <c>false</c> otherwise.</returns>
    bool ShouldRoute()
    {
        Debug.Log("Checking if route should be calculated");
        return _startLocationGeocoder.HasResponse && _endLocationGeocoder.HasResponse;
    }

    /// <summary>
    /// Route 
    /// </summary>
    void Route()
    {
        Debug.Log("Route: " + _coordinates.Length);
        _directionResource.Coordinates = _coordinates;
        _directions.Query(_directionResource, HandleDirectionsResponse);
    }
    /// <summary>
    /// Log directions response to UI.
    /// </summary>
    /// <param name="res">Res.</param>
    void HandleDirectionsResponse(DirectionsResponse res)
    {
        if (res == null)
        {
            Debug.Log("Response is null");
        }
        if (res != null)
        {
            Debug.Log("Response is NOT null");

            if (wayPointController != null)
            {
                Debug.Log("WayPoint Controller is NOT null");
                
                wayPointController.GetComponent<WayPointController>().InstantiateController(res);
            }

            //var data = JsonConvert.SerializeObject(res, Formatting.Indented, JsonConverters.Converters);
            //Debug.Log(data.ToString());
            //string sub = data.Substring(0, data.Length > 5000 ? 5000 : data.Length) + "\n. . . ";

            //_resultsText.text = sub;
        }
    }
}
