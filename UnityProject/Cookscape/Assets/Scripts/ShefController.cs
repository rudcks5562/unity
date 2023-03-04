using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProject.Cookscape;

public class ShefController : MonoBehaviour
{
    [Tooltip("Shef's weapon")]
    [SerializeField] GameObject m_ShefWeapon;

    InputHandler m_InputHandler;
    Animator m_PlayerAnimator;
    CommonRaycast m_PlayerRaycast;

    Weapon m_Weapon;

    CatchOther m_CatchOther;

    bool IsAttackReady;
    bool IsCatching;
    float AttackDelay;

    private void Awake()
    {
        m_InputHandler = GetComponent<InputHandler>();
        m_PlayerAnimator = GetComponent<Animator>();
        m_PlayerRaycast = GetComponent<CommonRaycast>();
        m_Weapon = GetComponentInChildren<Weapon>();
        m_CatchOther = GetComponent<CatchOther>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AttackHandler();

        InteractiveHandler();
    }

    void InteractiveHandler()
    {
        RaycastHit hitData = m_PlayerRaycast.ShootRay(10.0f);

        Rigidbody seekObjectsBody = hitData.rigidbody;

        //hitData nothing
        if (seekObjectsBody == null )
        {
            return;
        }

        GameObject seekObject = seekObjectsBody.gameObject;

        //find player
        if (seekObject.CompareTag("Player"))
        {

            //Player is Catchable?????
            if (true && !IsCatching)
            {
                Debug.Log("Search Catchable Player!");

                //And You Click E????
                if (m_InputHandler.GetEKeyInputDown())
                {
                    //set Animation
                    m_PlayerAnimator.SetTrigger("DoCarry");
                    m_PlayerAnimator.SetBool("IsCarrying", true);

                    IsCatching = true;
                    m_ShefWeapon.SetActive(false);

                    m_CatchOther.DoCatch(seekObject);
                }
            }
        }

    }

    void AttackHandler()
    {
        AttackDelay += Time.deltaTime;
        if (m_InputHandler.GetAttackKeyInputDown())
        {
            if (m_Weapon == null)
            {
                Debug.Log("do not have weapon");
                return;
            }
            else
            {
                IsAttackReady = m_Weapon.rate < AttackDelay;

                if (IsAttackReady)
                {
                    m_PlayerAnimator.SetTrigger("IsAttack");
                    m_Weapon.Use();
                    AttackDelay = 0;
                }
            }
        }
    }
}
