using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeZoneController : MonoBehaviour
{ 
    public GameObject CollectableBox;
    public List<GameObject> BoxNodes;
    public List<GameObject> CollectableBoxes;

    public GameObject CubeGate;
    public bool IsGateOpen = false;
    
    //public Transform SpawnCenter;

    private void Start()
    {
        if (CollectableBoxes.Count < BoxNodes.Count)
        {
            StartCoroutine(SpawnCollectableBoxes(3f));
        }


        
    }

    private void Update()
    {
        if (CollectableBoxes.Count < BoxNodes.Count)
        {
            //StartCoroutine(SpawnCollectableBoxes(3f));
            Debug.Log("baþladý");
        }
        else
        {
            //StopCoroutine(SpawnCollectableBoxes(.5f));
        }
        
        
    }

    private IEnumerator SpawnCollectableBoxes(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        while (!(CollectableBoxes.Count >= BoxNodes.Count))
        {
            GameObject SpawnNode = GetRandomEmptyNode(BoxNodes);
            GameObject newCollectable = Instantiate(CollectableBox, SpawnNode.transform);
            CollectableBoxes.Add(newCollectable);


            Debug.Log("spawncollectableboxes");

            yield return new WaitForSeconds(waitTime);
        }
       

    }

    private GameObject GetRandomEmptyNode(List<GameObject> NodeList)
    {
        int randomNumber = Random.Range(0,NodeList.Count);

        if (NodeList[randomNumber].transform.childCount != 0)
        {
            Debug.Log("dolu node geldi");
            return GetRandomEmptyNode(NodeList);

        }

        Debug.Log("getrandomemptynode");
        return NodeList[randomNumber];
        
    }


   

    
}
