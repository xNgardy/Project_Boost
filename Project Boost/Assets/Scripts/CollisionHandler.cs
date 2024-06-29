
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip successSound;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;
  
  float _levelLoadDelay = 2f;
  AudioSource _audioSource;
  private bool isTransition = false;
  private ParticleSystem _particleSystem;

  private void Start()
  {
    _audioSource = GetComponent<AudioSource>();
    _particleSystem = GetComponent<ParticleSystem>();
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransition)
    {
      return;
    }
    
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This item is friendly!");
        break;
      case "Finish":
        Debug.Log("YEY! I DID IT!");
        StartSuccessSequence();
        break;
      default:
        Debug.Log("Sorry, you blew up!");
        StartCrashSequence();
        break;
    }
  }
  void StartCrashSequence()
  {
    _audioSource.Stop();
    crashParticles.Play();
    isTransition = true;
    _audioSource.PlayOneShot(crashSound);
    GetComponent<Movement>().enabled = false;
    Invoke("ReLoadScene", _levelLoadDelay);
  }

  void StartSuccessSequence()
  {
    _audioSource.Stop();
    successParticles.Play();
    isTransition = true;
    _audioSource.PlayOneShot(successSound);
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", _levelLoadDelay);
  }
  private void ReLoadScene()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  public void LoadNextLevel()
  {
    int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(nextLevelIndex % SceneManager.sceneCountInBuildSettings);
  }
  
}
