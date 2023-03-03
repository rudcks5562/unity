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

        //3번 실행
        yield return new WaitForSeconds(0.4f);
        //4번 실행
        meleeArea.enabled = false;
        trailEffect.enabled = false;

        yield break;
    }

    //Use() 메인 루틴 => Swing() 서브루틴 => Use() 다음줄...;
    //Use() 메인루틴 + Swing() 코루틴 -> 즉 비동기


}
