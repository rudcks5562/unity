using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityProject.Cookscape {
    public class ReactWithObj : MonoBehaviour
    {
        GameObject playerEquipPoint;
        InputHandler inputHandler;
        CommonRaycast commonRaycast;

        GameObject child;

        // Start is called before the first frame update
        void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            commonRaycast = GetComponent<CommonRaycast>();
            playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hitData = commonRaycast.ShootRay(10f);
            Debug.Log(hitData);
            if (hitData.collider) {
                Debug.Log("Something there...");
                if (hitData.collider.tag == "Equipment" && inputHandler.GetEKeyInputDown()) {
                    GameObject equipment = hitData.collider.gameObject;
                    Equip(equipment);
                }
            }
        }

        void Equip(GameObject other)
        { 
            Collider[] objectColliders = other.GetComponents<Collider>();
            Rigidbody objectRigidbody = other.GetComponent<Rigidbody>();
            foreach(Collider collider in objectColliders) {
                collider.enabled = false;
            }
            objectRigidbody.isKinematic = true;

            other.transform.SetParent(playerEquipPoint.transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

}

