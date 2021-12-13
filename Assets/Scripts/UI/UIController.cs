using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject panelGame;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    
    private GamePanel _panelGame;

    private void Awake()
    {
        _panelGame = panelGame.AddComponent<GamePanel>();
        _panelGame.Init(panelGame);
    }

    private void Start()
    {
        panelGame.SetActive(true);
        ShopPanel.Instance.Panel.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        GameManager.Instance.GameController.GameOver += GameOver;
        GameManager.Instance.GameController.Victory += Victory;
    }

    public void GameOver(GameController gameController)
    {
        GameManager.Instance.GameController.GameOver -= GameOver;
        panelGame.SetActive(false);
        losePanel.SetActive(true);
        ShopPanel.Instance.Panel.SetActive(false);
    }

    public void Victory(GameController gameController)
    {
        GameManager.Instance.GameController.GameOver -= Victory;
        panelGame.SetActive(false);
        winPanel.SetActive(true);
        ShopPanel.Instance.Panel.SetActive(false);
    }

    
}
