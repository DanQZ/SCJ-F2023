using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string gameState;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraMotor mainCamera;
    GameObject currentPlayer;
    PlayerController curPlayerScript;

    void Awake(){
        gameState = "menu";
        StartGame();
        curScore = 0;
        highScore = 0;
    }
    public void StartGame(){
        InstantiatePlayer();
        mainCamera.lookAt = currentPlayer.transform;
    }
    GameObject InstantiatePlayer(){
        if(currentPlayer != null){
            Destroy(currentPlayer);
        }
        GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0f,0f,0f), transform.rotation);
        currentPlayer = newPlayer;
        curPlayerScript = newPlayer.GetComponent<PlayerController>();
        return newPlayer;
    }
    public void ShowMenu(){

    }
    public void ShowGameOver(){

    }
    public void ShowGameUI(){

    }

    public int curScore;
    public int highScore;
    [SerializeField] GameObject actorPrefab;
    List<Transform> allActorTrans = new List<Transform>();
    void createNewActor(){
        GameObject newActor = Instantiate(actorPrefab, new Vector3(0f,0f,0f), transform.rotation);
    }
}
