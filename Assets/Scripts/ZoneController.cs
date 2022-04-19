using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ZoneController : MonoBehaviour
{
    public ZoneScriptableObject ZoneSO;
    public int ZoneOpenAmount;

    public TextMeshProUGUI ZoneOpenAmountText;

    public bool IsZoneActive;
    public GameObject ZoneGate;

    public ZoneController DependencyZone;
    public CubeZoneController DependencyCubeZone;

    public SkinnedMeshRenderer HelperMeshRenderer;

    private void Start()
    {
        ZoneOpenAmount = ZoneSO.ZoneOpenCost;
        ZoneOpenAmountText.text = ZoneOpenAmount.ToString();

        if (ZoneSO.ID == 0)
        {
            IsZoneActive = true;
        }

    }


    private void OpenZone()
    {
        ZoneGate.SetActive(false);
        IsZoneActive = true;

        //HelperMeshRenderer.enabled = true;


        if (DependencyCubeZone == null)
        {
            return;
        }
        OpenCubeZone();
    }

    private void OpenCubeZone()
    {
      
        DependencyCubeZone.IsCubeZoneActive = true;
 
    }

    private void OnEnable()
    {
        PlayerController.DecreaseZoneAmount += DecreaseZoneAmount;
    }

    private void OnDisable()
    {
        
    }




    private void DecreaseZoneAmount(int ID)
    {
        if (ID == (int)(ZoneSO.ID))
        {
            ZoneOpenAmount--;
            ZoneOpenAmountText.text = ZoneOpenAmount.ToString();
            if (ZoneOpenAmount == 0)
            {
                if (DependencyZone == null)
                {
                    OpenZone();

                    PlayerController.DecreaseZoneAmount -= DecreaseZoneAmount;
                }
                else
                {
                    if (DependencyZone.IsZoneActive)
                    {
                        OpenZone();

                        PlayerController.DecreaseZoneAmount -= DecreaseZoneAmount;
                    }
                }

                
            }

        }

    }
}
