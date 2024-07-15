using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private GameObject playerObj = null;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
        
    private void LateUpdate()
    {
        Vector3 pos = playerObj.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
