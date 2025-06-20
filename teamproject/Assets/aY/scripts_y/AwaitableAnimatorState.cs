using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AwaitableAnimatorState : MonoBehaviour
{
    private void Start()
    {
        _animator = GetComponent<Animator>();
        AnimationStateLoop().Forget();
    }

    private Animator _animator;
    private const string StateDefault = "default";
    public string State = StateDefault;
    // state��؂�ւ���Ԋu.���ꂪ�Z���قǑf����, �����قǊɂ₩�ɐ؂�ւ��.
    public const float DurationTimeSecond = 0.4f;

    private async UniTaskVoid AnimationStateLoop()
    {
        var token = this.GetCancellationTokenOnDestroy();
        var hashDefault = Animator.StringToHash(StateDefault);

        while (true)
        {
            // State�X�V�̂���Update�������҂�
            await UniTask.Yield();
            if (token.IsCancellationRequested)
            {
                break;
            }

            var hashExpect = Animator.StringToHash(State);
            var currentState = _animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.shortNameHash != hashExpect)
            {
                // DurationTimeSecond�̊Ԋu�������Animator��State��؂�ւ���
                _animator.CrossFadeInFixedTime(hashExpect, DurationTimeSecond);
                // �؂�ւ��Ă���Ԃ�currentState�͐؂�ւ���O��State���o�Ă���.
                // ���̂���DurationTimeSecond���߂���܂ő҂�
                await UniTask.Delay(TimeSpan.FromSeconds(DurationTimeSecond), cancellationToken: token);
                continue;
            }

            // state���I�����Ă����ꍇ��default�ɖ߂�
            if (currentState.shortNameHash != hashDefault && currentState.normalizedTime >= 1f)
            {
                SetState(StateDefault);
            }
        }
    }

    public void SetState(string nextState)
    {
        if (_animator.HasState(0, Animator.StringToHash(nextState)))
        {
            // ���݂���State�����󂯓����
            State = nextState;
        }
    }
}