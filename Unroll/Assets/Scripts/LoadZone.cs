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
    private int levelReached;
    private readonly List<string> idsCollected = new List<string>();

    private void Start()
    {
        LoadGame();
        levelToLoad = SceneManager.GetActiveScene().buildIndex;
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        if (levelReached < levelToLoad)
        {
            levelReached = levelToLoad;
        }
        animator.SetTrigger("FadeOut");
    }

    public int GetLevelToLoad()
    {
        return levelToLoad;
    }

    public int GetMaxLevel()
    {
        return levelReached;
    }

    public void Collect(string id)
    {
        idsCollected.Add(id);
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
        if (levelToLoad > levelReached)
        {
            data.levelReached = levelToLoad;
            levelReached = levelToLoad;
        }
        else
        {
            data.levelReached = levelReached;
        }

        if (data.collectibles == null)
        {
            foreach (string levelName in Const.LEVEL_NAMES)
                if (!levelName.Contains("Easy"))
                    foreach (Collectible.Metal metalName in Const.COLLECTIBLE_METALS)
                        data.collectibles.Add(levelName + "-" + metalName, true);
        }

        foreach (string id in idsCollected)
            data.collectibles.Add(id, true);

        idsCollected.Clear();

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
            if (data.levelReached > levelReached)
            {
                levelReached = data.levelReached;
            }
        }
        else
        {
            levelToLoad = 1;
            levelReached = 0;
            SaveGame();
        }
    }

    public void GetCollectiblesForLevel(string levelId)
    {
        idsCollected.Clear();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();

        foreach (string collectibleId in data.collectibles.Keys)
            if (collectibleId.Contains(levelId) && data.collectibles[collectibleId])
                idsCollected.Add(collectibleId);
    }

    public bool IsCollected(string id)
    {
        return idsCollected.Contains(id);
    }
        

    [Serializable]
    class SaveData
    {
        public int currentLevel;
        public int levelReached;
        public Dictionary<string, bool> collectibles;
    }

}
