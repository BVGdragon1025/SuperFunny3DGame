using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Values")]
    [SerializeField] private float _plasticAmount;
    [SerializeField] private float _scrapAmount;
    [SerializeField] private float _foodAmount;
    [SerializeField] private int _lemurAmount;
    [SerializeField] private int _unemployedLemurs;

    [Header("Timers")]
    [SerializeField] private float _lemurSpawnTimer;

    public float PlasticAmount { get { return _plasticAmount; } }
    public float ScrapAmount { get { return _scrapAmount; } }
    public float FoodAmount { get { return _foodAmount; } }
    public int LemurAmount { get { return _lemurAmount; } }
    public int UnemployedLemursAmount { get { return _unemployedLemurs; } set { _unemployedLemurs = value; } }
    private UIManager _uiManager;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        InvokeRepeating(nameof(AddLemur), _lemurSpawnTimer, _lemurSpawnTimer);
        _unemployedLemurs = _lemurAmount;
    }

    private void Start()
    {
        _uiManager = UIManager.Instance;
        _uiManager.UpdateInfoUI(ResourceType.Plastic, _plasticAmount);
        _uiManager.UpdateInfoUI(ResourceType.Scrap, _scrapAmount);
        _uiManager.UpdateInfoUI(ResourceType.Food, _foodAmount);
    }

    public void ChangeResourcesAmount(ResourceType resourceType, float resourceAmount)
    {
        switch(resourceType)
        {
            case ResourceType.Plastic:
                _plasticAmount += resourceAmount;
                _uiManager.UpdateInfoUI(resourceType, _plasticAmount += resourceAmount);
                Debug.Log($"Resource added: {resourceType}, amount: {_plasticAmount += resourceAmount}");
                break;
            case ResourceType.Scrap:
                _scrapAmount += resourceAmount;
                _uiManager.UpdateInfoUI(resourceType, _scrapAmount += resourceAmount);
                Debug.Log($"Resource added: {resourceType}, amount: {_scrapAmount += resourceAmount}");
                break;
            case ResourceType.Food:
                _foodAmount += resourceAmount;
                _uiManager.UpdateInfoUI(resourceType, _foodAmount += resourceAmount);
                Debug.Log($"Resource added: {resourceType}, amount: {_foodAmount += resourceAmount}");
                break;

        }
    }

    public bool HasResources(ResourceType resourceType, float resourceAmount)
    {
        switch (resourceType)
        {
            case ResourceType.Plastic:
                if (resourceAmount <= _plasticAmount)
                    return true;
                break;
            case ResourceType.Scrap:
                if(resourceAmount <= _scrapAmount)
                    return true;
                break;
            case ResourceType.Food:
                if (resourceAmount <= _foodAmount)
                    return true;
                break;
        }
        return false;
    }

    private void AddLemur()
    {
        _lemurAmount++;
        _unemployedLemurs++;
        Debug.Log($"Lemur Added! Current amount: {_lemurAmount}");

    }
}
