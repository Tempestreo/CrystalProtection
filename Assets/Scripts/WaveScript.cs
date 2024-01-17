using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;

    [SerializeField] Transform[] EnemyPositions;

    [SerializeField] bool isWaveStarted;
    [SerializeField] int waveCount;
    void Start()
    {
        isWaveStarted = false;
        waveCount = 0;
    }


    void Update()
    {
        StartWave();
    }
    bool DeathCheck()
    {
        if (isWaveStarted)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].activeSelf == false)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    void StartWave()
    {
        if (isWaveStarted == false)
        {
            waveCount++;
            isWaveStarted = true;
            WaveInProcess();
        }
    }
    void WaveInProcess()
    {
        //add character and crystal's health control into condition
        if (DeathCheck() == true)
        {
            //new wave
            for (int i = 0; i < waveCount*2; i++)
            {
                Enemies[i % 2].transform.position = EnemyPositions[i].position;
                Enemies[i % 2].transform.rotation = EnemyPositions[i].rotation;
                Instantiate(Enemies[i % 2]);
            }
        }
    }
}
