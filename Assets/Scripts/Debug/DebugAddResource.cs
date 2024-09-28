using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddResource : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _resourceAmount;

    public void AddResource()
    {
        GameManager.Instance.ChangeResourcesAmount(_resourceType, _resourceAmount);
    }
}
