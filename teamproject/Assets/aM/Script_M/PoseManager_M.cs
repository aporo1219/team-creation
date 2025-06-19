using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    public int x = 0;
    public int y = 0;

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

    [SerializeField] RawImage Map;              //マップ画像
    [SerializeField] Image ReStart;             //再開画像
    [SerializeField] Image Status;              //ステータス画像
    [SerializeField] Image OperationMethod;     //操作確認画像
    [SerializeField] Image FinishStage;         //ステージ終了画像
    [SerializeField] Image Passive;             //パッシブスキル画像
    [SerializeField] Image Active;              //アクティブスキル画像

    [SerializeField] GameObject Frame;          //セレクターのフレーム画像

    Vector3[] UI_pos;   //UIの位置
    int pose_start = 0;

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

        UI_pos = new Vector3[7];

        UI_pos[0] = new Vector3(-800, 250, 0);
        UI_pos[1] = new Vector3(-370, 250, 0);
        UI_pos[2] = new Vector3(-800, 90, 0);
        UI_pos[3] = new Vector3(-370, 90, 0);
        UI_pos[4] = new Vector3(-600, 700, 0);
        UI_pos[5] = new Vector3(2450, 135, 0);
        UI_pos[6] = new Vector3(2450, 370, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ReStart.rectTransform.position = new Vector3(UI_pos[0].x, UI_pos[0].y, 0);
        Status.rectTransform.position = new Vector3(UI_pos[1].x, UI_pos[1].y, 0);
        OperationMethod.rectTransform.position = new Vector3(UI_pos[2].x, UI_pos[2].y, 0);
        FinishStage.rectTransform.position = new Vector3(UI_pos[3].x, UI_pos[3].y, 0);
        Map.rectTransform.position = new Vector3(UI_pos[4].x, UI_pos[4].y, 0);
        Passive.rectTransform.position = new Vector3(UI_pos[5].x, UI_pos[5].y, 0);
        Active.rectTransform.position = new Vector3(UI_pos[6].x, UI_pos[6].y, 0);

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
            if (pose_start < 30)
            {
                pose_start++;
                int i;
                for (i = 0; i < 5; i++)
                {
                    UI_pos[i].x += 35;
                }
                for (; i < 7; i++)
                {
                    UI_pos[i].x -= 35;
                }
            }
            else
            {
                //ポーズ画面中はゲーム内の動きを止める
                Time.timeScale = 0.0f;
                //左スティックの力の動きを取ってくる
                selectValue = selectorAction.ReadValue<Vector2>();
                //左スティックが倒されたら
                if ((selectValue.x != 0 || selectValue.y != 0) && !push_selector)
                {
                    //セレクターの連続の動きを15秒に１度にする
                    if (selector_move_time >= 15) selector_move_time = 0;
                    //左スティックが左に倒されたらカーソルを左に動かす
                    if (selectValue.x < -0.5 && selector_move_time == 0 && selector_pos != 2)
                    {
                        selector_pos--;
                        if (selector_pos < 0) selector_pos = 0;
                        Debug.Log("selector_moveleft");
                    }
                    //左スティックが右に倒されたらカーソルを右に動かす
                    if (selectValue.x > 0.5 && selector_move_time == 0 && selector_pos != 1)
                    {
                        selector_pos++;
                        if (selector_pos > 3) selector_pos = 3;
                        Debug.Log("selector_moveright");
                    }
                    //左スティックが上に倒されたらカーソルを上に動かす
                    if (selectValue.y > 0.5 && selector_move_time == 0)
                    {
                        selector_pos -= 2;
                        if (selector_pos < 0) selector_pos += 2;
                        Debug.Log("selector_moveup");
                    }
                    //左スティックが下に倒されたらカーソルを下に動かす
                    if (selectValue.y < -0.5 && selector_move_time == 0)
                    {
                        selector_pos += 2;
                        if (selector_pos > 3) selector_pos -= 2;
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
                if (poseClick.WasPressedThisFrame() && !push_selector)
                {
                    push_selector = true;
                    Click_Select(selector_pos);
                }
                //決定ボタンが離されたら
                if (poseClick.WasReleasedThisFrame() && push_selector)
                {
                    push_selector = false;
                    Remove_Color(selector_pos);
                }
            }
        }
        //ポーズ画面を離れたらゲーム内を動かす
        else
        {
            Time.timeScale = 1.0f;

            UI_pos[0] = new Vector3(-800, 250, 0);
            UI_pos[1] = new Vector3(-370, 250, 0);
            UI_pos[2] = new Vector3(-800, 90, 0);
            UI_pos[3] = new Vector3(-370, 90, 0);
            UI_pos[4] = new Vector3(-600, 700, 0);
            UI_pos[5] = new Vector3(2450, 135, 0);
            UI_pos[6] = new Vector3(2450, 370, 0);
            pose_start = 0;
            selector_pos = 0;
        }

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
