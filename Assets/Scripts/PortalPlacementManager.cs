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
                // Spawn the portal at the hit position, facing the camera
                spawnedPortal = Instantiate(portalPrefab, hitPose.position, hitPose.rotation);
                
                // Ensure the portal faces the user
                Vector3 lookPos = Camera.main.transform.position - spawnedPortal.transform.position;
                lookPos.y = 0; // Keep the portal upright
                spawnedPortal.transform.rotation = Quaternion.LookRotation(lookPos);
            }
            else
            {
                // Reposition the portal if it already exists
                spawnedPortal.transform.position = hitPose.position;
            }
        }
    }
}