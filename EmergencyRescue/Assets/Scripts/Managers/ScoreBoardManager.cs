using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<Transform> highscoreEntryTransformList;
    private void Awake() 
    {
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highScores =  JsonUtility.FromJson<HighScores>(jsonString);

        for(int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for(int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if(highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    //Swap
                    HighScoreEntry temp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = temp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f; 

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int score = highScoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {   
        //Create Highscore Entry
        HighScoreEntry highScoreEntry = new HighScoreEntry{score = score, name = name};


        //Load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highScores =  JsonUtility.FromJson<HighScores>(jsonString);

        //Add new entry to highscores
        highScores.highScoreEntryList.Add(highScoreEntry);

        // Save updated highscores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


    //Represents a single high score entry
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;

        public string name;
    }
}
