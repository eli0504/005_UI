using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Difficulty : MonoBehaviour
{
    public int difficulty; //Dificultad
    private Button _button;
    private GameManager gameManager;

    private void Awake() 
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty); //cuando hago click al botón se llama a SetDiffilculty
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void SetDifficulty() //asigna a cada botón su dificultad
    {
        gameManager.StartGame(difficulty);
    }
}
