using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    public AudioSource audioSource;
    public AudioClip playerShooting;
    public PlayerController playerController;
    public float duration = 2.5f;
    public float duration2 = 3f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.clip = playerShooting;
            audioSource.Play();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            StartCoroutine(Pickup(other));
        }

        if(other.CompareTag("PowerUpSpeed"))
        {
            StartCoroutine(Pickup2(other));
        }
    }

    IEnumerator Pickup(Collider other)
    {
        playerController.fireRate = 0.1f;

        yield return new WaitForSeconds(duration);

        playerController.fireRate = 0.25f;
    }

    IEnumerator Pickup2(Collider other)
    {
        playerController.speed = 20f;

        yield return new WaitForSeconds(duration2);

        playerController.speed = 10f;
    }
}
