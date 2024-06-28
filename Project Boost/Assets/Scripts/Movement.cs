using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.PlayerLoop;



public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 10000f;
    [SerializeField] private float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rocketParticles;
    
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    private void StartThrusting()
    {
        //rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        rb.AddForce(mainThrust * Time.deltaTime * transform.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            rocketParticles.Play();
        }
    }
    private void StopThrusting()
    {
        rocketParticles.Stop();
        audioSource.Stop();
    }

    void ProcessRotation()
    {
        StartRotating();
    }

    private void StartRotating()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate( Time.deltaTime * rotationThisFrame * Vector3.forward );
        rb.freezeRotation = false;
    } 
    
    
}
