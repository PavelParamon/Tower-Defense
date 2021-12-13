using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Sprite spriteTower;
    [SerializeField] private float radius;
    [SerializeField] private float reload;
    [SerializeField] private int cost;

    private Enemy _enemy;
    private Bullet _bullet;
    private float _timerShoot = 0;
    private SphereCollider collider;

    public int Cost => cost;

    public Sprite SpriteTower => spriteTower;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = radius;
    }

    void Update()
    {
        if (_timerShoot >= 0)
            _timerShoot -= Time.deltaTime;

        if (_enemy != null && _enemy.IsDead)
            _enemy = null;

        if (_enemy != null && !_enemy.IsDead)
        {
            LookAtEnemy(_enemy.transform);
            Shot(_enemy);
        }
    }

    public void Shot(Enemy enemy)
    {
        if (_timerShoot <= 0)
        {
            _bullet = Instantiate(prefabBullet, transform.Find("Rotate").transform.Find("Gun").gameObject.transform.position, transform.rotation).GetComponent<Bullet>();
            _bullet.Init(enemy.transform);

            var particle = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particle != null)
                particle.Play();
            _timerShoot = reload;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (_enemy == null && collider.tag.Equals("enemyBug") && !collider.GetComponent<Enemy>().IsDead)
            _enemy = collider.GetComponent<Enemy>();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (_enemy != null && collider.tag.Equals("enemyBug"))
            _enemy = null;
    }

    private void LookAtEnemy(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.Find("Rotate").transform.LookAt(targetPosition);
    }
}