using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTarget_Collider : MonoBehaviour, IDamageable
{
    public int hp = 3;
    public int score_part = 1;
    public ParticleSystem ps;

    private void OnEnable()
    {      
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        ps.Pause();
        ps.gameObject.SetActive(false);
       
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("part of TGT DESTROYED");
            TearItSelf();
        }
    }

    private void TearItSelf()
    {
        //대충 파티클 재생
        ps.gameObject.SetActive(true);
        ps.Play();
        Destroy(gameObject, 0.3f);
    }


    public void OnDamage(Vector3 hitPoint, Vector3 hitNormal)
    {
        hp--;
        // 스코어 매니저에 사전에 등록해 놓은 부위별 점수 보내기
        if (hp <= 0)
        {
            Debug.Log("part of TGT DESTROYED");
            TearItSelf();
        }

    }


}
