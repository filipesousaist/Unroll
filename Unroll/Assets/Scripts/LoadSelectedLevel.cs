﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadSelectedLevel : MonoBehaviour
{
    public TMP_Text levelName;
    public int levelId;

    public LoadZone load;
    private string path;


    private void Start()
    {
        load = FindObjectOfType<LoadZone>();
        path = Application.dataPath + "/Scenes/";
    }

    public void LoadLevel()
    {
         load.FadeToLevel(levelId);
    }
}
