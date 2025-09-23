using UnityEngine;

public class DestroyBoss : MonoBehaviour
{
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;

    [SerializeField] GameObject Boss;
    [SerializeField] BossStatus status;
    [SerializeField] GameObject effect;

    [SerializeField] AudioSource BossAS;
    [SerializeField] AudioSource EreaAS;

    GameObject score;
    ScoreManager manager;

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

            Instantiate(effect, new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z), Quaternion.identity);

            score = GameObject.Find("Score");
            if (score != null)
                manager = score.GetComponent<ScoreManager>();
            if (manager != null)
                manager.Boss_kill++;

            //�����A�j���[�V���������ăI�u�W�F�N�g������
            Destroy(Boss);
        }
    }
}
