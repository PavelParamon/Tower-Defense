using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int reward;
    private Move _moveEnemy;
    private Damageable _damagable;
    private DamageProvider _damageProvider;
    private bool isStunned;
    private Material _defaultMaterial;
    private GameObject _body;
    private Image _hpBar;

    public bool IsDead { get; private set; }


    private void Awake()
    {
        _moveEnemy = GetComponent<Move>();
        _hpBar = transform.Find("HP Canvas").transform.Find("FillGround").GetComponent<Image>();
        _damagable = GetComponent<Damageable>();
        _damageProvider = GetComponent<DamageProvider>();
        _damagable.DieEvent += OnDead;
        _damagable.DamageEvent += onGiveDamage;
        _damagable.HealthMax = health;
        _body = gameObject.transform.Find("MainBody").transform.gameObject;
        if (_body != null)
            _defaultMaterial = _body.GetComponent<SkinnedMeshRenderer>().material;
        IsDead = false;
        isStunned = false;
    }


    void FixedUpdate()
    {
        LookHpBarAtCamera();
        if (_moveEnemy.isFinish())
        {
            GameManager.Instance.LevelController.getWaveController().DeleteEnemy();
            GameManager.Instance.GameController.PlayerHealth -= _damageProvider.Damage;
            Destroy(gameObject);
        }
    }

    private void OnDead(Damageable damagable)
    {
        _moveEnemy.Stop();
        IsDead = true;
        GameManager.Instance.GameController.Money += reward;
        _damagable.DieEvent -= OnDead;
        _damagable.DamageEvent -= onGiveDamage;
        GameManager.Instance.LevelController.getWaveController().DeleteEnemy();
        Destroy(gameObject, 2f);
        gameObject.GetComponent<Animator>().SetBool("Death", true);
    }

    private void onGiveDamage(Damageable damagable, bool isFreezed)
    {
        _hpBar.fillAmount = (float) _damagable.Health / _damagable.HealthMax;

        if (isFreezed && !isStunned)
        {
            isStunned = true;
            StartCoroutine(Freezed());
        }
    }

    private void LookHpBarAtCamera()
    {
        Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y,
            Camera.main.transform.position.z);
        transform.Find("HP Canvas").transform.LookAt(targetPosition);
    }

    private IEnumerator Freezed()
    {
        _moveEnemy.Speed = _moveEnemy.Speed / 2;
        setEnemyFreezedTexture();
        yield return new WaitForSeconds(5);
        _moveEnemy.Speed = _moveEnemy.Speed * 2;
        isStunned = false;
        setEnemyInitialTexture();
        yield return null;
    }

    private void setEnemyFreezedTexture()
    {
        _body.GetComponent<SkinnedMeshRenderer>().material = (Material) Resources.Load("Materials/Enemies/Freezed");
    }

    private void setEnemyInitialTexture()
    {
        _body.GetComponent<SkinnedMeshRenderer>().material = _defaultMaterial;
    }
}