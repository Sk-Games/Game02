using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour {

    public int score;
    public int highScore = 0;
    public static GameManager inst;

    
    [SerializeField] TMP_Text scoreText;

    [SerializeField] PlayerMovement playerMovement;


    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        scoreText = FindObjectOfType<TMP_Text>();

        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.scoreData;
        }
    }

    private void Update()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        Debug.Log(Application.persistentDataPath);
    }

    public void IncrementScore ()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake ()
    {
        if (inst != null)
        {
            Destroy(gameObject);
            return;

        }
        inst = this;
        //DontDestroyOnLoad(gameObject);
    }


    [System.Serializable]
    class SaveData
    {
        public string name;
        public int scoreData;   
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = "highScore";
        data.scoreData =  score;
        Debug.Log(data.scoreData);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    

    public void LoadScore() {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.scoreData;
        }
    }


}