using UnityEngine;

public class SelectionUpdater : MonoBehaviour
{
    FellaSpawner fellaSpawner;
    RectTransform rectTransform;

    void Start()
    {
        fellaSpawner = FellaSpawner.Instance;
        rectTransform = GetComponent<RectTransform>();  // Get the RectTransform of the UI element
    }

    void Update()
    {
        UpdateSelectionPosition();
    }

    // Function to update the position of the UI image based on the selected color
    private void UpdateSelectionPosition()
    {
        Vector3 newPosition = Vector3.zero;  // Default position

        // Set the new position based on the selected color
        if (fellaSpawner.selectedColor == FellaSpawner.ColorType.Yellow)
        {
            newPosition = new Vector3(-234, 0, 0);
        }
        else if (fellaSpawner.selectedColor == FellaSpawner.ColorType.Green)
        {
            newPosition = new Vector3(-117, 0, 0);
        }
        else if (fellaSpawner.selectedColor == FellaSpawner.ColorType.Blue)
        {
            newPosition = new Vector3(0, 0, 0);
        }
        else if (fellaSpawner.selectedColor == FellaSpawner.ColorType.Purple)
        {
            newPosition = new Vector3(117, 0, 0);
        }
        else if (fellaSpawner.selectedColor == FellaSpawner.ColorType.Red)
        {
            newPosition = new Vector3(234, 0, 0);
        }

        // Update the RectTransform position to move the UI element
        rectTransform.anchoredPosition = newPosition;
    }
}
