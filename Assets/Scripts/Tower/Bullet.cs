using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _target;
    private Damageable _damageable;

    public void Init(Transform target)
    {
        _target = target;
        _damageable = gameObject.GetComponent<Damageable>();
        _damageable.DieEvent += DestroyBullet;
    }

    void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void DestroyBullet(Damageable damageable)
    {
        Destroy(gameObject);
    }
}