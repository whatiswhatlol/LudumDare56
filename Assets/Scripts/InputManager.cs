using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode UpgradeKey = KeyCode.Q;
    public KeyCode RestKey = KeyCode.E;
    FellaSpawner FellaSpawner;
    FellaStateManager FellaStateManager;
    public ActivesManager ActivesManager;
    private void Start()
    {
        FellaSpawner = FellaSpawner.Instance;
        FellaStateManager = FellaStateManager.Instance;
    }
    private void Update()
    {
        if(Input.GetKeyDown(UpgradeKey))
        {
            if (FellaSpawner.selectedColor == FellaSpawner.ColorType.Red)
            {
                //NO
                return;
            }
            else
            {
                FellaSpawner.subtractFromColor(FellaSpawner.selectedColor, 1);
                FellaStateManager.AssignFirstToTrain(FellaSpawner.selectedColor);
                ActivesManager.StartTrainFade(5f);

            }
        }
        if (Input.GetKeyDown(RestKey))
        {
            FellaSpawner.subtractFromColor(FellaSpawner.selectedColor, 1);
            FellaStateManager.AssignFirstToRest(FellaSpawner.selectedColor);
            Debug.Log("mimi");
            ActivesManager.StartRestFade(3f);

        }
    }

}
