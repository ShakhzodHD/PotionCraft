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

    private bool isGenerate = true;

    void Start()
    {
        StartGenerate();
    }

    private void Update()
    {
        float number—hildObj = gameObject.transform.childCount;
        CheckLimitGenerate(number—hildObj);
        if (isGenerate == false && number—hildObj < maxObj)
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
            GenerateResources();
        }
    }

    private void GenerateResources()
    {
        GameObject spawnedObj = Instantiate(resourcePrefab);
        spawnedObj.tag = tagObj;

        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.y += 1;
        spawnedObj.transform.position = spawnPos;
        spawnedObj.transform.SetParent(transform);
    }

    private void CheckLimitGenerate(float number)
    {
        if (number >= maxObj)
        {
            isGenerate = false;
        }
    }
}
