using System;
using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static PlayerController_y1;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public GameObject Combo;
    public GameObject Finish;

    [SerializeField] private float[] AttackMotionTime = new float[8] { 1.2f, 1.2f, 1.2f, 1.2f, 1.2f, 1.2f, 1.2f, 1.2f };
    [SerializeField] private float[] AttackInputLimit = new float[8] { 0.7f, 0.8f, 1.0f, 0.0f, 0.35f, 0.35f, 0.35f, 0.0f };
    [SerializeField] private float[] AttackMigrationTime = new float[8] { 0.6f, 0.7f, 0.7f, 0.0f, 0.05f, 0.05f, 0.05f, 0.0f };
    [SerializeField] private float[] AttackStartTime = new float[8] { 0.4f, 0.43f, 0.4f, 0.43f, 0.05f, 0.05f, 0.05f, 0.0f };
    [SerializeField] private float[] AttackEndTime = new float[8] { 0.5f, 0.53f, 0.5f, 0.5f, 0.05f, 0.05f, 0.05f, 0.0f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Combo.SetActive(false);
        Finish.SetActive(false);

        instance = this;
    }

    public int Attack(PlayerController_y1.AttackType attack)
    {
        PlayerController_y1.instance.canMove = false;
        PlayerController_y1.instance.canAction = false;

        switch (attack)
        { 
            case PlayerController_y1.AttackType.G1:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[0], AttackInputLimit[0], AttackMotionTime[0], AttackStartTime[0], AttackEndTime[0]));

                break;
            case PlayerController_y1.AttackType.G2:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[1], AttackInputLimit[1], AttackMotionTime[1], AttackStartTime[1], AttackEndTime[1]));

                break;
            case PlayerController_y1.AttackType.G3:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[2], AttackInputLimit[2], AttackMotionTime[2], AttackStartTime[2], AttackEndTime[2]));

                break;
            case PlayerController_y1.AttackType.GF:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[3], AttackInputLimit[3], AttackMotionTime[3], AttackStartTime[3], AttackEndTime[3]));

                break;
            case PlayerController_y1.AttackType.A1:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[4], AttackInputLimit[4], AttackMotionTime[4], AttackStartTime[4], AttackEndTime[4]));

                break;
            case PlayerController_y1.AttackType.A2:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[5], AttackInputLimit[5], AttackMotionTime[5], AttackStartTime[5], AttackEndTime[5]));

                break;
            case PlayerController_y1.AttackType.A3:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[6], AttackInputLimit[6], AttackMotionTime[6], AttackStartTime[6], AttackEndTime[6]));

                break;
            case PlayerController_y1.AttackType.AF:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[7], AttackInputLimit[7], AttackMotionTime[7], AttackStartTime[7], AttackEndTime[7]));

                break;
        }


        return 0;
    }

    public IEnumerator AttackOperation(PlayerController_y1.AttackType attack, float migrationtime, float inputlimit, float motiontime, float start, float end)
    {
        bool ATK = false;
        float time = 0.0f;
        bool combo = false;

        

        PlayerController_y1.instance.animator.SetInteger("Attack", PlayerController_y1.instance.AttackNum);

        if (PlayerController_y1.instance.AttackNum > 3)
        {
            while (time < motiontime)
            {
                time += Time.deltaTime;

                if(end < time)
                {
                    Finish.SetActive(false);
                }
                else if (start < time)
                {
                    Finish.SetActive(true);
                }

                yield return null;
            }
        }
        else
        {
            while (time < motiontime)
            {
                time += Time.deltaTime;

                if (end < time)
                {
                    Combo.SetActive(false);
                }
                else if (start < time)
                {
                    Combo.SetActive(true);
                }

                if (PlayerController_y1.instance.attackAction.WasPressedThisFrame() && ATK && time < inputlimit)
                {
                    Debug.Log("コンボ受付");
                    combo = true;
                }

                if(combo && time > migrationtime)
                {
                    break;
                }

                ATK = true;
                yield return null;
            }
            Debug.Log("コンボ受付終了");
        }

        if (combo)
        {
            Debug.Log("コンボ派生");
            PlayerController_y1.instance.Attack();
        }
        else
        {
            Debug.Log("コンボリセット");
            PlayerController_y1.instance.AttackNum = 0;
            PlayerController_y1.instance.AttackState = AttackType.None;
            PlayerController_y1.instance.animator.SetInteger("Attack", PlayerController_y1.instance.AttackNum);

            PlayerController_y1.instance.canMove = true;
            PlayerController_y1.instance.canAction = true;
        }

        
        yield return null;
    }

}
