using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefabs;

    private GameObject _panel;
    private Tower[] _towers;
    public static ShopPanel Instance { get; private set; }

    public GameObject Panel => _panel;

    private void Awake()
    {
        _panel = gameObject;
        Instance = this;
        _towers = new Tower[towerPrefabs.Length];
        gameObject.AddComponent<BuilderController>();
    }

    private void Start()
    {
        for (int i = 0; i < towerPrefabs.Length; i++)
        {
            _towers[i] = towerPrefabs[i].GetComponent<Tower>();
            CreateShopElement(towerPrefabs[i], _towers[i], i);
        }
    }

    private void CreateShopElement(GameObject towerPrefab, Tower tower, int index)
    {
        GameObject newButton =
            new GameObject(tower.name + " " + index, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(_panel.transform);
        newButton.AddComponent<TowerPrefab>();
        newButton.GetComponent<TowerPrefab>().Tower = towerPrefab;
        newButton.GetComponent<Image>().sprite = tower.SpriteTower;
        RectTransform rc = newButton.GetComponent<RectTransform>();
        if (towerPrefab.name.Contains("Cannon"))
            rc.sizeDelta = new Vector2(20, 20);
        else
            rc.sizeDelta = new Vector2(25, 20);
        rc.localPosition = new Vector3(rc.localPosition.x, rc.localPosition.y, 2.2f);
        rc.localRotation = Quaternion.Euler(-3.5f, 0f, 0f);
        rc.localScale = new Vector3(25f, 3f, 3.5f);


        GameObject newText = new GameObject("Cost " + tower.name, typeof(Text));
        newText.transform.SetParent(newButton.transform);
        rc = newText.GetComponent<RectTransform>();
        rc.localPosition = new Vector3(0f, 7f, 0f);
        rc.sizeDelta = new Vector2(20, 5);
        rc.localRotation = Quaternion.Euler(0f, 0f, 0f);
        rc.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        newText.GetComponent<Text>().text = "$ " + tower.Cost;
        newText.GetComponent<Text>().font = (Font) Resources.Load("Fonts/ReadexPro");
        newText.GetComponent<Text>().color = Color.yellow;
        newText.GetComponent<Text>().fontSize = 4;
        newText.GetComponent<Text>().alignment = TextAnchor.UpperRight;


        newButton.AddComponent<EventsShopButton>();

        //newButton.GetComponent<Button>().onClick.AddListener(delegate { Press(tower, index); });
    }
}