using UnityEngine;

public class singleton : MonoBehaviour
{
    public static singleton instance; // �C���X�^���X�̒�`
    public int number = 0; // ���̕ϐ��ɃA�N�Z�X������
    private void Awake()
    {
        // �V���O���g���̎���
        if (instance == null)
        {
            // ���g���C���X�^���X�Ƃ���
            instance = this;
        }
        else
        {
            // �C���X�^���X���������݂��Ȃ��悤�ɁA���ɑ��݂��Ă����玩�g����������
            Destroy(gameObject);
        }
    }
}
