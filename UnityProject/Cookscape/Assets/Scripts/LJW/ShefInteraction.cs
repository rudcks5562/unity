using System.Collections;
using UnityEngine;

namespace UnityProject.Cookscape
{

    public class ShefInteraction : MonoBehaviour
    {
        
        [Tooltip("Shef's back")]
        [SerializeField] Transform m_PlayerBack;
        [Tooltip("Shef's weapon")]
        [SerializeField] GameObject m_ShefWeapon;

        CommonRaycast m_PlayerRaycast;
        InputHandler m_InputHandler;
        Rigidbody m_PlayerBody;
        Animator m_PlayerAnimator;

        GameObject catchedMan;

        bool IsCatching;

        private void Awake()
        {
            m_PlayerRaycast = GetComponent<CommonRaycast>();
            m_InputHandler = GetComponent<InputHandler>();
            m_PlayerBody = GetComponent<Rigidbody>();
            m_PlayerAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            IsCatching = false;
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hitData = m_PlayerRaycast.ShootRay(30.0f);

            if ( hitData.rigidbody == null )
            {
                return;
            }

            //See Player
            if ( hitData.rigidbody.gameObject.CompareTag("Player"))
            {

                //Player is Catchable?????
                if ( true && !IsCatching )
                {
                    Debug.Log("Search Catchable Player!");

                    //And You Click E????
                    if ( m_InputHandler.GetEKeyInputDown() )
                    {
                        //catched man's rigidbody
                        catchedMan = hitData.rigidbody.gameObject;

                        //더해야 할 일
                        //1. 무기 비활성화 하기
                        //2. 애니메이션 넣기
                        StopCoroutine("Catch");
                        StartCoroutine("Catch");
                    }
                }
            }
        }

        IEnumerator Catch()
        {
            yield return null;

            IsCatching = true;
            m_ShefWeapon.SetActive(false);

            m_PlayerAnimator.SetTrigger("DoCarry");
            m_PlayerAnimator.SetBool("IsCarrying", true);

            Collider[] catchedManColliders = catchedMan.GetComponentsInChildren<Collider>();
            Rigidbody catchedManRigidbody = catchedMan.GetComponent<Rigidbody>();
            
            //catched man cant move
            foreach(Collider c in catchedManColliders)
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
}
