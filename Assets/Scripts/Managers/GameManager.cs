using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Main Values")]
    [SerializeField] private int _plasticAmount;
    [SerializeField] private int _scrapAmount;
    [SerializeField] private int _foodAmount;
    [SerializeField] private int _lemurAmount;

    [Header("Timers")]
    [SerializeField] private float _lemurSpawnTimer;

    public int PlasticAmount { get { return _plasticAmount; } }
    public int ScrapAmount { get { return _scrapAmount; } }
    public int FoodAmount { get { return _foodAmount; } }
    public int LemurAmount { get { return _lemurAmount; } }

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
    }

    public void ChangeResourcesAmount(ResourceType resourceType, int resourceAmount)
    {
        switch(resourceType)
        {
            case ResourceType.Plastic:
                _plasticAmount += resourceAmount;
                break;
            case ResourceType.Scrap:
                _scrapAmount += resourceAmount;
                break;
            case ResourceType.Food:
                _foodAmount += resourceAmount;
                break;

        }

        Debug.Log($"Resource added: {resourceType}, amount: {resourceAmount}");
    }

    private void AddLemur()
    {
        _lemurAmount++;
        Debug.Log($"Lemur Added! Current amoutn: {_lemurAmount}");
    }
}
