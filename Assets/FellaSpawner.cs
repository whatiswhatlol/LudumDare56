using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FellaSpawner : MonoBehaviour
{
    public static FellaSpawner Instance { get; private set; }  // Singleton instance

    public enum ColorType { Yellow, Green, Blue, Purple, Red }  // Enum for color selection
    public ColorType selectedColor = ColorType.Yellow;  // Default selected color

    [Header("Fellas Count")]
    public int yellowFellas = 1;
    public int greenFellas = 1;
    public int blueFellas = 1;
    public int purpleFellas = 1;
    public int redFellas = 1;

    [Header("Prefab References")]
    public GameObject yellowPrefab;
    public GameObject greenPrefab;
    public GameObject bluePrefab;
    public GameObject purplePrefab;
    public GameObject redPrefab;
    

    NumberOfFellasUpdater NumberOfFellasUpdater;


    public List<GameObject> fellas;
    public List<FellaStats> fellaStats;

    private void Awake()
    {
        // Ensure there's only one instance of this singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleScrollWheelSelection();  // Handle color selection with scroll wheel
        HandleNumberKeySelection();    // Handle color selection with number keys
        HandleInstantiation();
    }

    // Handle instantiating the selected prefab when clicking the mouse button
    private void HandleInstantiation()
    {
        if (Input.GetMouseButtonDown(0))  // Left-click
        {
            InstantiateColorPrefab();
        }
    }

    // Function to instantiate the correct prefab based on the selected color and available fellas
    public void InstantiateColorPrefab()
    {
        Vector3 playerPosition = transform.position;  // Assuming you have a PlayerManager Singleton

        // Instantiate based on selected color and fellas count
        switch (selectedColor)
        {
            case ColorType.Yellow:
                if (yellowFellas > 0)
                {
                    GameObject temp = Instantiate(yellowPrefab, playerPosition, Quaternion.identity);
                    yellowFellas--;  // Decrease fellas count
                    fellas.Add(temp);
                    fellaStats.Add(temp.GetComponent<FellaStats>());
                }
                break;
            case ColorType.Green:
                if (greenFellas > 0)
                {
                    GameObject temp = Instantiate(greenPrefab, playerPosition, Quaternion.identity);
                    greenFellas--;
                    fellas.Add(temp);
                    fellaStats.Add(temp.GetComponent<FellaStats>());

                }
                break;
            case ColorType.Blue:
                if (blueFellas > 0)
                {
                    GameObject temp = Instantiate(bluePrefab, playerPosition, Quaternion.identity);
                    blueFellas--;
                    fellas.Add(temp);
                    fellaStats.Add(temp.GetComponent<FellaStats>());

                }
                break;
            case ColorType.Purple:
                if (purpleFellas > 0)
                {
                    GameObject temp = Instantiate(purplePrefab, playerPosition, Quaternion.identity);
                    purpleFellas--;
                    fellas.Add(temp);
                    fellaStats.Add(temp.GetComponent<FellaStats>());

                }
                break;
            case ColorType.Red:
                if (redFellas > 0)
                {
                    GameObject temp = Instantiate(redPrefab, playerPosition, Quaternion.identity);
                    redFellas--;
                    fellas.Add(temp);
                    fellaStats.Add(temp.GetComponent<FellaStats>());

                }
                break;
        }
        updateNumbers();
    }

    // Handle color selection with the scroll wheel
    private void HandleScrollWheelSelection()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)  // Scroll up
        {
            CycleColorBackward();
        }
        else if (scroll < 0f)  // Scroll down
        {
            CycleColorForward();
        }
    }

    // Cycle through the colors in forward direction
    private void CycleColorForward()
    {
        selectedColor++;

        // If we reach past Red, loop back to Yellow
        if ((int)selectedColor > (int)ColorType.Red)
        {
            selectedColor = ColorType.Yellow;
        }

    }

    // Cycle through the colors in backward direction
    private void CycleColorBackward()
    {
        selectedColor--;

        // If we go before Yellow, loop back to Red
        if ((int)selectedColor < (int)ColorType.Yellow)
        {
            selectedColor = ColorType.Red;
        }

    }

    // Handle color selection with number keys
    private void HandleNumberKeySelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Press '1' key
        {
            selectedColor = ColorType.Yellow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // Press '2' key
        {
            selectedColor = ColorType.Green;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))  // Press '3' key
        {
            selectedColor = ColorType.Blue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))  // Press '4' key
        {
            selectedColor = ColorType.Purple;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))  // Press '5' key
        {
            selectedColor = ColorType.Red;
        }

    }



    public void setNumberUpdater(NumberOfFellasUpdater upd)
    {
        NumberOfFellasUpdater = upd;
    }

    private void updateNumbers()
    {
        NumberOfFellasUpdater.UpdateNumberOfFellas();
    }




}
