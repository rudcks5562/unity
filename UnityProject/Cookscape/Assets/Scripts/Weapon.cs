using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    private void Awake()
    {
        //meleeArea = GetComponent<BoxCollider>();
        //trailEffect = GetComponent<TrailRenderer>();
    }

    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.9f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //3�� ����
        yield return new WaitForSeconds(0.4f);
        //4�� ����
        meleeArea.enabled = false;
        trailEffect.enabled = false;

        yield break;
    }

    //Use() ���� ��ƾ => Swing() �����ƾ => Use() ������...;
    //Use() ���η�ƾ + Swing() �ڷ�ƾ -> �� �񵿱�


}
