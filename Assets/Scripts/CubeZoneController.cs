using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeZoneController : MonoBehaviour
{ 
    public GameObject CollectableBox;
    public List<GameObject> BoxNodes;
    public List<GameObject> CollectableBoxes;

    public GameObject CubeGate;
    public bool IsCubeZoneActive = false;

    private WaitForSeconds NodeActivationDelay = new WaitForSeconds(1f);

    
    //public Transform SpawnCenter;

    private void Start()
    {
        /*
        if (IsCubeZoneActive)
        {
            StartCoroutine(ActivateRandomNode(BoxNodes));
        }
        */
        

        
    }

    private void Update()
    {
        if (IsCubeZoneActive)
        {
            StartCoroutine(ActivateRandomNode(BoxNodes));
            StopCoroutine(ActivateRandomNode(BoxNodes));
            CubeGate.SetActive(false);
        }



        //StartCoroutine(ActivateRandomNode(BoxNodes));
        if (CollectableBoxes.Count < BoxNodes.Count)
        {
            //StartCoroutine(SpawnCollectableBoxes(3f));

        }
        else
        {
            //StopCoroutine(SpawnCollectableBoxes(.5f));
        }
    }


    /*
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
    */
    /*
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
    */

    private IEnumerator ActivateRandomNode(List<GameObject> NodeList)
    {
        //int randomNumber = Random.Range(0, NodeList.Count);

        foreach (var item in NodeList)
        {
            item.SetActive(true);
            yield return NodeActivationDelay;
        }
    }

    private void CallActivateRandomNodeFunc()
    {
        StartCoroutine(ActivateRandomNode(BoxNodes));
    }


   

    
}
