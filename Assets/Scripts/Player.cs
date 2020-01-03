using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   [SerializeField]
   private float _speed;
   [SerializeField]
   private float _speedMultiplier;

   [SerializeField]
   private GameObject _laserPrefab;

   [SerializeField]
   private GameObject _tripleshotPrefab;

   private Vector3 _offset;

   [SerializeField]
   private float _fireRate;
   private float _canFire;

   [SerializeField]
   private int _lives;

   private SpawnManager _spawnManager;

   private bool _isTripleShotActive;
   private bool _isSpeedBoostActive;
   private bool _isShieldActive;

   [SerializeField]
   private GameObject _shieldVisualizer;

   [SerializeField]
   private GameObject _leftEngine;
   [SerializeField]
   private GameObject _rightEngine;

   private float _waitTime;

   [SerializeField]
   private int _score;

   private UIManager _uiManager;

   [SerializeField]
   private AudioClip _laserSoundClip;
   private AudioSource _audioSource;

   private void Start()
   {
      _speed = 5.0f;
      _speedMultiplier = 3.0f;

      _offset = new Vector3(0, 0.8f, 0);   // laser offset position

      transform.position = new Vector3(0, 0, 0);
      _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
      _audioSource = GetComponent<AudioSource>();

      // cool down system
      _fireRate = 0.15f;
      _canFire = -1f;

      _lives = 3;

      _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

      if (_spawnManager == null)
      {
         Debug.LogError("The Spawn Manager is NULL.");
      }

      if (_uiManager == null)
      {
         Debug.Log("The UI Manager is NULL.");
      }

      if (_audioSource == null)
      {
         Debug.LogError("AudioSource on the Player is NULL");
      }
      else
      {
         _audioSource.clip = _laserSoundClip;
      }

      _isTripleShotActive = false;
      _isSpeedBoostActive = false;
      _isShieldActive = false;

      _shieldVisualizer.SetActive(false);

      _waitTime = 5.0f;

      _score = 0;

   }

   private void Update()
   {
      CalculateMovement();

      if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
      {
         Debug.Log("Space Key Pressed - Spawn Game Objects");
         FireLaser();
      }
   }

   private void CalculateMovement()
   {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");
      Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

      // one liner to move vertially and horizontally
      transform.Translate(direction * _speed * Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 5f), 0);

      // player horizontal bounds with wrap around
      if (transform.position.x <= -11.2f)
      {
         transform.position = new Vector3(11.2f, transform.position.y, 0);
      }
      else if (transform.position.x >= 11.2f)
      {
         transform.position = new Vector3(-11.2f, transform.position.y, 0);
      }
   }

   private void FireLaser()
   {
      _canFire = Time.time + _fireRate;

      if (_isTripleShotActive == true)
      {
         // powerup: instantiates 3 lasers (triple shot prefab)
         Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity);

      }
      else
      {
         // instantiates 1 laser
         Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
      }

      _audioSource.Play();
   }

   public void Damage()
   {
      if (_isShieldActive == true)
      {
         _isShieldActive = false;
         _shieldVisualizer.SetActive(false);

         return;
      }

      _lives--;

      _uiManager.UpdateLives(_lives);

      if (_lives == 2)
      {
         _leftEngine.SetActive(true);
      }
      if (_lives == 1)
      {
         _rightEngine.SetActive(true);
      }

      if (_lives < 1)
      {
         _spawnManager.OnPlayerDeath();
         Destroy(this.gameObject);
      }
   }

   public void TripleShotActive()
   {
      _isTripleShotActive = true;
      StartCoroutine(TripleShotPowerDownRoutine());
   }

   IEnumerator TripleShotPowerDownRoutine()
   {
      yield return new WaitForSeconds(_waitTime);
      _isTripleShotActive = false;
   }

   public void SpeedBoostActive()
   {
      _isSpeedBoostActive = true;
      _speed *= _speedMultiplier;
      StartCoroutine(SpeedBoostPowerDownRoutine());
   }

   IEnumerator SpeedBoostPowerDownRoutine()
   {
      yield return new WaitForSeconds(_waitTime);
      _isSpeedBoostActive = false;
      _speed /= _speedMultiplier;
   }

   public void ShieldActive()  // active for 1 enemy attach
   {
      _isShieldActive = true;
      _shieldVisualizer.SetActive(true);
   }

   public void AddScore(int points)
   {
      _score += points;
      _uiManager.UpdateScore(_score);
   }

}
