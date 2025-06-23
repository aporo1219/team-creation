using System;
using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static PlayerController_y1;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    PlayerController_y1 PController_y;
    PlayerStatus PStatus;

    [SerializeField] private float[] AttackMotiontime = new float[8] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f };
    [SerializeField] private float[] AttackInputLimit = new float[8] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    [SerializeField] private float[] AttackInputStart = new float[8] { 0.2f, 0.2f, 0.2f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public int Attack(PlayerController_y1.AttackType attack)
    {
        switch (attack)
        { 
            case PlayerController_y1.AttackType.G1:

                StartCoroutine(AttackOperation(attack, AttackInputStart[0], AttackInputLimit[0], AttackMotiontime[0]));

                break;
            case PlayerController_y1.AttackType.G2:

                StartCoroutine(AttackOperation(attack, AttackInputStart[1], AttackInputLimit[1], AttackMotiontime[1]));

                break;
            case PlayerController_y1.AttackType.G3:

                StartCoroutine(AttackOperation(attack, AttackInputStart[2], AttackInputLimit[2], AttackMotiontime[2]));

                break;
            case PlayerController_y1.AttackType.GF:

                StartCoroutine(AttackOperation(attack, AttackInputStart[3], AttackInputLimit[3], AttackMotiontime[3]));

                break;
            case PlayerController_y1.AttackType.A1:

                StartCoroutine(AttackOperation(attack, AttackInputStart[4], AttackInputLimit[4], AttackMotiontime[4]));

                break;
            case PlayerController_y1.AttackType.A2:

                StartCoroutine(AttackOperation(attack, AttackInputStart[5], AttackInputLimit[5], AttackMotiontime[5]));

                break;
            case PlayerController_y1.AttackType.A3:

                StartCoroutine(AttackOperation(attack, AttackInputStart[6], AttackInputLimit[6], AttackMotiontime[6]));

                break;
            case PlayerController_y1.AttackType.AF:

                StartCoroutine(AttackOperation(attack, AttackInputStart[7], AttackInputLimit[7], AttackMotiontime[7]));

                break;
        }


        return 0;
    }

    public IEnumerator AttackOperation(PlayerController_y1.AttackType attack, float inputstart, float inputlimit, float motiontime)
    {
        float time = 0.0f;
        bool combo = false;
        PlayerController_y1.instance.animator.SetInteger("Attack", PlayerController_y1.instance.AttackNum);

        if (PlayerController_y1.instance.AttackNum > 3)
        {
            while (time < inputlimit)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (time < inputlimit)
            {
                time += Time.deltaTime;

                if (PlayerController_y1.instance.attackAction.WasPressedThisFrame())
                {
                    Debug.Log("コンボ受付");
                    combo = true;
                }

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
        }
        yield return null;
    }

}
