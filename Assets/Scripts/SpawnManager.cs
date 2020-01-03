using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [SerializeField]
   private GameObject _enemyPrefab;

   [SerializeField]
   private GameObject _enemyContainer;

   [SerializeField]
   private GameObject[] powerups;

   private bool _stopSpawning = false;

   public void StartSpawning()
   {
      StartCoroutine(SpawnEnemyRoutine());
      StartCoroutine(SpawnPowerupRoutine());
   }

   IEnumerator SpawnEnemyRoutine()
   {
      yield return new WaitForSeconds(3.0f);

      while (_stopSpawning == false)
      {
         float waitTime = 5.0f;

         Vector3 posToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 10.0f, 0);
         GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
         newEnemy.transform.parent = _enemyContainer.transform;

         yield return new WaitForSeconds(waitTime);
      }
   }

   IEnumerator SpawnPowerupRoutine()
   {
      yield return new WaitForSeconds(3.0f);

      while (_stopSpawning == false)
      {
         float waitTime = Random.Range(15.0f, 21.0f);

         Vector3 posToSpawn = new Vector3(Random.Range(-9.5f, 9.5f), 10.0f, 0);
         int randomPowerUp = Random.Range(0, 3);   // powerups 0, 1, and 2
         Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);

         yield return new WaitForSeconds(waitTime);
      }
   }

   public void OnPlayerDeath()
   {
      _stopSpawning = true;
   }

}
