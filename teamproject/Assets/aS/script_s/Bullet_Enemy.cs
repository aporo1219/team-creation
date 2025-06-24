using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{ 
    [SerializeField] GameObject Bullet_OBJ;

    Vector3 Enemy_Pos;
    Vector3 Bullet_Pos;
 
    private float Bullet_Speed;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bullet_Speed = 10.0f;
       Bullet_OBJ.SetActive(false);
    }

    // Update is called once per frame
    

    //íeÇÃî≠éÀ
    public void Shot()
    {
        Bullet_OBJ.SetActive(true);
        Debug.Log("î≠éÀ");
        //à⁄ìÆ
        Bullet_OBJ.transform.Translate(Vector3.left * Time.deltaTime * Bullet_Speed);
        //ÇPÇOïbåoâﬂÇ≈è¡Ç¶ÇÈ
        Invoke(nameof(Delite), 1.0f);
    }

    //íeÇ™è¡Ç¶ÇÈ
    void Delite()
    {
        GameObject new_Bullet = Instantiate(Bullet_OBJ, Enemy_Pos, transform.rotation);
        Destroy(gameObject);
        
    }

    //ÉvÉåÉCÉÑÅ[Ç∆Ç†Ç¡ÇΩÇΩÇÁè¡Ç¶ÇÈ
    void OnTriggerEnter(Collider other)
    {
       if(gameObject.tag == ("Player"))
       {
            Delite();
       }
    }
        
}
