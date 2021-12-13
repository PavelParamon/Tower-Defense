using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventsShopButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private GameObject _shadowTower;

    public void OnPointerDown(PointerEventData eventData)
    {
        BuilderController.Instance.MouseUp = false;
        BuilderController.Instance.TowerPrefabToBuild = gameObject.GetComponent<TowerPrefab>().Tower;
        _shadowTower = Instantiate(gameObject.GetComponent<TowerPrefab>().Tower, gameObject.transform.position,
            gameObject.transform.rotation);
        if(_shadowTower.name.Contains("Cannon"))
            _shadowTower.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
        else
            _shadowTower.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        setObjectShadow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //eventData.pointerCurrentRaycast.gameObject.transform.root.transform.Find("Ground").position = Input.mousePosition;
        //_shadowTower.transform.position = Input.mousePosition;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        _shadowTower.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(_shadowTower);
        BuilderController.Instance.MouseUp = true;
    }

    private void setObjectShadow()
    {
        Transform[] children = _shadowTower.GetComponentsInChildren<Transform>();
        
        foreach (Transform child in children)
        {
            if(child.GetComponent<MeshRenderer>())
            {
                child.gameObject.GetComponent<MeshRenderer>().material = (Material)Resources.Load("Materials/Towers/Shadow");
            }
        }

    }
}