using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARRaycastController : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject objectToPlace;
    private Vector3 surfaceNormal;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (objectToPlace != null)
            {
                objectToPlace.transform.position = hitPose.position;
                objectToPlace.transform.rotation = hitPose.rotation;
                objectToPlace.SetActive(true);
                surfaceNormal = hits[0].pose.up;
                objectToPlace.transform.rotation = Quaternion.LookRotation(surfaceNormal);
            }
        }
    }
}
