using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private bool _isDestroyed = false;
    public bool isDestroyed
    {
        get => _isDestroyed;
        set
        {
            if (value) Destroy(gameObject);
        }
    }
    [SerializeField] public MaterialsType Material = MaterialsType.None;
    [SerializeField] public GameObject MaterialPrefab;

    public void Awake()
    {
        if (Material == MaterialsType.None) Material = MaterialsType.Wood;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
