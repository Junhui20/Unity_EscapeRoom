using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnDoorClose : MonoBehaviour
{
    public Material myMaterial;
    public GameObject doorSignal1;
    public GameObject doorSignal2;
    public GameObject myDoor;
    public GameObject myDoor2;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            {
            doorSignal1.GetComponent<MeshRenderer>().material = myMaterial;
            doorSignal2.GetComponent<MeshRenderer>().material = myMaterial;
            myDoor.SetActive(true);
            myDoor2.SetActive(true);
            }
    }
}
