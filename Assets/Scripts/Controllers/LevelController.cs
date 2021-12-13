using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;

    private LevelConfig.Wave _currentWave;
    private WaveController _waveController;
    private bool _levelIsEnd;
    private Coroutine _spawnCoroutine;

    public bool LevelIsEnd
    {
        get => _levelIsEnd;
        set => _levelIsEnd = value;
    }

    public WaveController getWaveController()
    {
        return _waveController;
    }

    public int CountWaves { get; private set; }
    public float PreparationTime { get; private set; }


    private void Awake()
    {
        _levelIsEnd = false;
        CountWaves = _levelConfig.waves.Length;
        PreparationTime = _levelConfig.preparationTime;
        _waveController = gameObject.AddComponent<WaveController>();
    }

    void Start()
    {
        GameManager.Instance.GameController.NumWave = 1;
        _spawnCoroutine = StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        if (_levelIsEnd && _waveController.isEnd() && GameManager.Instance.GameController.PlayerHealth > 0)
            GameManager.Instance.GameController.IsVictory = true;
    }

    public void StopLevel()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(_levelConfig.preparationTime);
        GameObject doorR1 = GameObject.Find("Ground").transform.Find("ForestCastle_Red 1").transform
            .Find("ForestCastleDoorR_Red").gameObject;
        GameObject doorL1 = GameObject.Find("Ground").transform.Find("ForestCastle_Red 1").transform
            .Find("ForestCastleDoorL_Red").gameObject;
        GameObject doorR2 = GameObject.Find("Ground").transform.Find("ForestCastle_Red 2").transform
            .Find("ForestCastleDoorR_Red").gameObject;
        GameObject doorL2 = GameObject.Find("Ground").transform.Find("ForestCastle_Red 2").transform
            .Find("ForestCastleDoorL_Red").gameObject;
        _currentWave = _levelConfig.waves[0];
        _waveController.setWave(_currentWave);
        OpenCloseDoors(doorR1, doorL1, doorR2, doorL2);
        for (int i = 1; i < _levelConfig.waves.Length; i++)
        {
            if (_waveController.isEnd())
            {
                OpenCloseDoors(doorR1, doorL1, doorR2, doorL2);
                GameManager.Instance.GameController.Money += _levelConfig.reward * i;
                yield return new WaitForSeconds(_levelConfig.preparationTime);
                OpenCloseDoors(doorR1, doorL1, doorR2, doorL2);
                _currentWave = _levelConfig.waves[i];
                _waveController.setWave(_currentWave);
            }
            else
            {
                i--;
                yield return null;
            }
        }

        _levelIsEnd = true;
    }

    private void OpenCloseDoors(GameObject doorR1, GameObject doorL1, GameObject doorR2, GameObject doorL2)
    {
        doorR1.SetActive(!doorR1.activeSelf);
        doorL1.SetActive(!doorL1.activeSelf);
        doorR2.SetActive(!doorR2.activeSelf);
        doorL2.SetActive(!doorL2.activeSelf);
    }
}