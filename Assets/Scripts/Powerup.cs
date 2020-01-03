//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
   [SerializeField]
   private float _speed;

   [SerializeField]
   private int powerupID;  //IDs for power ups: 0 = Triple Shot, 1 =  Speed, 2 = Shield

   [SerializeField]
   private AudioClip _powerupClip;

   private void Start()
   {
      _speed = 3.0f;
   }

   private void Update()
   {
      transform.Translate(Vector3.down * _speed * Time.deltaTime);
      if (transform.position.y <= 0)
      {
         Destroy(this.gameObject);
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         Player player = other.transform.GetComponent<Player>();

         AudioSource.PlayClipAtPoint(_powerupClip, transform.position);

         if (player != null)
         {
            switch (powerupID)
            {
               case 0:
                  {
                     player.TripleShotActive();
                     Debug.Log("Triple Shot Collected!");
                     break;
                  }
               case 1:
                  {
                     player.SpeedBoostActive();
                     Debug.Log("Speed Boost Collected!");
                     break;
                  }
               case 2:
                  {
                     player.ShieldActive();
                     Debug.Log("Shield Collected!");
                     break;
                  }
               default:
                  {
                     Debug.Log("Default Value: Invalid powerupID");
                     break;
                  }
            }
         }

         Destroy(this.gameObject);
      }
   }
}
