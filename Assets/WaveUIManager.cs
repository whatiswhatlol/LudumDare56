using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text Waves;
    public  TMP_Text Enemies;
    EnemySpawner Spawner;
    void Start()
    {
        Spawner = EnemySpawner.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Waves.text = "Wave: " + Spawner.currentWave;
        Enemies.text = "Enemies Remaining: " + Spawner.activeEnemies;
    }
}
