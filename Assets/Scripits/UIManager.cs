using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text health;
    public Text ammo;

    public GameObject[] weapomIndicator = new GameObject[4];

    public void setHealth(string i)
    {
        health.text = i;
    }
    public void setAmmo(string i)
    {
        ammo.text = i;
    }
    public void setWeaponToDisplay(int e)
    {
        for (int i = 0; i < weapomIndicator.Length; i++)
        {
            weapomIndicator[i].SetActive(false);
        }
        for (int i = 0; i < weapomIndicator.Length; i++)
        {
            if (i == e)
            {
                weapomIndicator[i].SetActive(true);
            }
        }
    }

}
