using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator),  typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public float velocidad=0.7f;
    protected Transform trans;
    protected Animator anim;
    public GameObject prefabDisaroJugador;
    public AudioSource audioS;
    protected PlayerModel jugadorModelo;
    public Image imageVida;
    public Text pointsText;
    // Use this for initialization
    void Start (){
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        jugadorModelo = new PlayerModel();
        jugadorModelo.pVelocidad = velocidad;
        pointsText.text = "Puntos: " + jugadorModelo.pPuntuacion;
    }

    // Update is called once per frame
    void Update (){
        Vector2 vector2 = new Vector2();
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");
        vector2.Normalize(); //el modulo sea 1

        if(Input.GetButtonDown("Fire1")){//if(Input.GetAxisRaw("Fire1")==1){
            anim.SetBool("shoot", true);
            disparar();
        }else{
            anim.SetBool("shoot", false);
            if(audioS.loop){
                audioS.loop = false;
            }
        }

        mover(vector2);
    }

    void mover(Vector2 vector2){
        //calculamos las coordenadas mínima y máxima en coordenadas del munndo
        Vector2 vector2min=Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max=Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        vector2.x *= velocidad * Time.deltaTime;
        vector2.y *= velocidad * Time.deltaTime;
        vector2.x=Mathf.Clamp(trans.position.x + vector2.x, vector2min.x, vector2max.x);
        vector2.y=Mathf.Clamp(trans.position.y + vector2.y, vector2min.y, vector2max.y);
        trans.position = new Vector2(vector2.x, vector2.y);
    }

    void disparar(){
        //creo una bala en el cañon izquierdo y otra en el derecho
        Instantiate(prefabDisaroJugador, new Vector2(trans.GetChild(0).position.x, trans.GetChild(0).position.y), Quaternion.identity);
        Instantiate(prefabDisaroJugador, new Vector2(trans.GetChild(1).position.x, trans.GetChild(1).position.y), Quaternion.identity);
        if(!audioS.loop){
            audioS.loop = true;
            audioS.Play(0);
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag.Equals("Enemy")){
            jugadorModelo.pVida -= 20;
            imageVida.rectTransform.sizeDelta = new Vector2(jugadorModelo.pVida, imageVida.rectTransform.sizeDelta.y);
            collider.gameObject.GetComponent<Enemy>().kill();
        }

        if (collider.gameObject.tag.Equals("Bullet")){
            jugadorModelo.pVida -= 10;
            imageVida.rectTransform.sizeDelta = new Vector2(jugadorModelo.pVida, imageVida.rectTransform.sizeDelta.y);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.tag.Equals("Life")){
            if(jugadorModelo.pVida<100){
                jugadorModelo.pVida += 20;
                imageVida.rectTransform.sizeDelta = new Vector2(jugadorModelo.pVida, imageVida.rectTransform.sizeDelta.y);
            }
            Destroy(collider.gameObject);
        }
            
        if (jugadorModelo.pVida <= 0){
            Destroy(gameObject);
            SceneManager.LoadScene("FinalScene");
        }
    }

    public void EnemyDestroy(){
        jugadorModelo.pPuntuacion += 10;
        pointsText.text = "Puntos: " + jugadorModelo.pPuntuacion;
    }


}
