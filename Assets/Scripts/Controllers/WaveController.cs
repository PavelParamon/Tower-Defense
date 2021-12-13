using System;
using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private LevelConfig.Wave _wave;
    private int _countTypeEnemies;
    private int _countEnemies;
    private int _nowCountEnemies;
    private bool _waveIsEnd;
    private bool _changeWave;
    
    void FixedUpdate()
    {
        if (_waveIsEnd)
        {
            StopCoroutine(SpawnProcess());
        }
        else if (_changeWave && !_waveIsEnd)
        {
            ResetProcess();
            StartCoroutine(SpawnProcess());
        }
            
    }

    public bool isEnd()
    {
        return _waveIsEnd;
    }

    public void setWave(LevelConfig.Wave wave)
    {
        _wave = wave;
        _changeWave = true;
        _waveIsEnd = false;
    }

    private IEnumerator SpawnProcess()
    {
        for (int i = 0; i < _countTypeEnemies; i++)
        {
            for (int j = 0; j < _wave.getEnemyies()[i].getCount(); j++)
            {
                SpawnEnemy(_wave.getEnemyies()[i].getPrefab(), _wave.getSpawnPoints()[UnityEngine.Random.Range(0, _wave.getSpawnPoints().Length)]);
                yield return new WaitForSeconds(_wave.getTimeSpawn());
            }
        }
    }

    private void SpawnEnemy(GameObject enemy, GameObject position)
    {
        Instantiate(enemy, GameObject.Find(position.name).transform.position, GameObject.Find(position.name).transform.rotation);
        _nowCountEnemies++;
        _countEnemies--;
    }

    public void DeleteEnemy()
    {
        _nowCountEnemies--;
        CheckEndWave();
    }

    private void CheckEndWave()
    {
        if (_nowCountEnemies == 0 && _countEnemies == 0)
        {
            Debug.Log("Wave is ended");
            _waveIsEnd = true;
            GameManager.Instance.GameController.NumWave++;
        }
    }

    private void ResetProcess()
    {
        _changeWave = false;
        _countEnemies = 0;
        _countTypeEnemies = _wave.getEnemyies().Length;
        for (int i = 0; i < _countTypeEnemies; i++)
            _countEnemies += _wave.getEnemyies()[i].getCount();
    }
}