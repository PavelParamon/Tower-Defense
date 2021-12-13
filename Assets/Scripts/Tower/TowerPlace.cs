using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Material _material;
    private Color _initialColor;
    private GameObject _tower;
    private GameObject _salePanel;
    private bool _initialPlace;
    private bool _draggedTower;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _initialColor = _material.color;
        _initialPlace = true;
        _draggedTower = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _material.color = Color.white;
        if (_tower == null && BuilderController.Instance.TowerPrefabToBuild != null &&
            !BuilderController.Instance.MouseUp)
            _draggedTower = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _material.color = _initialColor;
        if (BuilderController.Instance.TowerPrefabToBuild != null &&
            !BuilderController.Instance.MouseUp)
            _draggedTower = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_tower != null)
        {
            if (!_salePanel.activeSelf)
                _salePanel.SetActive(true);
            else
            {
                _salePanel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (!_initialPlace && _tower == null)
        {
            transform.position += new Vector3(0, 1, 0);
            _initialPlace = true;
        }

        if (_draggedTower && _tower == null && BuilderController.Instance.MouseUp)
        {
            if (GameManager.Instance.GameController.Money >=
                BuilderController.Instance.TowerPrefabToBuild.GetComponent<Tower>().Cost)
            {
                GameManager.Instance.GameController.Money -=
                    BuilderController.Instance.TowerPrefabToBuild.GetComponent<Tower>().Cost;
                _tower = Instantiate(BuilderController.Instance.TowerPrefabToBuild, transform.position,
                    transform.rotation);
                _tower.GetComponent<Tower>().enabled = true;
                transform.position += new Vector3(0, -1, 0);
                _initialPlace = false;
                _salePanel = _tower.transform.Find("Canvas").gameObject;

                BuilderController.Instance.TowerPrefabToBuild = null;
            }

            _draggedTower = false;
        }
    }
}