using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    [Header("Building Section")]
    private GameObject _buildingPrefab;
    private GameObject _toBuild;
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hit;

    public LayerMask groundLayerMask;
    public static BuildingPlacer Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _mainCamera = Camera.main;
        _buildingPrefab = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(_buildingPrefab != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(_toBuild);
                _toBuild = null;
                _buildingPrefab = null;
                Cursor.visible = true;
                return;

            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_toBuild.activeSelf)
                    _toBuild.SetActive(false);
                return;
            }
            else if( !_toBuild.activeSelf)
            {
                _toBuild.SetActive(true);
            }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(_ray, out _hit, 1000f, groundLayerMask))
            {
                if (_toBuild.activeSelf)
                    _toBuild.SetActive(true);

                _toBuild.transform.position = _hit.point;

                BuildingManager buildingManager = _toBuild.GetComponent<BuildingManager>();
                Building building = _toBuild.GetComponent<Building>();

                if (building.HasResources())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (buildingManager.hasValidPlacement)
                        {
                            buildingManager.SetPlacementMode(BuildingState.Placed);
                            GameManager.Instance.ChangeResourcesAmount(building.ResourceType, -building.BuildingCost);
                            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                            {
                                _toBuild = null;
                                PrepareBuilding();
                            }
                            else
                            {
                                buildingManager.GetComponent<Collider>().isTrigger = false;
                                Cursor.visible = true;
                                _buildingPrefab = null;
                                _toBuild = null;
                            }
                        }
                        else
                        {
                            buildingManager.SetPlacementMode(BuildingState.NotValid);
                        }
                    }
                }
                else
                {
                    buildingManager.SetPlacementMode(BuildingState.NotValid);
                }

            }

        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        _buildingPrefab = prefab;
        PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null);
        Cursor.visible = false;
    }

    private void PrepareBuilding()
    {
        if (_toBuild)
        {
            Destroy(_toBuild);
        }

        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);

        BuildingManager buildingManager = _toBuild.GetComponent<BuildingManager>();
        buildingManager.isPlaced = false;
        buildingManager.SetPlacementMode(BuildingState.NotValid);
    }
}
