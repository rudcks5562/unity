using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProject.Cookscape;

public class CatchOther : MonoBehaviour
{
    [Tooltip("Shef's back")]
    [SerializeField] Transform m_PlayerBack;

    GameObject catchedMan;

    private void Awake()
    {
    }

    public void DoCatch(GameObject _catchedMan)
    {
        catchedMan = _catchedMan;

        StopCoroutine("Catch");
        StartCoroutine("Catch");
    }

    IEnumerator Catch()
    {
        yield return null;

        Collider[] catchedManColliders = catchedMan.GetComponentsInChildren<Collider>();
        Rigidbody catchedManRigidbody = catchedMan.GetComponent<Rigidbody>();

        //catched man cant move
        foreach (Collider c in catchedManColliders)
        {
            c.enabled = false;
        }
        catchedManRigidbody.isKinematic = true;
        catchedManRigidbody.useGravity = false;
        //catchedMan.GetComponent<PlayerCommonController>().enabled = false;

        //wait for pickup animation
        yield return new WaitForSeconds(0.9f);

        //position change to my front
        Transform catchedManPosition = catchedMan.transform;
        catchedManPosition.SetParent(m_PlayerBack.transform);
        catchedManPosition.localPosition = new Vector3(1f, 0.8f, 0.5f);
        catchedManPosition.rotation = Quaternion.Euler(-90f, 0f, 90f);

        //end
        yield break;
    }
}
