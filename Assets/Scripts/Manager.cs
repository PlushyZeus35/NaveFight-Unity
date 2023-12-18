using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject gameObjectEnemigo;
    public GameObject gameObjectLife;
    private IEnumerator corutina;
    public int minCounter = 1;
    public int maxCounter = 8;
    // Start is called before the first frame update
    void Start()
    {
        corutina = coroutinaAvisiones(2);
        StartCoroutine(corutina);
        Debug.Log("Creo aviones " + Time.time);
    }

    private IEnumerator coroutinaAvisiones(float waitTime){
        //The bottom-left of the viewport is (0,0); the topright is (1,1).
        Vector2 vector2min=Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max=Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float rangox = vector2max.x - vector2min.x;
        float rangoy = (vector2max.y-vector2min.y)*2 ;
        Vector2 vector2 = new Vector2();
        while (true){
            int aviones = Random.Range(minCounter, maxCounter);
            int lifes = Random.Range(0, 2);
            for (int i = 0; i < aviones; i++){
                GameObject gameObjectAvion = Instantiate(gameObjectEnemigo);
                vector2.x = vector2min.x + rangox * Random.value;
                vector2.y = vector2max.y + rangoy * Random.value;
                gameObjectAvion.transform.position = vector2;
            }

            for (int i = 0; i < lifes; i++){
                GameObject gameObjectVida = Instantiate(gameObjectLife);
                vector2.x = vector2min.x + rangox * Random.value;
                vector2.y = vector2max.y + rangoy * Random.value;
                gameObjectVida.transform.position = vector2;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
