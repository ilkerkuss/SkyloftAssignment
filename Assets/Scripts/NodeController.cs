using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public GameObject CollectableBox;
    public CubeZoneController NodeParentZone;

    public bool IsNodeEmpty;
    public float SpawnWaitTime;
    public float CurrentTimeSpawn;


    void Start()
    {
        IsNodeEmpty = true;
        NodeParentZone = GetComponentInParent<CubeZoneController>();
        
    }

    void Update()
    {
        if (IsNodeEmpty)
        {
            if (CurrentTimeSpawn > 0 )
            {
                CurrentTimeSpawn -= Time.deltaTime;
            }

            else
            {
                CurrentTimeSpawn = SpawnWaitTime;
                StartCoroutine(SpawnCollectableBoxes(3f));
            }
            
        }

        if (transform.childCount == 0)
        {
            IsNodeEmpty = true;
            StopCoroutine(SpawnCollectableBoxes(3f));
        }
    }

    private IEnumerator SpawnCollectableBoxes(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        while ((IsNodeEmpty))
        {
            GameObject SpawnNode = this.gameObject;
            GameObject newCollectableBox = Instantiate(CollectableBox, SpawnNode.transform);
            NodeParentZone.CollectableBoxes.Add(newCollectableBox);
            
    

            IsNodeEmpty = false;

            
            yield return new WaitForSecondsRealtime(waitTime);
        }

    }
}
