using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class HitObjectManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private GameObject spawnedObject;
    private ARRaycastManager arCameraManager;
    private Vector2 touchPosition;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arCameraManager = GetComponent<ARRaycastManager>();
    }

    private bool CanGetTouchPosition(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    private void Update()
    {
        if (!CanGetTouchPosition(out touchPosition))
            return;
        else if(arCameraManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hit = hits[0].pose;

            if (spawnedObject == null)
                spawnedObject = Instantiate(prefab, hit.position, hit.rotation);
            else
                spawnedObject.transform.position = hit.position;
        }
    }
}
