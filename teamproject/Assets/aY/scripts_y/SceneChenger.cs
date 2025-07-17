using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[���̕ύX�Ȃǂ𐧌䂷��֐�
/// </summary>
public class SceneChenger : MonoBehaviour
{
    public static SceneChenger instance = null;

    Image sr;
    private float cla;                      //�����x
    private float clarespeed = 0.03f;       //�ω����x

    private bool fadein = true;             //�t�F�[�h�C��/�t�F�[�h�A�E�g�؂�ւ�

    private int scenenum;                   //�؂�ւ���̃V�[���̃r���h�C���f�b�N�X
    private int selectscene = 11;           //�X�e�[�W�Z���N�g�̃r���h�C���f�b�N�X

    public static string gameState = "";    //�t�F�[�h�C��/�t�F�[�h�A�E�g�����ǂ���

    private bool SceneChange;
    private bool Gameend;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<SceneChenger>();

        SceneChange = false;
        Gameend = false;

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        gameState = "loading";
        sr = GetComponent<Image>();
        cla = sr.color.a;
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (SceneChange)
        {
            SceneManager.LoadScene(scenenum);
        }
    }

    public void NextScene()
    {
        StartCoroutine(FadeOut((SceneManager.GetActiveScene().buildIndex) + 1));
    }

    public void ChangeScene(int n)
    {
            StartCoroutine(FadeOut(n));
    }

    public void ReturnSelect()
    {
        if (gameState != "loading")
        {
            StartCoroutine(FadeOut(selectscene));
        }
            
    }

    public IEnumerator FadeIn()
    {
        cla = sr.color.a;
        while (cla > 0f && fadein)
        {
            cla -= clarespeed;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, cla);
            Debug.Log("sr.color = " + sr.color.a);
            yield return null;
        }
        gameState = "playing";
        Time.timeScale = 1;
    }

    public IEnumerator FadeOut(int i = -1)
    {
        fadein = false;
        Time.timeScale = 0;

        gameState = "loading";

        if (i != -1)
            scenenum = i;
        else
            Gameend = true;

        cla = sr.color.a;
        while (cla < 1f)
        {
            cla += clarespeed;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, cla);
            yield return null;
        }

        if (!Gameend)
            SceneChange = true;
        else
            Application.Quit();
    }

    public void Quit()
    {
        StartCoroutine(FadeOut());
    }

}
