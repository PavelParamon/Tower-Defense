using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameController gameController;
    
    public static GameManager Instance { get; private set; }
    public LevelController LevelController
    {
        get { return levelController; }
    }
    
    public GameController GameController
    {
        get { return gameController; }
    }

    private void Awake()
    {
        Instance = this;
    }
}
