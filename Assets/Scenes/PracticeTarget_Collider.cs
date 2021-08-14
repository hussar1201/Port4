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
        //���� ��ƼŬ ���
        ps.gameObject.SetActive(true);
        ps.Play();
        Destroy(gameObject, 0.3f);
    }


    public void OnDamage(Vector3 hitPoint, Vector3 hitNormal)
    {
        hp--;
        // ���ھ� �Ŵ����� ������ ����� ���� ������ ���� ������
        if (hp <= 0)
        {
            Debug.Log("part of TGT DESTROYED");
            TearItSelf();
        }

    }


}
