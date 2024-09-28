using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Building Placement Materials")]
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;
    public MeshRenderer meshRenderer;
    private Dictionary<MeshRenderer, List<Material>> _initializeMaterials;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isPlaced;

    private Building _building;
    private bool _isDying;

    private int _numberOfObstacles;
    [SerializeField] private LayerMask _layer;

    private GameManager _gameManager;
    private GameObject _zone;
    private BuildingPlacer _buildingPlacer;

    private void Awake()
    {
        _isDying = false;
        hasValidPlacement = true;
        isPlaced = true;
        _numberOfObstacles = 0;
        _building = GetComponent<Building>();
        InitialMaterials();
       
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _buildingPlacer = BuildingPlacer.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced) return;

        if(((1 << other.gameObject.layer) & _layer.value) != 0)
        {
            _zone = other.gameObject;
        }

        _numberOfObstacles++;
        SetPlacementMode(BuildingState.NotValid);

    }

    private void OnTriggerExit(Collider other)
    {
        if (isPlaced) return;

        if (IsPlaced(other.gameObject)) return;

        if(((1 << other.gameObject.layer) & _layer.value) != 0)
        {
            _zone = null;
        }

        _numberOfObstacles--;
        if(_numberOfObstacles == 0)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Zone"))
                SetPlacementMode(BuildingState.Valid);
        }

    }

    public void SetPlacementMode(BuildingState state)
    {
        switch (state)
        {
            case BuildingState.Placed:
                isPlaced = true;
                hasValidPlacement = true;
                _building.enabled = true;
                StartCoroutine(_building.StartProduction());
                break;
            case BuildingState.Valid:
                hasValidPlacement = true;
                break;
            case BuildingState.NotValid:
                hasValidPlacement = false;
                break;
        }

        SetMaterial(state);
    }

    private void SetMaterial(BuildingState state)
    {
        if(state == BuildingState.Placed)
        {
            meshRenderer.sharedMaterials = _initializeMaterials[meshRenderer].ToArray();

        }
        else
        {
            Material materialToApply = state == BuildingState.Valid ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] materials;
            int numberOfMaterials;

            numberOfMaterials = _initializeMaterials[meshRenderer].Count;
            materials = new Material[numberOfMaterials];

            for(int i = 0; i < numberOfMaterials; i++)
            {
                materials[i] = materialToApply;
            }
            meshRenderer.sharedMaterials = materials;
        }

    }

    private void InitialMaterials()
    {
        if(_initializeMaterials == null)
        {
            _initializeMaterials = new Dictionary<MeshRenderer, List<Material>>();
        }

        if(_initializeMaterials.Count > 0)
        {
            foreach(var initialMaterial in _initializeMaterials)
            {
                initialMaterial.Value.Clear();
            }
            _initializeMaterials.Clear();
        }
        _initializeMaterials[meshRenderer] = new List<Material>(meshRenderer.sharedMaterials);

    }

    private bool IsPlaced(GameObject gameObject)
    {
        return ((1 << gameObject.layer) & _buildingPlacer.groundLayerMask.value) != 0;
    }

}
