using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PortalPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab; // Assign your Portal Prefab here
    [SerializeField] private GameObject placementIndicator; // Optional: A visual marker for where the portal will land

    private ARRaycastManager raycastManager;
    private GameObject spawnedPortal;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // This method should be called by your "Place Portal" UI Button
    public void TryPlacePortal()
    {
        // Shoot a ray from the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (spawnedPortal == null)
            {
                // 1. Spawn the portal
                spawnedPortal = Instantiate(portalPrefab, hitPose.position, hitPose.rotation);
            }

            // // 2. Calculate direction from Portal to Camera
            // Vector3 directionToCamera = Camera.main.transform.position - spawnedPortal.transform.position;

            // // 3. Keep it upright (ignore vertical tilt)
            // directionToCamera.y = 0;

            // // 4. Point the Portal's FORWARD at the camera
            // // If it still looks backwards, use: -directionToCamera
            // spawnedPortal.transform.rotation = Quaternion.LookRotation(directionToCamera);

            // 5. Explicitly move it if it already existed
            if (spawnedPortal != null)
            {
                spawnedPortal.transform.position = hitPose.position;
            }
        }
    }
}