using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Script que dirige los hilos de todo el juego

    public GameObject[] targetPrefabs;

    private float minX = -3.75f;
    private float minY = -3.75F;
    private float distanceBetweenSquares = 2.5f;

    private int score; //puntuación

    public bool isGameOver;
    public float spawnRate = 1f; //cada cuanto aparecen los objectos
    public List<Vector3> targetPositionsInScene; //guarda las posiciones que estan ocupadas en la rejilla
    public Vector3 randomPos;

    public TextMeshProUGUI scoreText;

    //llamamos a la corrutina
    private void Start()
    {
        isGameOver = false;
        StartCoroutine("SpawnRandomTarget");
        scoreText.text = $"Score:{score}";
    }

    //Posición aleatoria en el centro de los cuadrados

    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minX + Random.Range(0, 4) * distanceBetweenSquares;
        float spawnPosY = minY + Random.Range(0, 4) * distanceBetweenSquares;
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    //corrutina para el spawner
    private IEnumerator SpawnRandomTarget() //instancia un objeto
    {
        while (!isGameOver) //mientras no estamos muertos
        {
            yield return new WaitForSeconds(spawnRate); //esperamos ese tiempo

            int randomIndex = Random.Range(0, targetPrefabs.Length); //qué elemento hacemos aparecer

            randomPos = RandomSpawnPosition(); //en qué cuadrado hacemos aparecer el objeto aleatorio

            //si la posición está ocupada en la lista busco otra posición libre
            while (targetPositionsInScene.Contains(randomPos))
            {
                randomPos = RandomSpawnPosition();
            }

            Instantiate(targetPrefabs[randomIndex], randomPos, targetPrefabs[randomIndex].transform.rotation);
            targetPositionsInScene.Add(randomPos); //añadimos a la lista de posiciones ocupadas la nueva posición que ocupamos
        }
    }

    public void UpdateScore(int newPoints)
    {
        score += newPoints;
        scoreText.text = $"Score: {score}";
    }

}
