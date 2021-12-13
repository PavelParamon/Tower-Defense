using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProvider : MonoBehaviour
{
    [SerializeField] private int damage;
    public bool DamageAfterCollide;

    public int Damage { get; private set; }
    

    private void Awake()
    {
        Damage = damage;
    }
    
}
