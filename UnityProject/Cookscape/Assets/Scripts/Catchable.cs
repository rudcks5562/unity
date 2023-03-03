using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catchable : MonoBehaviour
{

    Rigidbody m_PlayerBody;
    BoxCollider m_Collider;

    private void Awake()
    {
        m_PlayerBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Catch")
        {
            Debug.Log("¾Æ¾ß");
        }
    }
}
