using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTarget_Collider : MonoBehaviour, IDamageable
{
    public int hp = 1;
    public int score_part = 1;
    public ParticleSystem ps;
    private Target target_parent;
    private bool isDestroyed = false;

    private void OnEnable()
    {      
        ps = GetComponentInChildren<ParticleSystem>();       
        ps.Play();
        ps.Pause();
        ps.gameObject.SetActive(false);     
    }

    public void OnParentAcquired()
    {
        target_parent = GetComponentInParent<Target>();
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
        // 게임매니저에 사전에 등록해 놓은 부위별 점수 보내기
        if (hp <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            GameManager.instance.AddPoint();
            target_parent.RemoveHitPoint();
            TearItSelf();
        }

    }


}
