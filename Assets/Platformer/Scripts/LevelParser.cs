using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        // Go through the rows from bottom to top
        int row = 0;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            int column = 0;
            char[] letters = currentLine.ToCharArray();
            foreach (var letter in letters)
            {
                if (letter == 'x')
                {
                    var tempObject = Instantiate(rockPrefab, environmentRoot.transform, true);
                    tempObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                }

                if (letter == 'b')
                {
                    var tempObject = Instantiate(brickPrefab, environmentRoot.transform, true);
                    tempObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                }

                if (letter == 's')
                {
                    var tempObject = Instantiate(stonePrefab, environmentRoot.transform, true);
                    tempObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                }

                if (letter == '?')
                {
                    var tempObject = Instantiate(questionBoxPrefab, environmentRoot.transform, true);
                    tempObject.transform.position = new Vector3(column + 0.5f, row + 0.5f, 0f);
                }

                
                column++;
            }
            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
