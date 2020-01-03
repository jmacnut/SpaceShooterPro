﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Application.Quit();
      }
   }
   public void LoadGame()
   {
      SceneManager.LoadScene(1);   // 1 = Game Scene
   }
}
