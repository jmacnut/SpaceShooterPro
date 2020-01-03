﻿//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   [SerializeField]
   private float _speed;

   // must init here (not in start())
   private bool _isEnemyLaser = false;

   private void Start()
   {
      _speed = 8.0f;
   }
   private void Update()
   {
      if (_isEnemyLaser == false)
      {
         MoveUp();
      }
      else
      {
         MoveDown();
      }
   }

   private void MoveUp()
   {
      transform.Translate(Vector3.up * _speed * Time.deltaTime);

      if (transform.position.y > 11.0f)
      {
         if (transform.parent != null)
         {
            // destroys all children
            Destroy(transform.parent.gameObject);
         }

         Destroy(this.gameObject);
      }
   }

   private void MoveDown()
   {
      transform.Translate(Vector3.down * _speed * Time.deltaTime);

      if (transform.position.y <= 0f)
      {
         if (transform.parent != null)
         {
            // destroys all children
            Destroy(transform.parent.gameObject);
         }

         Destroy(this.gameObject);
      }
   }

   public void AssignEnemyLaser()
   {
      _isEnemyLaser = true;
      Debug.Log("Laser::AssignEnemyLaser(): _isEnemyLaser = " + _isEnemyLaser);
   }

   // ToDo: Find/Add sound asset and play on impact
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player" && _isEnemyLaser == true)
      {
         Player player = other.GetComponent<Player>();

         if (player != null)
         {
            player.Damage();
         }
      }
   }
}
