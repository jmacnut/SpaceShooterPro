//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   [SerializeField]
   private bool _isGameOver;

   private void Start()
   {
      _isGameOver = false;
   }

   private void Update()
   {
      if (_isGameOver == true)
      {
         if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
         {
            // scene must be added to the current build settings
            //SceneManager.LoadScene("Game");   // 0 = "Game"
            SceneManager.LoadScene(1);          // loads faster with index
         }
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Application.Quit();  // executable or app mode, only
      }
   }

   public void GameOver()
   {
      Debug.Log("GameManager::GameOver() Called");
      _isGameOver = true;
   }
}
