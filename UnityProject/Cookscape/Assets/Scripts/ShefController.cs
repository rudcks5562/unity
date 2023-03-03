using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProject.Cookscape;

public class ShefController : MonoBehaviour
{


    InputHandler m_InputHandler;
    Animator m_PlayerAnimator;
    Weapon m_Weapon;

    bool isAttackReady;
    float AttackDelay;

    private void Awake()
    {
        m_InputHandler = GetComponent<InputHandler>();
        m_PlayerAnimator = GetComponent<Animator>();
        m_Weapon = GetComponentInChildren<Weapon>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
                isAttackReady = m_Weapon.rate < AttackDelay;

                if (isAttackReady)
                {
                    m_PlayerAnimator.SetTrigger("IsAttack");
                    m_Weapon.Use();
                    AttackDelay = 0;
                }
            }
        }
    }
}
