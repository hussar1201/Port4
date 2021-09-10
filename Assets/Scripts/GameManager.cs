using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Target[] arr_target;
    public int score;
    private const int score_per_hit = 10;
    public bool flag_practice_over
    {
        get;
        private set;
    }

    public bool flag_practice_start
    {
        get;
        private set;
    }

    public static GameManager instance
    {
        get
        {
            if (m_instance==null) m_instance = FindObjectOfType<GameManager>();
            return m_instance;
        }
        private set { }
    }
    private static GameManager m_instance; 


    private void Awake()
    {
        if(instance!=this)
        {
            Destroy(gameObject);
        }
        flag_practice_over = false;
    }


    public void StartPractice()
    {
        StartCoroutine(SetCountDown());
        score = 0;
    }

    IEnumerator SetCountDown()
    {
        for(int i = 3; i >0; i--) {
            UIManager.instance.ChangeTextState("Practice Start\n in\n" +" "+i);
            yield return new WaitForSeconds(1f);
        }
        SetTargets();
        UIManager.instance.ChangeTextState("Fire At Will!!");
    }

    public void SetTargets()
    {
        for(int i = 0;i< arr_target.Length;i++)
        {
            arr_target[i].SetUpTarget();
        }
    }

    public void AddPoint()
    {
        score += score_per_hit;
        
        UIManager.instance.ChangeTextState("Fire At Will!!\nScore: " + score);
        //if (score/ score_per_hit >= arr_target[0].num_point_hit)
        if (score / score_per_hit >= arr_target.Length * arr_target[0].num_point_hit)
        {
            UIManager.instance.ChangeTextState("All Targets neutralized.\n Total Score: " + score);
            flag_practice_over = true;
        }

    }

}
