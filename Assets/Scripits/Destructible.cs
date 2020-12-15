using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible: MonoBehaviour
{

    public GameObject destoryVersion;


    public void Destory()
    {

        Instantiate(destoryVersion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
