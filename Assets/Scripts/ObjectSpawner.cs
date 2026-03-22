using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    // Reference to the Prefab you want to spawn (assign this in the Inspector)
    public GameObject objectToSpawnPrefab;
    // Reference to the AR Session Origin (assign this in the Inspector)
    public ARSessionOrigin arSessionOrigin;

    // A public function that the UI button can call
    public void SpawnObjectAtScreenCenter()
    {
        // Get the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        
        // Raycast from the center of the screen into the AR world
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arSessionOrigin.GetComponent<ARRaycastManager>().Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            // If the raycast hits an AR plane, instantiate the object at that position and rotation
            var hitPose = hits[0].pose;
            Instantiate(objectToSpawnPrefab, hitPose.position, hitPose.rotation);
        }
        else
        {
            Debug.Log("Did not hit an AR plane with the screen center raycast.");
        }
    }
}
