using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityProject.Cookscape;

public class ShefController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Shef's weapon")]
    [SerializeField] GameObject m_ShefWeapon;

    [Tooltip("This is Jail")]
    [SerializeField] LayerMask m_JailMask;

    InputHandler m_InputHandler;
    Animator m_PlayerAnimator;
    CommonRaycast m_PlayerRaycast;
    GameManager m_GameManger;
    ShefTriggerVolume m_TriggerVolume;

    PlayerCommonController m_PlayerCommonController;

    Weapon m_Weapon;

    CatchOther m_CatchOther;

    bool IsAttackReady;
    bool IsCatching;
    float AttackDelay;

    private void Awake()
    {
        m_PlayerCommonController = GetComponent<PlayerCommonController>();

        m_PlayerAnimator = m_PlayerCommonController.PlayerAnimator;
        m_InputHandler = m_PlayerCommonController.inputHandler;

        m_TriggerVolume = GetComponent<ShefTriggerVolume>();

        m_PlayerRaycast = GetComponent<CommonRaycast>();
        m_Weapon = GetComponentInChildren<Weapon>();
        m_CatchOther = GetComponent<CatchOther>();
        m_GameManger = GetComponent<GameManager>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AttackHandler();

        RaycastInteractiveHandler();

        ImprisonHandler();
    }

    void ImprisonHandler()
    {
        if (IsCatching &&
            m_TriggerVolume.CanImprisonCatchee &&
            m_InputHandler.GetEKeyInputDown())
        {
            //throw
            m_CatchOther.Imprison();
            IsCatching = false;
            m_PlayerAnimator.SetBool("IsCarrying", false);
        }
    }

    void RaycastInteractiveHandler()
    {
        RaycastHit hitData = m_PlayerRaycast.ShootRay(5.0f);

        Rigidbody seekObjectsBody = hitData.rigidbody;

        //hitData nothing
        if (seekObjectsBody == null )
        {
            m_GameManger.HideGuideText();
            m_GameManger.setGuideText("");
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
                m_GameManger.setGuideText("Press (E) To Catch");
                m_GameManger.showGuideText();

                //And You Click E????
                if (m_InputHandler.GetEKeyInputDown())
                {
                    //set Animation
                    m_PlayerAnimator.SetTrigger("DoCarry");
                    m_PlayerAnimator.SetBool("IsCarrying", true);

                    //set Other Objects
                    IsCatching = true;
                    m_ShefWeapon.SetActive(false);
                    m_Weapon.enabled = false;

                    m_CatchOther.DoCatch(seekObject);

                    m_GameManger.HideGuideText();
                    m_GameManger.setGuideText("");
                }
            }
        }
    }

    void AttackHandler()
    {
        AttackDelay += Time.deltaTime;
        if (m_InputHandler.GetAttackKeyInputDown())
        {
            if (m_Weapon == null || !m_Weapon.enabled)
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
