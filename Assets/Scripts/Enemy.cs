//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField]
   private float _speed;
   private Player _player;
   private int _points;

   [SerializeField]
   private GameObject _laserPrefab;

   private float _fireRate;
   private float _canFire;

   private Animator _anim;
   private AudioSource _audioSource;

   private void Start()
   {
      _speed = 4.0f;
      _fireRate = 3.0f;
      _canFire = -1.0f;

      _player = GameObject.Find("Player").GetComponent<Player>();

      _audioSource = GetComponent<AudioSource>();

      if (_player == null)
      {
         Debug.LogError("The Player is NULL");
      }

      _points = 10;

      _anim = GetComponent<Animator>();

      if (_anim == null)
      {
         Debug.LogError("The Animator is NULL");
      }

   }

   private void Update()
   {
      CalculateMovement();
  
      if (Time.time > _canFire)
      {
         _fireRate = Random.Range(3.0f, 7.0f);
         _canFire = Time.time + _fireRate;

         GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
         Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

         for (int i = 0; i < lasers.Length; i++)
         {
            lasers[i].AssignEnemyLaser();
         }
      }
   }

   private void CalculateMovement()
   {

      transform.Translate(Vector3.down * _speed * Time.deltaTime);

      // if at bottom of screen, respawn at top at a new random x position
      if (transform.position.y <= 0)
      {
         float x_posRandom = Random.Range(-9.0f, 9.0f);
         float y_pos = 8.0f;  // use 10.0f once it works
         Vector3 newPos = new Vector3(x_posRandom, y_pos, 0);

         transform.position = newPos;
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Debug.Log("*** Enemy Hit by: " + other.transform.name);

      // order matters when destroying objects - object must exist to destroy other objects

      if (other.tag == "Player")
      {
         Debug.Log("Destroy: Player");

         if (_player != null)
         {
            _player.Damage();
         }

         Debug.Log("Destroy: Enemy");
         _anim.SetTrigger("OnEnemyDeath");
         _speed = 0;
         _audioSource.Play();
         Destroy(GetComponent<Collider2D>());
         Destroy(this.gameObject, 2.8f);
      }

      // ToDO: Enemy can destroy another Enemy
      if (other.tag == "Laser")
      {
         Debug.Log("Destroy: Laser");

         if (_player != null)
         {
            _player.AddScore(_points);
         }

         Debug.Log("Destroy: Enemy");
         _anim.SetTrigger("OnEnemyDeath");
         _speed = 0;
         _audioSource.Play();
         Destroy(GetComponent<Collider2D>());
         Destroy(this.gameObject, 2.8f);
      }
   }
}
