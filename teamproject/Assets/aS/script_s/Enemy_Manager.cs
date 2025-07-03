using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    private static int Enemy_Number;//ID���蓖��
    private static Dictionary<int, Enemy_Status> Enemys = new();
   
    public static Enemy_Manager Instance;
    

    //�C���X�^���X�̑��
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    


    // Update is called once per frame
    void FixedUpdate()
    {
        
       
    }

   
    //ID�̓o�^
    public static int Entry_Enemy_ID(Enemy_Status Enemy)
    {
        int Enemy_Tag = Enemy_Number++;
        Enemys[Enemy_Tag] = Enemy;
        return Enemy_Tag ;
    }

    //ID�̓o�^����(�j�󂳂ꂽ�Ƃ��j
    public static void Delite_ListEnemy(int Enemy_Tag)
    {
        Enemys.Remove(Enemy_Tag);
       
    }

}
