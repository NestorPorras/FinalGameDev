using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class static_enemy2 : MonoBehaviour
{
    Animator leAnimator;

    [SerializeField] float distance;
    [SerializeField] GameObject player;
    [SerializeField] int dashForce;
    [SerializeField] GameObject bullet;
    [SerializeField] float minTimeBetshoot;
    [SerializeField] AudioClip deathsound;

    private float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        leAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(deathsound, transform.position);
            leAnimator.SetTrigger("death");
        }
    }


    void Fire()
    {
        shootTime -= Time.deltaTime;

        if (player != null && Vector2.Distance(player.transform.position, transform.position) <= distance && shootTime <= 0)
        {
            if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x)
            {
                if(Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
                {
                    GameObject leBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                    leBullet.GetComponent<Rigidbody2D>().velocity = dashForce * Vector2.right;
                    leBullet.transform.localScale = new Vector2(-1, 1);
                }
                
            }
            else if(transform.localScale.x > 0 && player.transform.position.x < transform.position.x)
            {
                if(Mathf.Abs(transform.position.y - player.transform.position.y) < 1)
                {
                    GameObject leBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                    leBullet.GetComponent<Rigidbody2D>().velocity = dashForce * Vector2.left;
                    leBullet.transform.localScale = new Vector2(1, 1);
                }
                
            }
            
            shootTime = minTimeBetshoot;
        }
    }

    void AfterExplosion()
    {
        Destroy(this.gameObject);
    }
}
