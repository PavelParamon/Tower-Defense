using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public static BuilderController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject TowerPrefabToBuild { get; set; }

    public bool MouseUp { get; set; }
}