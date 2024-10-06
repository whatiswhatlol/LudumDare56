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
        texts[0].text = spawner.yellowFellas.ToString();
        texts[1].text = spawner.greenFellas.ToString();
        texts[2].text = spawner.blueFellas.ToString();
        texts[3].text = spawner.purpleFellas.ToString();
        texts[4].text = spawner.redFellas.ToString();
    }


}
