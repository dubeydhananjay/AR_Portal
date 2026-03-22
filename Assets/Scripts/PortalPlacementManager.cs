using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PortalPlacementManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PortalUIController uiController;

    private ARRaycastManager raycastManager;
    private GameObject spawnedPortal;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake() => raycastManager = GetComponent<ARRaycastManager>();

    // Get the trackables plane with ar raycastManager and store ar raycast hits
    // the source point of ray would be (Screen.width / 2, Screen.height / 2)
    //we will update the text based on the raycast hits and planes found.
    void Update()
    {
        if (raycastManager == null) return;
        if (spawnedPortal != null) return;

        var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        bool hitFound = raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        uiController.UpdateUIState(hitFound, spawnedPortal != null);
    }

    //Once we have hits, we will place the portal on the first hit we got
    //this method is added to "Place Portal" button event in UI
    public void TryPlacePortal()
    {
        if (hits.Count > 0 && spawnedPortal == null)
        {
            spawnedPortal = Instantiate(raycastManager.raycastPrefab, hits[0].pose.position, hits[0].pose.rotation, null);
            spawnedPortal.AddComponent<ARAnchor>();
            uiController.UpdateUIState(false, true);
        }
    }
}