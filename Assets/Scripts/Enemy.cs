using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public float velocidad=0.2f;
    protected Rigidbody2D rigid;
    protected Transform trans;
    protected SpriteRenderer spriteRender;
    protected EnemyModel enemigoModelo;
    protected Animator animator;
    public GameObject prefabDisaroJugador;
    // Start is called before the first frame update
    void Start(){
        enemigoModelo = new EnemyModel();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0, -velocidad);
        trans = GetComponent<Transform>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(shootCoroutine());
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

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name.StartsWith("PlayerShoot")){
            Player player;
            player = GameObject.Find("Player").GetComponent<Player>();
            player.EnemyDestroy();
            kill();
        }
        if (enemigoModelo.pVida <= 0 || other.gameObject.name.StartsWith("Player")){
            kill();
        }
    }

    public void destruirInstancia(){
        Destroy(gameObject);
    }

    IEnumerator shootCoroutine(){
        float tiempoAleatorio = Random.Range(1f, 7f);
        yield return new WaitForSeconds(tiempoAleatorio);
        shoot();
    }

    void shoot(){
        Instantiate(prefabDisaroJugador, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
    }

    public void kill(){
        //desactivamos colisiones y mandamos morir
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        animator.SetBool("explode", true);
    }

}
