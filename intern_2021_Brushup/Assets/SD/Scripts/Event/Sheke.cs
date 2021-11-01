using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraSheke()
    {
        GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
    }
}
