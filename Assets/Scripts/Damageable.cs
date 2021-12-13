using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int healthMax;

    public Action<Damageable, bool> DamageEvent;
    public Action<Damageable> DieEvent;

    public int Health { get; private set; }

    public int HealthMax
    {
        get => healthMax;
        set => healthMax = value;
    }

    private void Awake()
    {
        Health = HealthMax;
    }

    private void OnTriggerEnter(Collider collider)
    {
        var _gameObjectDamageProvider = gameObject.GetComponent<DamageProvider>();
        var _damageProvider = collider.GetComponent<DamageProvider>();
        if (_damageProvider != null && _gameObjectDamageProvider != null)
        {
            if (_gameObjectDamageProvider.DamageAfterCollide || _damageProvider.DamageAfterCollide)
            {
                if(gameObject.name.Contains("Bullet"))
                    Health -= _damageProvider.Damage;
                if (DamageEvent != null && !collider.tag.Equals("enemyBug") && gameObject.tag.Equals("enemyBug"))
                {
                    Health -= _damageProvider.Damage;
                    DamageEvent(this, collider.name.Contains("Freezed"));
                }
                    

                if (Health <= 0)
                {
                    if (DieEvent != null)
                        DieEvent(this);
                    //Destroy(gameObject);
                }
            }
        }
    }
}