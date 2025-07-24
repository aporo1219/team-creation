using UnityEngine;

public class singleton : MonoBehaviour
{
    public static singleton instance; // インスタンスの定義
    public int number = 0; // この変数にアクセスしたい
    private void Awake()
    {
        // シングルトンの呪文
        if (instance == null)
        {
            // 自身をインスタンスとする
            instance = this;
        }
        else
        {
            // インスタンスが複数存在しないように、既に存在していたら自身を消去する
            Destroy(gameObject);
        }
    }
}
