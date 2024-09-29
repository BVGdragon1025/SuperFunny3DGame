using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Building UI")]
    [SerializeField] private TextMeshProUGUI _buildingName;
    [SerializeField] private TextMeshProUGUI _lemurAmountText;
    [SerializeField] private GameObject _buildingUI;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;

    public static UIManager Instance;
    
    public GameObject BuildingUI { get { return _buildingUI; } }
    public TextMeshProUGUI LemurText { get { return _lemurAmountText; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateBuildingUI(string buildingName, string lemurCount, Building building)
    {
        _addButton.onClick.RemoveAllListeners();
        _removeButton.onClick.RemoveAllListeners();
        _buildingUI.SetActive(true);
        _buildingName.text = buildingName;
        _lemurAmountText.text = lemurCount;
        _addButton.onClick.AddListener(delegate { building.ChangeLemurCount(1); });
        _removeButton.onClick.AddListener(delegate { building.ChangeLemurCount(-1); });

    }
}
