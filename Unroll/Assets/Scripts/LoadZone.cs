using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadZone : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    private void Start()
    {
        levelToLoad = SceneManager.GetActiveScene().buildIndex;
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public int GetLevelToLoad()
    {
        return levelToLoad;
    }

    public void OnFadeComplete()
    {
        SaveGame();

        SceneManager.LoadScene(levelToLoad);
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.currentLevel = levelToLoad;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            levelToLoad = data.currentLevel;
        }
        else
        {
            levelToLoad = 1;
        }
    }

    [Serializable]
    class SaveData
    {
        public int currentLevel;
    }

}
