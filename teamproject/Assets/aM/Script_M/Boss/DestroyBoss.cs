using UnityEngine;

public class DestroyBoss : MonoBehaviour
{
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;

    [SerializeField] GameObject Boss;
    [SerializeField] BossStatus status;

    [SerializeField] AudioSource BossAS;
    [SerializeField] AudioSource EreaAS;

    private void Update()
    {
        //HP��0�ȉ��̎�
        if(status.HP <= 0)
        {
            //�ǂ�����
            if (wall1 != null)
                Destroy(wall1);
            if (wall2 != null)
                Destroy(wall2);

            //BGM��ύX
            BossAS.mute = true;
            EreaAS.mute = false;
            EreaAS.Play();

            //�����A�j���[�V���������ăI�u�W�F�N�g������
            Destroy(Boss);
        }
    }
}
