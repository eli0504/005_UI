using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Script que dirige los hilos de todo el juego

    public GameObject[] targetPrefabs; //array de objetos

    private float minX = -3.75f;
    private float minY = -3.75F;
    private float distanceBetweenSquares = 2.5f;

    private int lives = 3;

    private int score; //puntuaci�n total

    private float spawnRate = 1f; //cada cuanto aparecen los objectos
    private Vector3 randomPos;

    public bool isGameOver;
    public List<Vector3> targetPositionsInScene; //guarda las posiciones que estan ocupadas en la rejilla (LA LISTA ES FLEXIBLE)

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject startGamePanel;

    public bool hasPowerupShield;

    private void Start()
    {
        startGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    public void StartGame(int difficulty)
    {
        isGameOver = false;

        score = 0;
        UpdateScore(0);

        lives = 3;
        livesText.text = $"Lives: {lives}";
        spawnRate /= difficulty; //cuanto m�s dif�cil m�s r�pido aparecen los objetos

        StartCoroutine(SpawnRandomTarget());

        startGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); //el texto aparece cuando morimos
    }

    public void RestartGame() //RECARGA LA ESCENA ACTUAL
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //lo reinicia todo
    }

    //Posici�n aleatoria en el centro de los cuadrados

    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minX + Random.Range(0, 4) * distanceBetweenSquares;
        float spawnPosY = minY + Random.Range(0, 4) * distanceBetweenSquares;
        return new Vector3(spawnPosX, spawnPosY, 0);
    }

    //corrutina para el spawner (HACE APARECER OBJETOS)
    private IEnumerator SpawnRandomTarget() //instancia un objeto
    {
        while (!isGameOver) //mientras no estamos muertos
        {
            yield return new WaitForSeconds(spawnRate); //esperamos el tiempo que indica "spawnRate"

            int randomIndex = Random.Range(0, targetPrefabs.Length); //qu� elemento hacemos aparecer

            randomPos = RandomSpawnPosition(); //en qu� cuadrado hacemos aparecer el objeto aleatorio

            //si la posici�n est� ocupada en la lista busco otra posici�n libre
            while (targetPositionsInScene.Contains(randomPos))
            {
                randomPos = RandomSpawnPosition();
            }

            Instantiate(targetPrefabs[randomIndex], randomPos, targetPrefabs[randomIndex].transform.rotation); //se instancia un objeto en una posici�n libre aleatoria
            targetPositionsInScene.Add(randomPos); //a�adimos a la lista de posiciones ocupadas la nueva posici�n que ocupamos
        }
    }

    public void UpdateScore(int newPoints) //funci�n para ganar puntos
    {
        score += newPoints; //nuevos puntos a�adidos
        scoreText.text = $"Score: /n{score}";
    }

    public void MinusLife()
    {
        lives--;
        livesText.text = $"Lives: {lives}";
        if(lives <= 0)
        {
            GameOver();
        }
    }
}
