using UnityEngine;
using UnityEngine.Rendering;

public class PortalCameraLogic : MonoBehaviour
{
    // List of all materials used INSIDE the portal
    [SerializeField] private Material[] interiorMaterials;

    // Called when the camera enters the Doorway trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortalDoor"))
        {
            SetStencilFunction(CompareFunction.Always);
        }
    }

    // Called when the camera exits the Doorway trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PortalDoor"))
        {
            // Use the position to determine if we exited to the 'outside' or 'inside'
            // For simplicity, we check if the camera is in front or behind the door
            Vector3 relativePos = other.transform.InverseTransformPoint(transform.position);

            if (relativePos.z < 0) 
            {
                // We are outside
                SetStencilFunction(CompareFunction.Equal);
            }
            else 
            {
                // We are inside
                SetStencilFunction(CompareFunction.Always);
            }
        }
    }

    private void SetStencilFunction(CompareFunction func)
    {
        foreach (var mat in interiorMaterials)
        {
            // "_StencilComp" is the standard property name for the Stencil Comparison
            mat.SetInt("_StencilComp", (int)func);
        }
    }
}