using UnityEngine;

public class EnemyModelSwitcher : MonoBehaviour
{
    [Header("モデル差し替え用")]
    [SerializeField] private Transform modelRoot; // モデルを入れ替える親
    [SerializeField] private GameObject[] modelPrefabs; // 差し替え候補
    [SerializeField] private RuntimeAnimatorController sharedAnimatorController; // 共通アニメーター
    private Animator currentAnimator;


    private GameObject currentModel;

    public void SwitchModel(int index)
    {
        if (index < 0 || index >= modelPrefabs.Length)
        {
            Debug.LogWarning("モデルのインデックスが無効です");
            return;
        }

        // 旧モデル削除
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // 新モデル生成
        currentModel = Instantiate(modelPrefabs[index], modelRoot);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;

        // Animatorの再設定
        Animator animator = currentModel.GetComponent<Animator>();
        if (animator != null && sharedAnimatorController != null)
        {
            animator.runtimeAnimatorController = sharedAnimatorController;
        }
        else
        {
            Debug.LogWarning("Animatorまたはコントローラーが未設定です");
        }
    }

    // アニメーション制御の例（ここに追記可）
    public void SetWalk(bool isWalking)
    {
        if (currentAnimator != null)
            currentAnimator.SetBool("Walk", isWalking);
    }

    public void SetAttack(bool IsAttack)
    {
        if (currentAnimator != null)
            currentAnimator.SetTrigger("Attack");
    }

    public void SetDeath(bool IsDeath)
    {
        if (currentAnimator != null)
            currentAnimator.SetTrigger("Death");
    }
}

