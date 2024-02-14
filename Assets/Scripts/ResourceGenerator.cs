using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private string tagObj;
    [SerializeField] private float intervalGenerate;
    [SerializeField, Range(1, byte.MaxValue)] private byte maxObj;

    public ResourceType resourceType;

    private int generatedCount;
    private bool isGenerate = true;

    public enum ResourceType
    {
        Leaf,
        Wood,
        Hide
    }

    void Start()
    {
        StartGenerate();
    }

    private void Update()
    {
        CheckLimitGenerate();
        if (isGenerate == false && generatedCount < maxObj)
        {
            isGenerate = true;
            StartGenerate();
        }
    }

    private void StartGenerate()
    {
        StartCoroutine(RepeatedGenerate(intervalGenerate));
    }
    private IEnumerator RepeatedGenerate(float interval)
    {
        while (isGenerate == true)
        {
            yield return new WaitForSeconds(interval);
            GenerateResources(resourceType);
            generatedCount++;
        }
    }

    private void GenerateResources(ResourceType type)
    {
        GameObject spawnedObj = Instantiate(resourcePrefab);
        spawnedObj.tag = tagObj;
        Vector3 spawnPos = spawnedObj.transform.position;
        spawnPos = gameObject.transform.position;
        spawnPos.y += 1;
        spawnedObj.transform.position = spawnPos;
        
        ResourceProperties resourceProperties = spawnedObj.GetComponent<ResourceProperties>();
        resourceProperties.typeResource = type.ToString();
    }

    private void CheckLimitGenerate()
    {
        if (generatedCount >= maxObj)
        {
            isGenerate = false;
        }
    }

    public void OnTakenObj()
    {
        generatedCount--;
    }
}
