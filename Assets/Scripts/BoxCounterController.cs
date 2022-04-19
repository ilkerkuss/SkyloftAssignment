using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCounterController : MonoBehaviour
{
    [SerializeField]private Text StackBoxText;
    [SerializeField] private PlayerController Player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerController.UpdateBoxText += SetBoxText;
    }

    private void OnDisable()
    {
        PlayerController.UpdateBoxText -= SetBoxText;
    }

    private void SetBoxText(int BoxAmount)
    {
        StackBoxText.text = BoxAmount.ToString();
    }
}
