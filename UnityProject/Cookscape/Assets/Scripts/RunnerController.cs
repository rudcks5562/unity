using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityProject.Cookscape;

public class RunnerController : MonoBehaviour
{
    GameObject playerEquipPoint;

    // COMPONENTS
    GameManager m_GameManager;
    InputHandler m_InputHandler;
    CommonRaycast m_CommonRaycast;

    // MEMBER VARIABLES
    private bool m_IsEquiped = false;
    private bool m_IsInteracting = false;
    private Collider m_CurrentInteractingObj;


    void Awake()
    {
        m_GameManager = GetComponent<GameManager>();
        m_InputHandler = GetComponent<InputHandler>();
        m_CommonRaycast = GetComponent<CommonRaycast>();
        playerEquipPoint = GameObject.FindGameObjectWithTag("EquipPoint");
    }

    void Update()
    {
        RaycastHit hitData = m_CommonRaycast.ShootRay(5f);

        // YOU HAVE AN EQUIPTMENT & PRESS 'F'
        if (m_IsEquiped && m_InputHandler.GetFKeyInputDown()) {
            Drop();
        }

        // YOU ARE INTERACTING
        if (m_IsInteracting && !m_InputHandler.GetEKeyHeldDown()) {
            m_IsInteracting = false;
            m_CurrentInteractingObj = null;

            // HIDE GAUGE INFO
            m_GameManager.HideGaugeInfo();
        } else if (m_IsInteracting && m_InputHandler.GetEKeyHeldDown()) {
            IInteractable interactableObj = m_CurrentInteractingObj.GetComponent<Collider>().GetComponent<IInteractable>();
            if (interactableObj != null) {
                interactableObj.ChargeGauge();
                float gauge = interactableObj.GetGauge();
                m_GameManager.SetGauge(gauge);

                // SHOW GAUGE INFO
                m_GameManager.ShowGaugeInfo();

                // SHOW GAUGE VALUE ON THE GUIDE TEXT
                m_GameManager.ShowGuideText();
                m_GameManager.SetGuideText("Processing... " + (int)gauge + "%");
            }
        }

        // THERE IS SOMETHING OBJECT
        if (hitData.collider) {
            // IT IS EQUIPTMENT
            if (hitData.collider.tag == "Equipment") {
                if (!m_IsEquiped) {
                    // SHOW GUIDE TEXT
                    m_GameManager.ShowGuideText();
                    m_GameManager.SetGuideText("Press (F) To Equip");

                    // YOU DON'T HAVE AN EQUIPTMENT & PRESS 'F'
                    if (m_InputHandler.GetFKeyInputDown()) {
                        // IF YOU HAS AN EQUIPMENT
                        if (m_IsEquiped) return;

                        GameObject equipment = hitData.collider.gameObject;
                        Equip(equipment);
                        m_GameManager.HideGuideText();
                        m_GameManager.SetGuideText("");
                    }
                }
            } else if (hitData.collider.tag == "InteractiveObject") { // IT IS INTERACTIVE OBJECT
                if (!m_IsInteracting) {
                    // SHOW GUIDE TEXT
                    m_GameManager.ShowGuideText();
                    m_GameManager.SetGuideText("Press (E) To Interact");

                    // START INTERACTING
                    if (m_InputHandler.GetEKeyHeldDown()) {
                        m_IsInteracting = true;
                        m_CurrentInteractingObj = hitData.collider;
                    }
                }
            }
        } else {
            m_GameManager.HideGuideText();
            m_GameManager.SetGuideText("");

            m_GameManager.HideGaugeInfo();
        }
    }

    void Equip(GameObject other)
    { 
        if (m_IsEquiped) return;

        // Equipment's Physics System Off
        PhysicSystemToggle(other, false);

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

        DetachEquipment();
        PhysicSystemToggle(equipment, true);
        StartCoroutine("ThrowEquipment", rigidbody);

        m_IsEquiped = false;
    }

    void PhysicSystemToggle(GameObject target, bool isOn)
    {
        Collider[] objectColliders = target.GetComponents<Collider>();
        Rigidbody objectRigidbody = target.GetComponent<Rigidbody>();
        foreach(Collider collider in objectColliders) {
            collider.enabled = isOn;
        }
        objectRigidbody.isKinematic = !isOn;
    }

    void DetachEquipment()
    {
        playerEquipPoint.transform.DetachChildren();
    }

    IEnumerator ThrowEquipment(Rigidbody rigidbody)
    {
        // CONSTRAINTS ON ALL
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // THROW EQUIPMENT
        float throwPower = 0.3f;
        Vector3 throwAngle = transform.forward * 10f;
        throwAngle.y = 5f;
        rigidbody.AddForce(throwAngle * throwPower, ForceMode.Impulse);

        // CONSTRAINTS OFF
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}

