
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PortalCameraLogic : MonoBehaviour
{
    [Header("Portal Settings")]
    [SerializeField] private List<Material> interiorMaterials = new List<Material>();

    private const string PORTAL_DOOR = "PortalDoor";
    private bool isInside = false;

    private void Start()
    {
        // Mask the environment at the start of the app
        SetStencilFunction(CompareFunction.Equal);
    }

    //the doorway of the portal is the mask and when the camera and mask intersect trigger is activated
    //once trigger is activated all the shader masked behind the doorway will have "Not Equal" comparison in stencil shader
    //i.e all the gameobjects will be drawn or not be masked behind the doorway
    //And when user again goes through the portal, again the trigger activates and mask the environment by setting the "Equal" comparison.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PORTAL_DOOR)) return;

        isInside = !isInside;
        if(isInside) {
            SetStencilFunction(CompareFunction.NotEqual);
        }
        else {
            SetStencilFunction(CompareFunction.Equal);
        }
    }

    private void SetStencilFunction(CompareFunction func)
    {
        foreach (var mat in interiorMaterials)
        {
            if (mat != null)
            {
                // Matches the [Enum] property name in your Specular Stencil shader
                mat.SetInt("_StencilTest", (int)func);
            }
        }
    }
}