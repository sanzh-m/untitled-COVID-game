using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coughScript : MonoBehaviour
{
    public GameObject virusEffect;
    public float repeatTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("cough", 2.0f, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void cough()
    {
        Instantiate(virusEffect, transform.position, Quaternion.identity);
    }
}
