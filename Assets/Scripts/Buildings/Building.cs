using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Data"), Tooltip("Basic building data, e.g. resources amount, building cost")]
    [SerializeField] private string _buildingName;
    public string BuildingName {  get { return _buildingName; } }
    [SerializeField] private ResourceType _resourceType;
    public ResourceType ResourceType { get { return _resourceType; } }
    [SerializeField] private int _buildingCost = 0;
    public int BuildingCost {  get { return _buildingCost; } }
    [SerializeField] private float _resourceAmount;
    public float Resource { get { return _resourceAmount; } }
    [SerializeField] private float _timeDelay;
    public float TimeDelay { get { return _timeDelay; } }
    [SerializeField] private bool _hasFinished;
    public bool HasFinished { get { return _hasFinished; } }

    [Header("Lemur Data"), Tooltip("Lemur data, e.g. maximum lemur count in this building, current lemur count etc.")]
    [SerializeField] private int _maxLemurCount;
    public int MaxLemurCount { get { return _maxLemurCount; } }
    [SerializeField] private int _currentLemurCount = 0;
    public int CurrentLemurCount { get { return _currentLemurCount; } }

    [Header("Upgrades Data"), Tooltip("Upgrade levels etc.")]
    public int buildingLevelUpgrade;
    public int buildingEfficiencyUpgrade;
    public bool _randomChanceUnlocked;
    public bool _freeFoodUnlocked;

    protected GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(StartProduction());
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasFinished && _currentLemurCount > 0) ResetProduction();
    }

    public bool HasResources()
    {
        if(GameManager.Instance.HasResources(_resourceType, _buildingCost))
            return true;
        return false;
    }

    public IEnumerator StartProduction()
    {
        _hasFinished = false;
        yield return new WaitForSeconds(_timeDelay);
        gameManager.ChangeResourcesAmount(_resourceType, _resourceAmount * _currentLemurCount);
        _hasFinished = true;
    }

    public void GiveResource()
    {
        gameManager.ChangeResourcesAmount(_resourceType, _resourceAmount * _currentLemurCount);
        ResetProduction();
    }

    public void ResetProduction()
    {
        StartCoroutine(StartProduction());
    }

    public void ChangeLemurCount(int lemurAmount)
    {
        _currentLemurCount = Mathf.Clamp(_currentLemurCount + lemurAmount, 0, _maxLemurCount);
        gameManager.UnemployedLemursAmount = lemurAmount;
    }

}
