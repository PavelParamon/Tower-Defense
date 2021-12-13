using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private GameObject _panelGame;
    private float preparationTime;
    private float timeToNextWave;

    public void Init(GameObject panelGame)
    {
        _panelGame = panelGame;
    }

    private void Start()
    {
        preparationTime = GameManager.Instance.LevelController.PreparationTime;
        timeToNextWave = preparationTime;
        _panelGame.transform.Find("HealthInt").GetComponent<Text>().text =
            GameManager.Instance.GameController.PlayerHealth.ToString();
        _panelGame.transform.Find("Count Money").GetComponent<Text>().text =
            "$: " + GameManager.Instance.GameController.Money;
        _panelGame.transform.Find("Current Wave").GetComponent<Text>().text = "Wave: " +
                                                                              GameManager.Instance.GameController
                                                                                  .NumWave + "/";
        _panelGame.transform.Find("Count Waves").GetComponent<Text>().text =
            GameManager.Instance.LevelController
                .CountWaves.ToString();

        GameManager.Instance.GameController.ChangePlayerHealth += ChangeHealth;
        GameManager.Instance.GameController.ChangeMoney += ChangeMoney;
        GameManager.Instance.GameController.ChangeWave += ChangeWave;
    }

    private void Update()
    {
        if (timeToNextWave == preparationTime)
        {
            StartCoroutine(ChangeTimeNexWave());
        }
    }

    private void ChangeHealth(GameController gameController)
    {
        var health = GameManager.Instance.GameController.PlayerHealth;
        if (health >= 40)
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().text =
                GameManager.Instance.GameController.PlayerHealth.ToString();
        else if (health < 40 && health >= 15)
        {
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().color = Color.yellow;
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().text =
                GameManager.Instance.GameController.PlayerHealth.ToString();
        }
        else if (health >= 0 && health < 15)
        {
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().color = Color.red;
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().text =
                GameManager.Instance.GameController.PlayerHealth.ToString();
        }
        else
        {
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().color = Color.red;
            _panelGame.transform.Find("HealthInt").GetComponent<Text>().text = "0";
        }
    }

    private void ChangeMoney(GameController gameController)
    {
        _panelGame.transform.Find("Count Money").GetComponent<Text>().text = "$: " +
                                                                             GameManager.Instance.GameController.Money;
    }

    private void ChangeWave(GameController gameController)
    {
        timeToNextWave = preparationTime;
        _panelGame.transform.Find("Current Wave").GetComponent<Text>().text = "Wave: " +
                                                                              GameManager.Instance.GameController
                                                                                  .NumWave + "/";
    }
    
    IEnumerator ChangeTimeNexWave() {
        for(;;)
        {
            _panelGame.transform.Find("Time Next Wave").GetComponent<Text>().text =
                ((int) timeToNextWave--).ToString();
            if (timeToNextWave < 0)
                break;
            yield return new WaitForSeconds(1);
        }
    }
}