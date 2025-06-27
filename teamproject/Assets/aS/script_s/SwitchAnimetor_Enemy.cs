using UnityEngine;

public class EnemyModelSwitcher : MonoBehaviour
{
    [Header("���f�������ւ��p")]
    [SerializeField] private Transform modelRoot; // ���f�������ւ���e
    [SerializeField] private GameObject[] modelPrefabs; // �����ւ����
    [SerializeField] private RuntimeAnimatorController sharedAnimatorController; // ���ʃA�j���[�^�[

    private GameObject currentModel;

    public void SwitchModel(int index)
    {
        if (index < 0 || index >= modelPrefabs.Length)
        {
            Debug.LogWarning("���f���̃C���f�b�N�X�������ł�");
            return;
        }

        // �����f���폜
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // �V���f������
        currentModel = Instantiate(modelPrefabs[index], modelRoot);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;

        // Animator�̍Đݒ�
        Animator animator = currentModel.GetComponent<Animator>();
        if (animator != null && sharedAnimatorController != null)
        {
            animator.runtimeAnimatorController = sharedAnimatorController;
        }
        else
        {
            Debug.LogWarning("Animator�܂��̓R���g���[���[�����ݒ�ł�");
        }
    }
}
