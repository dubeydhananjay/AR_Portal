using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PortalUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject placementButton;  // The "Place Portal" button
    [SerializeField] private TextMeshProUGUI statusText;  // Top-level instructions

    public void UpdateUIState(bool floorDetected, bool portalAlreadyPlaced)
    {
        if (portalAlreadyPlaced)
        {
            placementButton.SetActive(false);
            statusText.text = "Portal Active. Walk through to enter!";
            return;
        }

        if (floorDetected)
        {
            placementButton.SetActive(true);
            statusText.text = "Floor found! Tap to spawn.";
        }
        else
        {
            placementButton.SetActive(false);
            statusText.text = "Move your Device to find the ground...";
        }
    }
}