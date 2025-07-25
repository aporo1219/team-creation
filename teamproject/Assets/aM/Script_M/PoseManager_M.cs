using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PoseManager_M : MonoBehaviour
{
    //public int x = 0;
    //public int y = 0;

    [SerializeField] ShowTaskSystem tasksystem;

    public int showing_pose;//ポーズが表示されてるか

    [SerializeField] GameObject pose_obj;       //ポーズキャンバスを読み込む
    [SerializeField] GameObject game_obj;       //ゲームキャンバスを読み込む
    InputAction poseAction;                     //ポーズ画面に切り替えるためのアクション
    InputAction selectorAction;                 //ポーズ画面内のセレクターの動き
    InputAction poseClick;                      //ポーズ画面で決定ボタンを押した
    InputAction poseCancel;

    Vector2 selectValue;                        //セレクターの上下左右の判定

    int selector_move_time = 0;                 //セレクターの連続で動くまでの時間

    public int selector_pos = 0;                //セレクターの位置

    bool push_selector = false;                 //決定ボタンが押されたか

    [SerializeField] Image BackGround;          //ポーズ画面の背景
    int alpha_time = 0;                         //透明度

    bool Click_UI = false;                      //ステータスか操作説明のUIがクリックされたか
    int comment_move_time = 0;                  //説明パネルが動く時間
    bool comment_move = false;                  //説明パネルが動いているか
    int comment_num = 0;                        //どの説明パネルが開かれているか
    bool push_cancel = false;                   //キャンセルボタンが押されたか

    [SerializeField] RawImage Map;              //マップ画像
    [SerializeField] Image ReStart;             //再開画像
    [SerializeField] Image Status;              //ステータス画像
    [SerializeField] Image OperationMethod;     //操作確認画像
    [SerializeField] Image FinishStage;         //ステージ終了画像
    [SerializeField] Image Passive;             //パッシブスキル画像
    [SerializeField] Image Active;              //アクティブスキル画像
    [SerializeField] Image Comment_Panel;       //説明パネル画像

    [SerializeField] GameObject Frame;          //セレクターのフレーム画像

    [SerializeField] Text Comment_Text;         //説明パネルの文字

    Vector3[] UI_pos;                           //UIの位置
    int pose_start = 0;                         //ポーズ画面のフェードイン＆フェードアウトの時間

    enum Selector_ID
    {
        RESTART,
        STATUS,
        OPERATIONMETHOD,
        FINISHSTAGE
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showing_pose = 0;
        poseAction = InputSystem.actions.FindAction("Pose");
        selectorAction = InputSystem.actions.FindAction("PoseSelect");
        poseClick = InputSystem.actions.FindAction("PoseClick");
        poseCancel = InputSystem.actions.FindAction("PoseCancel");

        UI_pos = new Vector3[8];

        UI_pos[0] = new Vector3(-800, 250, 0);
        UI_pos[1] = new Vector3(-370, 250, 0);
        UI_pos[2] = new Vector3(-800, 90, 0);
        UI_pos[3] = new Vector3(-370, 90, 0);
        UI_pos[4] = new Vector3(-600, 700, 0);
        UI_pos[5] = new Vector3(2450, 135, 0);
        UI_pos[6] = new Vector3(2450, 370, 0);
        UI_pos[7] = new Vector3(1400, 1350, 0);
    }
    //1350
    // Update is called once per frame
    void Update()
    {
        //UIの移動処理
        ReStart.rectTransform.position = UI_pos[0];
        Status.rectTransform.position = UI_pos[1];
        OperationMethod.rectTransform.position = UI_pos[2];
        FinishStage.rectTransform.position = UI_pos[3];
        Map.rectTransform.position = UI_pos[4];
        Passive.rectTransform.position = UI_pos[5];
        Active.rectTransform.position = UI_pos[6];
        Comment_Panel.rectTransform.position = UI_pos[7];

        //Escapeキーの入力でポーズ画面の切り替えを行う
        //コントローラーのポーズボタンでポーズ画面の切り替えを行う
        if ((Input.GetKeyDown(KeyCode.Escape) || poseAction.WasPressedThisFrame()) && !Click_UI && SceneChenger.gameState == "playing")
        {
            Comment_Text.text = tasksystem.task;
            showing_pose++;
        }

        if (pose_start == 0)
            Change_Pose(showing_pose);

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
        else if(showing_pose == 0 && alpha_time > 0)
        {
            alpha_time -= 8;
            BackGround.color = new Color32(0, 0, 0, (byte)alpha_time);
        }

        //ポーズ画面中の行動
        if (showing_pose == 1)
        {
            //ポーズ画面中はゲーム内の動きを止める
            Time.timeScale = 0.0f;
            //30フレームの間にUIを表示する
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
                UI_pos[i].y -= 19;
            }
            else
            {
                //左スティックの力の動きを取ってくる
                selectValue = selectorAction.ReadValue<Vector2>();
                //左スティックが倒されたら
                if ((selectValue.x != 0 || selectValue.y != 0) && !push_selector && !Click_UI)
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
                if (poseClick.WasPressedThisFrame() && !push_selector && !Click_UI)
                {
                    push_selector = true;
                    Click_Select(selector_pos);
                }
                //決定ボタンが離されたら
                if (poseClick.WasReleasedThisFrame() && push_selector && !Click_UI)
                {
                    push_selector = false;
                    Remove_Color(selector_pos);
                }

                //キャンセルボタンが押されたら
                if(poseCancel.WasPressedThisFrame() && !push_selector && comment_num == selector_pos)
                {
                    push_cancel = true;
                }
                //キャンセルボタンが離されたら
                if(poseCancel.WasReleasedThisFrame() && push_selector && !push_cancel)
                {
                    push_selector = false;
                }
                //UIがクリックされたら
                if (Click_UI && selector_pos != comment_num)
                {
                    if (comment_move_time < 30 && !comment_move)
                    {
                        comment_move_time++;
                        UI_pos[7].y += 19;
                    }
                    if (comment_move_time > 0 && comment_move)
                    {
                        comment_move_time--;
                        UI_pos[7].y -= 19;
                    }
                    if (comment_move_time == 30)
                    {
                        comment_move = true;
                        Change_Text(selector_pos);
                    }
                    if (comment_move_time == 0)
                    {
                        comment_move = false;
                        push_selector = false;
                        comment_num = selector_pos;
                    }
                }
                //UIがキャンセルされたら
                if(push_cancel && (selector_pos == 1 || selector_pos == 2))
                {
                    push_selector = true;
                    if (comment_move_time < 30 && !comment_move)
                    {
                        comment_move_time++;
                        UI_pos[7].y += 19;
                    }
                    if (comment_move_time > 0 && comment_move)
                    {
                        comment_move_time--;
                        UI_pos[7].y -= 19;
                    }
                    if (comment_move_time == 30)
                    {
                        comment_move = true;
                        Change_Text(0);
                    }
                    if (comment_move_time == 0)
                    {
                        comment_move = false;
                        push_selector = false;
                        comment_num = 0;
                        push_cancel = false;
                        comment_num = 0;
                        Click_UI = false;
                        Remove_Color(selector_pos);
                    }
                }
                else
                {
                    push_cancel = false;
                }
            }
        }
        //ポーズ画面を離れたらゲーム内を動かす
        else
        {
            //30フレームの間にUIを非表示にする
            if (pose_start > 0)
            {
                pose_start--;
                int i;
                for (i = 0; i < 5; i++)
                {
                    UI_pos[i].x -= 35;
                }
                for (; i < 7; i++)
                {
                    UI_pos[i].x += 35;
                }
                UI_pos[i].y += 19;
            }
            //UIが非表示になったらゲーム内を動かす
            if(pose_start == 0)
            {
                Time.timeScale = 1.0f;
                selector_pos = 0;
            }
        }

        //セレクターの位置を表示する
        Show_Selector(selector_pos);
    }

    //ポーズの表示非表示を管理する関数
    void Change_Pose(int showing)
    {
        if (showing == 1)
        {
            ReStart.color = new Color32(255, 255, 255, 255);
            pose_obj.SetActive(true);
            game_obj.SetActive(false);
        }
        else
        {
            pose_obj.SetActive(false);
            game_obj.SetActive(true);
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
                Click_UI = true;
                break;
            case (int)Selector_ID.OPERATIONMETHOD:
                OperationMethod.color = new Color32(100, 100, 100, 255);
                Click_UI = true;
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

    //説明パネルの文字を変える
    void Change_Text(int spos)
    {
        switch(spos)
        {
            case 0:
                Comment_Text.fontSize = 60;
                Comment_Text.text = tasksystem.task;
                break;
            case 1:
                Comment_Text.fontSize = 60;
                Comment_Text.text = "テスト２";
                break;
            case 2:
                Comment_Text.fontSize = 40;
                Comment_Text.text = "\n\n移動：左スティック｜攻撃：Xボタン\n\n" +
                                    "視点：右スティック｜ジャンプ：Aボタン\n\n" +
                                    "回避：RTボタン｜ダッシュ：左スティック押し込み\n\n";
                break;
        }
    }
}
