using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField]
   private Text _scoreText;

   [SerializeField]
   private Image _livesImg;

   [SerializeField]
   private Sprite[] _livesSprites;

   [SerializeField]
   private Text _gameOverText;

   [SerializeField]
   private Text _restartGameText;

   [SerializeField]
   private Text _quitGameText;

   [SerializeField]
   private GameManager _gameManager;

   private void Start()
   {
      _scoreText.text = "SCORE: " + 0;
      _gameOverText.gameObject.SetActive(false);
      _restartGameText.gameObject.SetActive(false);
      _quitGameText.gameObject.SetActive(false);
      _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

      if (_gameManager == null)
      {
         Debug.LogError("UIManager::Start() - GameManager is NULL");
      }
   }

   public void UpdateScore(int playerScore)
   {
      _scoreText.text = "Score: " + playerScore.ToString();
   }

   public void UpdateLives(int currentLives)
   {
      _livesImg.sprite = _livesSprites[currentLives];
      Debug.Log("Lives = " + currentLives);

      if (currentLives == 0)
      {
         GameOverSequence();
      }
   }

   private void GameOverSequence()
   {
      _gameOverText.gameObject.SetActive(true);
      _restartGameText.gameObject.SetActive(true);
      _quitGameText.gameObject.SetActive(true);
      StartCoroutine(GameOverFlickerRoutine());
   }

   IEnumerator GameOverFlickerRoutine()
   {
      float delayTime = 0.5f;
      while (true)
      {
         _gameManager.GameOver();
         _gameOverText.text = "GAME OVER";
         yield return new WaitForSeconds(delayTime);
         _gameOverText.text = "";
         yield return new WaitForSeconds(delayTime);
      }
   }
}
