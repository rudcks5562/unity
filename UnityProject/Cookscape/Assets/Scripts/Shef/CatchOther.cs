using System.Collections;
using UnityEngine;

namespace UnityProject.Cookscape
{

    public class CatchOther : MonoBehaviour
    {
        [Tooltip("Shef's back")]
        [SerializeField] Transform m_PlayerCatchPoint;

        GameObject catchedMan;

        public void DoCatch(GameObject _catchedMan)
        {
            catchedMan = _catchedMan;

            StopCoroutine("Catch");
            StartCoroutine("Catch");
        }

        public void Imprison()
        {
            if ( catchedMan == null )
            {
                Debug.Log("not have catched foods");
                return;
            }

            m_PlayerCatchPoint.transform.DetachChildren();
            PhysicSystemToggle(catchedMan, true);
            StopCoroutine("ThrowCatchee");
            StartCoroutine("ThrowCatchee", catchedMan.GetComponent<Rigidbody>());
        }

        void PhysicSystemToggle(GameObject target, bool isOn)
        {
            Collider[] catchedManColliders = target.GetComponentsInChildren<Collider>();
            Rigidbody catchedManRigidbody = target.GetComponent<Rigidbody>();

            //catched man cant move
            foreach (Collider c in catchedManColliders)
            {
                c.enabled = isOn;
            }
            catchedManRigidbody.isKinematic = !isOn;
        }
        IEnumerator ThrowCatchee(Rigidbody rigidbody)
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

        IEnumerator Catch()
        {
            yield return null;

            PhysicSystemToggle(catchedMan, false);

            //wait for pickup animation
            yield return new WaitForSeconds(0.9f);

            //position change to my front
            Transform catchedManPosition = catchedMan.transform;
            catchedManPosition.SetParent(m_PlayerCatchPoint.transform);
            catchedManPosition.localPosition = new Vector3(1f, 0.8f, 0.5f);
            catchedManPosition.rotation = Quaternion.Euler(-90f, 0f, 90f);

            //end
            yield break;
        }
    }
}