using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraMotor cameraMotor;
    [SerializeField] Camera mainCamera;
    GameObject currentPlayer;
    PlayerController curPlayerScript;

    void Awake()
    {
        curScore = 0;
        highScore = 0;
        ShowUI("mainmenu");
    }

    public void ToMainMenu(){
        ShowUI("mainmenu");
    }

    IEnumerator inGameCoroutine;
    public void StartGame()
    {
        curScore = 0f;
        InstantiatePlayer();
        cameraMotor.lookAt = currentPlayer.transform;
        if (inGameCoroutine != null)
        {
            inGameCoroutine = null;
        }
        inGameCoroutine = InGameCoroutine();
        StartCoroutine(inGameCoroutine);
        ShowUI("ingame");
    }
    GameObject InstantiatePlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        GameObject newPlayer = Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        currentPlayer = newPlayer;
        curPlayerScript = newPlayer.GetComponent<PlayerController>();
        curPlayerScript.mc = mainCamera;
        curPlayerScript.gm = this;
        return newPlayer;
    }
    public void GameOver()
    {
        StopCoroutine(inGameCoroutine);
        inGameCoroutine = null;
        Destroy(currentPlayer);

        ShowUI("gameover");
        if (curScore > highScore)
        {
            highScore = curScore;
        }
    }
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Text gameOverScoreDisplay;
    [SerializeField] GameObject inGameUI;
    [SerializeField] Text highScoreText;
    public void ShowUI(string UIName)
    {
        gameOverUI.SetActive(false);
        inGameUI.SetActive(false);
        mainMenuUI.SetActive(false);
        switch (UIName)
        {
            case "mainmenu":
                mainMenuUI.SetActive(true);
                highScoreText.text = "" + highScore;
                break;
            case "gameover":
                gameOverUI.SetActive(true);
                gameOverScoreDisplay.text = "" + curScore;
                break;
            case "ingame":
                inGameUI.SetActive(true);
                break;
        }
    }

    float timerStart;
    float timerEnd;
    float nextActorSpawn;
    IEnumerator InGameCoroutine()
    {
        timerStart = Time.time;
        timerEnd = timerStart += 60f;
        nextActorSpawn = timerStart + 5f;

        SpawnActor();
        SpawnActor();
        SpawnActor();
        SpawnActor();
        
        bool gameOver = false;
        while (!gameOver)
        {
            AddScore();
            
            if(Time.time > nextActorSpawn){
                SpawnActor();
                nextActorSpawn = Time.time + 10f;
            }

            if (Time.time > timerEnd)
            {
                gameOver = true;
                break;
            }
            yield return null;
        }
        GameOver();
        yield return null;
    }
    void AddScore()
    {
        float POVSizeMult = currentPlayer.transform.localScale.x;

        Vector3 distanceDiff = currentPlayer.transform.position - GetCenterPosition();
        float distanceFrom = new Vector3(distanceDiff.x, distanceDiff.y,0f).magnitude / POVSizeMult;
        float distanceFromPerfectMult = 1f / Mathf.Max(0.1f, distanceFrom);

        //currentPlayer.transform.position = GetCenterPosition();

        float zoomFrom = GetFarthestZoomDistance();
        float zoomFromPerfectMult = 1f / Mathf.Max(0.1f, zoomFrom);
        curScore += 1f * distanceFromPerfectMult * zoomFromPerfectMult;
        curScoreText.text = "" + (int)curScore;

        float green = 1f * (distanceFromPerfectMult * zoomFromPerfectMult);
        float red = 1f - green;
        curPlayerScript.POVSprite.color = new Color(red, green, 0f, curPlayerScript.POVSprite.color.a);
        Debug.Log($"mult distance: {distanceFromPerfectMult}\nmult zoom: {zoomFromPerfectMult}");
    }
    float GetFarthestZoomDistance()
    {
        float farthest = 0f;
        float topBound = curPlayerScript.topLeft.position.y;
        float botBound = curPlayerScript.botLeft.position.y;
        float leftBound = curPlayerScript.topLeft.position.x;
        float rightBound = curPlayerScript.botRight.position.x;
        foreach (Transform actorTran in allActorTrans)
        {
            
            if (topBound - actorTran.position.y > farthest)
            {
                farthest = topBound - actorTran.position.y;
            }
            if(actorTran.position.y > topBound){
                farthest = 999f;
            }
            
            if (actorTran.position.y - botBound > farthest)
            {
                farthest = actorTran.position.y - botBound;
            }
            if(actorTran.position.y < botBound){
                farthest = 999f;
            }
            
            if (rightBound - actorTran.position.x > farthest)
            {
                farthest = rightBound - actorTran.position.x;
            }
            if(actorTran.position.x > rightBound){
                farthest = 999f;
            }

            if (actorTran.position.x - leftBound > farthest)
            {
                farthest = actorTran.position.x - leftBound;
            }
            if(actorTran.position.x < leftBound){
                farthest = 999f;
            }
        }

        return Mathf.Abs(farthest);
    }

    float curScore;
    [SerializeField] Text curScoreText;
    float highScore;
    [SerializeField] GameObject actorPrefab;
    List<Transform> allActorTrans = new List<Transform>();
    void SpawnActor()
    {
        GameObject newActor = Instantiate(actorPrefab, new Vector3(0f, 0f, 0f), transform.rotation);
        allActorTrans.Add(newActor.transform);
    }

    public void SetActorSize(Vector3 scale){
        foreach (Transform actorTran in allActorTrans)
        {
            actorTran.localScale = scale;
        }
    }

    Vector3 GetCenterPosition()
    {
        float maxY = allActorTrans[0].transform.position.y;
        float minY = allActorTrans[0].transform.position.y;
        float maxX = allActorTrans[0].transform.position.x;
        float minX = allActorTrans[0].transform.position.x;
        foreach (Transform actorTran in allActorTrans)
        {
            if (actorTran.position.y > maxY)
            {
                maxY = actorTran.position.y;
            }
            if (actorTran.position.y < minY)
            {
                minY = actorTran.position.y;
            }
            if (actorTran.position.x > maxX)
            {
                maxX = actorTran.position.x;
            }
            if (actorTran.position.x < minX)
            {
                minX = actorTran.position.x;
            }
        }
        Vector3 output = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f, 0f);
        Debug.Log($"CenterPos: {output.x}, {output.y}");
        return output;
    }

    public void ExitApplication()// referenced by button objects
    {
        Application.Quit();
    }
}
