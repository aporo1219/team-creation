using System;
using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static PlayerController_y1;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    private PlayerController_y1 PlayerCont;

    public GameObject Combo;
    public GameObject Finish;

    public float a = 1.0f;

    private float[] AttackMotionTime = new float[8] { 0.93f, 0.93f, 0.93f, 0.93f, 0.93f, 0.93f, 0.93f, 0.93f };
    private float[] AttackInputLimit = new float[8] { 0.43f, 0.53f, 0.73f, 0.0f, 0.43f, 0.53f, 0.73f, 0.0f };
    private float[] AttackMigrationTime = new float[8] { 0.33f, 0.43f, 0.43f, 0.0f, 0.33f, 0.43f, 0.43f, 0.0f };
    private float[] AttackStartTime = new float[8] { 0.13f, 0.16f, 0.13f, 0.16f, 0.13f, 0.16f, 0.13f, 0.16f };
    private float[] AttackEndTime = new float[8] { 0.23f, 0.26f, 0.23f, 0.23f, 0.23f, 0.26f, 0.23f, 0.23f };
    private float[] FinishAfterTime = new float[2] { 0.2f, 0.2f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerCont = GetComponent<PlayerController_y1>();

        Combo.SetActive(false);
        Finish.SetActive(false);

        instance = this;
    }

    public int Attack(AttackType attack)
    {
        if (!PlayerCont.onGround)
        {
            PlayerCont.rb.linearVelocity = new Vector3(PlayerCont.rb.linearVelocity.x, 0, PlayerCont.rb.linearVelocity.z);
        }

        PlayerCont.canMove = false;
        PlayerCont.canRotate = false;
        PlayerCont.canAction = false;

        switch (attack)
        { 
            case AttackType.G1:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[0], AttackInputLimit[0], AttackMotionTime[0], AttackStartTime[0], AttackEndTime[0]));

                break;
            case AttackType.G2:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[1], AttackInputLimit[1], AttackMotionTime[1], AttackStartTime[1], AttackEndTime[1]));

                break;
            case AttackType.G3:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[2], AttackInputLimit[2], AttackMotionTime[2], AttackStartTime[2], AttackEndTime[2]));

                break;
            case AttackType.GF:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[3], AttackInputLimit[3], AttackMotionTime[3], AttackStartTime[3], AttackEndTime[3]));

                break;
            case AttackType.A1:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[4], AttackInputLimit[4], AttackMotionTime[4], AttackStartTime[4], AttackEndTime[4]));

                break;
            case AttackType.A2:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[5], AttackInputLimit[5], AttackMotionTime[5], AttackStartTime[5], AttackEndTime[5]));

                break;
            case AttackType.A3:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[6], AttackInputLimit[6], AttackMotionTime[6], AttackStartTime[6], AttackEndTime[6]));

                break;
            case AttackType.AF:

                StartCoroutine(AttackOperation(attack, AttackMigrationTime[7], AttackInputLimit[7], AttackMotionTime[7], AttackStartTime[7], AttackEndTime[7]));

                break;
        }


        return 0;
    }

    public IEnumerator AttackOperation(AttackType attack, float migrationtime, float inputlimit, float motiontime, float start, float end)
    {
        bool ATK = false;
        float time = 0.0f;
        bool combo = false;
        bool finish = false;

        

        PlayerCont.animator.SetInteger("Attack", PlayerCont.AttackNum);
        yield return null;
        PlayerCont.animator.SetBool("Move", false);

        if (PlayerCont.AttackNum > 3)
        {
            finish = true;
            while (time < motiontime)
            {
                time += Time.deltaTime;

                if(end < time)
                {
                    Finish.SetActive(false);
                }
                else if (start < time)
                {
                    if(!Finish.activeInHierarchy)
                    {
                        PlayerCont.rb.linearVelocity = PlayerCont.transform.forward * 100;
                        if (!PlayerCont.onGround)
                        {
                            PlayerCont.rb.linearVelocity = new Vector3(PlayerCont.rb.linearVelocity.x, a, PlayerCont.rb.linearVelocity.z);
                        }
                    }
                    Finish.SetActive(true);
                }

                PlayerCont.rb.linearVelocity = new Vector3(PlayerCont.rb.linearVelocity.x * 0.8f, PlayerCont.rb.linearVelocity.y, PlayerCont.rb.linearVelocity.z * 0.8f);

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
                    if (!Combo.activeInHierarchy)
                    {
                        PlayerCont.rb.linearVelocity = PlayerCont.transform.forward * 30;
                        if (!PlayerCont.onGround)
                        {
                            PlayerCont.rb.linearVelocity = new Vector3(PlayerCont.rb.linearVelocity.x, a, PlayerCont.rb.linearVelocity.z);
                        }
                    }

                    Combo.SetActive(true);
                }

                if (PlayerCont.attackAction.WasPressedThisFrame() && ATK && time < inputlimit)
                {
                    Debug.Log("コンボ受付");
                    combo = true;
                }

                if(combo && time > migrationtime)
                {
                    break;
                }

                PlayerCont.rb.linearVelocity = new Vector3(PlayerCont.rb.linearVelocity.x * 0.8f, PlayerCont.rb.linearVelocity.y, PlayerCont.rb.linearVelocity.z * 0.8f);
                ATK = true;
                yield return null;
            }
            Debug.Log("コンボ受付終了");
        }

        if (combo)
        {
            Debug.Log("コンボ派生");
            PlayerCont.Attack();
        }
        else
        {
            Debug.Log("コンボリセット");
            PlayerCont.AttackNum = 0;
            PlayerCont.AttackState = AttackType.None;
            PlayerCont.animator.SetInteger("Attack", PlayerController_y1.instance.AttackNum);

            if(finish)
            {
                if(PlayerCont.onGround)
                {
                    for (float t = 0; t < FinishAfterTime[0];)
                    {
                        t += Time.deltaTime;
                        yield return null;
                    }
                }
                else
                {
                    for (float t = 0; t < FinishAfterTime[1];)
                    {
                        t += Time.deltaTime;
                        yield return null;
                    }
                }
            }

            PlayerCont.canMove = true;
            PlayerCont.canRotate = true;
            PlayerCont.canAction = true;
        }


        yield return null;
    }

}
