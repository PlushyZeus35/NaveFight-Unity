using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float velocidad = 0.2f;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRender;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -velocidad);
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        //The bottom-left of the viewport is (0,0); the topright is (1,1).
        Vector2 vector2min=Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max=Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //tengo en cuanta el borde inferior del sprite
        if (vector2min.y > spriteRender.bounds.max.y){
            Destroy(this.gameObject);
        }
    }
}
