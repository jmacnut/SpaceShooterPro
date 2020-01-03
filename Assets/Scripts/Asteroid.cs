//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   [SerializeField]
   private float _rotateSpeed;

   [SerializeField]
   private GameObject _explosionPrefab;

   [SerializeField]
   private SpawnManager _spawnManager;

   private void Start()
   {
      _rotateSpeed = 20.0f;
      _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
   }

   private void Update()
   {
      // rotate object on the z axis
      transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Laser")
      {
         Vector3 currentPosition = transform.position;

         Instantiate(_explosionPrefab, currentPosition, Quaternion.identity);
         Destroy(other.gameObject);
         _spawnManager.StartSpawning();
         Destroy(this.gameObject, 0.25f);
      }
   }
}
