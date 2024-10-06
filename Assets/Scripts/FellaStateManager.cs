using UnityEngine;
using System.Collections;

public class FellaStateManager : MonoBehaviour
{
    public static FellaStateManager Instance { get; private set; }  // Singleton

    [Header("State Timers")]
    public float trainTime = 5f;  // Time required to train fellas
    public float restInterval = 3f;  // Time between increasing the number of fellas in the Rest state

    private FellaSpawner fellaSpawner;  // Reference to the FellaSpawner

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fellaSpawner = FellaSpawner.Instance;  // Get reference to the FellaSpawner
    }



    // Assign a fella to Train
    public void AssignFirstToTrain(FellaSpawner.ColorType colorType)
    {
        int temp = fellaSpawner.getColorCount(colorType);
        if (temp >= 0)
        {
            //ADD UI REMINDER
            StartCoroutine(TrainFella(colorType));
        }
    }

    // Assign a fella to Rest
    public void AssignFirstToRest(FellaSpawner.ColorType colorType)
    {
        int temp = fellaSpawner.getColorCount(colorType);
        if (temp >= 0)
        {
            //ADD UI REMINDER
            Debug.Log("Assign");

            StartCoroutine(RestFella(colorType));
        }
    }

    // Coroutine to handle fella training
    private IEnumerator TrainFella(FellaSpawner.ColorType colorType)
    {
        yield return new WaitForSeconds(trainTime);
        fellaSpawner.UpgradeFella(colorType);

    }

    // Coroutine to handle fella resting
    private IEnumerator RestFella(FellaSpawner.ColorType colorType)
    {
        Debug.Log("Begin REst");

        yield return new WaitForSeconds(restInterval);
        fellaSpawner.RestFella(colorType);
    }


}
