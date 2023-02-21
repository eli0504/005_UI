using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float lifeTime = 2f; //tiempo en el que desaparecerá el Target
    private GameManager gameManager;
    public int points; //puntos de los targets
    public GameObject explosionParticle;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //conexión target con GameManager
        Destroy(gameObject, lifeTime); //autodestrucción
    }

    private void OnMouseDown() //detecta cuando hacemos click sobre el Target
    {
        if (!gameManager.isGameOver)
        {
            if (gameObject.CompareTag("Bad")) //si hacemos click en una calavera morimos
            {
                gameManager.GameOver();
            }else if (gameObject.CompareTag("Good"))
            {
                gameManager.UpdateScore(points);
            }
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDestroy() //un objeto ha sido destruido
    {
        gameManager.targetPositionsInScene.Remove(transform.position); //eliminamos la posición de la lista de posiciones ocupadas
    }
}
