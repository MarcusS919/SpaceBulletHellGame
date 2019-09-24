using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    //handles movements
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    // Handles projectile spawning
    public Rigidbody2D projectile;          // What to spawn
    public Transform projectileSpawnPoint;  // Where to spawn 
    public float projectileForce;           // How fast to fire it
    public float fireWaitTime;
    public bool AllowFire;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (fireWaitTime==0)
        {
            fireWaitTime = 1;
        }
        AllowFire = true;
    }
	
	// Update is called once per frame
	void Update () {


        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // Check if 'Fire1' input was pressed
        // - Left Ctrl or Left Click
        if (Input.GetButtonDown("Fire1"))
        {
            if (AllowFire == true)
            fire();

        }
    }

    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

    }



    void fire()
    {
        Debug.Log("Pew Pew");

        // Check if 'projectileSpawnPoint' and 'projectile' exist
        if (projectileSpawnPoint && projectile)
        {

            // Create the 'Projectile' and add to Scene
            Rigidbody2D temp = Instantiate(projectile, projectileSpawnPoint.position,
                projectileSpawnPoint.rotation);

            // Stop 'Character' from hitting 'Projectile'
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                temp.GetComponent<Collider2D>(), true);

            temp.AddForce(projectileSpawnPoint.up * projectileForce, ForceMode2D.Impulse);
        }
        AllowFire = false;
        StartCoroutine(FireWait());
    }

    IEnumerator FireWait()
    {
        yield return new WaitForSeconds(fireWaitTime);
        AllowFire = true;
    }
}
