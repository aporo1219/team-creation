using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    public int showing_pose;//ポーズが表示されてるか

    [SerializeField] GameObject pose_obj;       //ポーズキャンバスを読み込む
    [SerializeField] GameObject game_obj;       //ゲームキャンバスを読み込む
    InputAction poseAction;                     //ポーズ画面に切り替えるためのアクション
    InputAction selectorAction;                 //ポーズ画面内のセレクターの動き
    InputAction poseClick;                      //ポーズ画面で決定ボタンを押した

    Vector2 selectValue;                        //セレクターの上下左右の判定

    int selector_move_time = 0;                 //セレクターの連続で動くまでの時間

    public int selector_pos = 0;                //セレクターの位置

    bool push_selector = false;                 //決定ボタンが押されたか

    [SerializeField] Image BackGround;          //ポーズ画面の背景
    int alpha_time = 0;

    [SerializeField] GameObject Map;            //マップ画像
    [SerializeField] Image ReStart;             //再開画像
    [SerializeField] Image Status;              //ステータス画像
    [SerializeField] Image OperationMethod;     //操作確認画像
    [SerializeField] Image FinishStage;         //ステージ終了画像

    [SerializeField] GameObject Frame;          //セレクターのフレーム画像

    enum Selector_ID
    {
        RESTART,
        STATUS,
        OPERATIONMETHOD,
        FINISHSTAGE
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
        poseAction = InputSystem.actions.FindAction("Pose");
        selectorAction = InputSystem.actions.FindAction("PoseSelect");
        poseClick = InputSystem.actions.FindAction("PoseClick");

        ReStart.rectTransform.position = new Vector3(100, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Escapeキーの入力でポーズ画面の切り替えを行う
        //コントローラーのポーズボタンでポーズ画面の切り替えを行う
        if (Input.GetKeyDown(KeyCode.Escape) || poseAction.WasPressedThisFrame())
        {
            Change_Pose(showing_pose);
            showing_pose++;
        }

        //数値が１より大きくなったら０に戻す
        if (showing_pose > 1)
            showing_pose = 0;

        //ポーズ画面に移動したら数値が200になるまで背景の透明度を変更する
        if(showing_pose == 1 && alpha_time < 200)
        {
            alpha_time += 8;
            BackGround.color = new Color32(0, 0, 0, (byte)alpha_time);
        }
        //ポーズ画面から離れたら背景の透明度を０に戻す
        else if(showing_pose == 0)
        {
            alpha_time = 0;
            BackGround.color = new Color32(0, 0, 0, 0);
        }

        //ポーズ画面中の行動
        if (showing_pose == 1)
        {
            //ポーズ画面中はゲーム内の動きを止める
            Time.timeScale = 0.0f;
            //左スティックの力の動きを取ってくる
            selectValue = selectorAction.ReadValue<Vector2>();
            //左スティックが上下に倒されたらセレクトの切り替えを行う
            if (selectValue.y != 0 && !push_selector)
            {
                //セレクターの連続の動きを15秒に１度にする
                if (selector_move_time >= 15) selector_move_time = 0;
                //左スティックが上に倒されたらカーソルを上に動かす
                if (selectValue.y > 0.5 && selector_move_time == 0)
                {
                    selector_pos--;
                    if (selector_pos < 0) selector_pos = 0;
                    Debug.Log("selector_moveup");
                }
                //左スティックが下に倒されたらカーソルを下に動かす
                if (selectValue.y < -0.5 && selector_move_time == 0)
                {
                    selector_pos++;
                    if (selector_pos > 3) selector_pos = 3;
                    Debug.Log("selector_movedown");
                }
                //左スティックが上下に傾けられたらselector_move_timeを１ずつ増やす
                if (selectValue.y > 0.5 || selectValue.y < -0.5)
                    selector_move_time++;
            }
            else
            {
                //左スティックが傾けられてなかったらselector_move_timeを０にする
                selector_move_time = 0;
            }

            //決定ボタンが押されたら
            if (poseClick.WasPressedThisFrame())
            {
                push_selector = true;
                Click_Select(selector_pos);
            }
            //決定ボタンが離されたら
            if (poseClick.WasReleasedThisFrame())
            {
                push_selector = false;
                Remove_Color(selector_pos);
            }
        }
        //ポーズ画面を離れたらゲーム内を動かす
        else
            Time.timeScale = 1.0f;

        //セレクターの位置を表示する
        Show_Selector(selector_pos);
    }

    //ポーズの表示非表示を管理する関数
    void Change_Pose(int showing)
    {
        if (showing == 0)
        {
            ReStart.color = new Color32(255, 255, 255, 255);
            pose_obj.SetActive(true);
            game_obj.SetActive(false);
            Debug.Log("1");
        }
        else
        {
            pose_obj.SetActive(false);
            game_obj.SetActive(true);
            Debug.Log("2");
        }
    }

    //セレクターフレームの位置を変更する関数
    void Show_Selector(int spos)
    {
        switch(spos)
        {
            case (int)Selector_ID.RESTART:
                Frame.transform.position = ReStart.transform.position;
                break;
            case (int)Selector_ID.STATUS:
                Frame.transform.position = Status.transform.position;
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                Frame.transform.position = OperationMethod.transform.position;
                break;
            case (int)Selector_ID.FINISHSTAGE:
                Frame.transform.position = FinishStage.transform.position;
                break;
        }
    }

    //選択された場所の色を暗くする
    void Click_Select(int spos)
    {
        switch (spos)
        {
            case (int)Selector_ID.RESTART:
                ReStart.color = new Color32(100, 100, 100,255);
                break;
            case (int)Selector_ID.STATUS:
                Status.color = new Color32(100, 100, 100, 255);
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(100, 100, 100, 255);
                break;
            case (int)Selector_ID.FINISHSTAGE:
                FinishStage.color = new Color32(100, 100, 100, 255);
                break;
        }
    }

    //時間が過ぎたら色を戻す
    void Remove_Color(int spos)
    {
        switch (spos)
        {
            case (int)Selector_ID.RESTART:
                ReStart.color = new Color32(255, 255, 255, 255);
                Change_Pose(showing_pose);
                showing_pose++;
                break;
            case (int)Selector_ID.STATUS:
                Status.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(255, 255, 255, 255);
                break;
            case (int)Selector_ID.FINISHSTAGE:
                FinishStage.color = new Color32(255, 255, 255, 255);
                break;
        }
    }
}
