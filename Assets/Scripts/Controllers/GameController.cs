using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int baseHealth;
    [SerializeField] private int baseMoney;

    public Action<GameController> GameOver;
    public Action<GameController> Victory;
    public Action<GameController> ChangePlayerHealth;
    public Action<GameController> ChangeMoney;
    public Action<GameController> ChangeWave;

    private int _playerHealth;
    private int _money;
    private int _numWave;

    public bool IsVictory { get; set; }

    public int PlayerHealth
    {
        get => _playerHealth;
        set
        {
            _playerHealth = value;
            if (ChangePlayerHealth != null)
                ChangePlayerHealth(this);
        }
    }

    public int NumWave
    {
        get => _numWave;
        set
        {
            _numWave = value;
            if (ChangeWave != null)
                ChangeWave(this);
        }
    }

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            if (ChangeMoney != null)
                ChangeMoney(this);
        }
    }

    private void Awake()
    {
        _playerHealth = baseHealth;
        _money = baseMoney;
        _numWave = 1;
        IsVictory = false;
    }

    private void Update()
    {
        if (_playerHealth <= 0)
        {
            GameManager.Instance.LevelController.StopLevel();
            if (GameOver != null)
                GameOver(this);
        }

        if (IsVictory)
        {
            if (Victory != null)
                Victory(this);
        }
    }
}