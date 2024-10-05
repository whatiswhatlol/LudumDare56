using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NumberOfFellasUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text[] texts;
    private FellaSpawner spawner;
    void Start()
    {
        spawner = FellaSpawner.Instance;

        spawner.setNumberUpdater(this);
    }
    public void UpdateNumberOfFellas()
    {
        if (spawner.selectedColor == FellaSpawner.ColorType.Yellow)
        {
            texts[0].text = spawner.yellowFellas.ToString();
            return;
        }
        if (spawner.selectedColor == FellaSpawner.ColorType.Green)
        {
            texts[1].text = spawner.greenFellas.ToString();
            return;
        }
        if (spawner.selectedColor == FellaSpawner.ColorType.Blue)
        {
            texts[2].text = spawner.blueFellas.ToString();
            return;
        }
        if (spawner.selectedColor == FellaSpawner.ColorType.Purple)
        {
            texts[3].text = spawner.purpleFellas.ToString();
            return;
        }
        if (spawner.selectedColor == FellaSpawner.ColorType.Red)
        {
            texts[4].text = spawner.redFellas.ToString();
            return;
        }



    }


}
