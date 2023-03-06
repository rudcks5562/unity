using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjController : MonoBehaviour, IInteractable
{
    [SerializeField] string type;
    // CHARGING SPEED PER ONE SECOND
    [SerializeField] float m_ChargingSpeed;
    float m_Gauge;

    private void Awake()
    {
        this.m_Gauge = 0;
    }
    
    public string getObjType()
    {
        return this.type;
    }

    public void ChargeGauge()
    {
        if (this.m_Gauge == 100) {
            Debug.Log("Completed");
            return;
        }
        this.m_Gauge += Time.deltaTime * m_ChargingSpeed;
    }

    public float GetGauge()
    {
        return this.m_Gauge;
    }
}
