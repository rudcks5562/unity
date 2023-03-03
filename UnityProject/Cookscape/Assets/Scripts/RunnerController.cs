using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProject.Cookscape;

public class RunnerController : MonoBehaviour
{
    GameObject playerEquipPoint;
    InputHandler m_InputHandler;
    CommonRaycast commonRaycast;

    private bool m_IsEquiped = false;

    GameObject child;

    // Start is called before the first frame update
    void Awake()
    {
        m_InputHandler = GetComponent<InputHandler>();
        commonRaycast = GetComponent<CommonRaycast>();
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitData = commonRaycast.ShootRay(10f);
        Debug.Log(hitData);

        // DO NOT HAVE EQUIPTMENT
        if (!m_IsEquiped) {
            if (hitData.collider) {
                Debug.Log("Something there...");

                // PRESS 'F' & THERE IS A EQUIPMENT
                if (hitData.collider.tag == "Equipment" && m_InputHandler.GetFKeyInputDown()) {
                    if (m_IsEquiped) return;
                    GameObject equipment = hitData.collider.gameObject;
                    Equip(equipment);
                }
            }
        } else { // HAVE EQUIPMENT
            // PRESS 'F'
            if (m_InputHandler.GetFKeyInputDown()) {
                Drop();
            }
        }
    }

    void Equip(GameObject other)
    { 
        if (m_IsEquiped) return;

        // Equipment's Physics System Off
        Collider[] objectColliders = other.GetComponents<Collider>();
        Rigidbody objectRigidbody = other.GetComponent<Rigidbody>();
        foreach(Collider collider in objectColliders) {
            collider.enabled = false;
        }
        objectRigidbody.isKinematic = true;

        // Equipment On My Hand
        other.transform.SetParent(playerEquipPoint.transform);
        other.transform.localPosition = Vector3.zero;
        other.transform.rotation = new Quaternion(0, 0, 0, 0);

        // Equiped State: true
        m_IsEquiped = true;
    }

    void Drop()
    {
        if (!m_IsEquiped) return;

        // Get Equipment On My Hand
        GameObject equipment = playerEquipPoint.GetComponentInChildren<Rigidbody>().gameObject;
        Rigidbody rigidbody = equipment.GetComponent<Rigidbody>();

        // Detach Equipment
        playerEquipPoint.transform.DetachChildren();

        // Equipment's Physics System ON
        Collider[] objectColliders = equipment.GetComponents<Collider>();
        Rigidbody objectRigidbody = equipment.GetComponent<Rigidbody>();
        foreach(Collider collider in objectColliders) {
            collider.enabled = true;
        }
        objectRigidbody.isKinematic = false;


        // float throwPower = 10f;
        // Vector3 throwAngle = transform.forward * 10f;
        // throwAngle.y = 5f;

        // rigidbody.AddForce(throwAngle * throwPower, ForceMode.Impulse);

        m_IsEquiped = false;   
    }
}

