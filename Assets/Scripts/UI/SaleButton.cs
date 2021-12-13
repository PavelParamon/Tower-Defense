using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleButton : MonoBehaviour
{
    public void Sale()
    {
        GameManager.Instance.GameController.Money += transform.root.gameObject.GetComponent<Tower>().Cost / 2;
        Destroy(transform.root.gameObject);
    }
}