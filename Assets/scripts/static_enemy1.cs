using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class static_enemy1 : MonoBehaviour
{
    Animator leAnimator;

    [SerializeField] float distance;
    [SerializeField] GameObject player;
    [SerializeField] int dashForce;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip deathsound;

    // Start is called before the first frame update
    void Start()
    {
        leAnimator = GetComponent<Animator>();
        StartCoroutine(PlayerIsClose());
    }

    // Update is called once per frame
    void Update()
    {
    }


    IEnumerator PlayerIsClose()
    {
        while (true)
        {
            if (player != null && Vector2.Distance(player.transform.position, transform.position) <= distance)
            {
                Debug.Log("close");
                leAnimator.SetTrigger("shoot");
                Fire();
                yield return new WaitForSecondsRealtime(1f);
            }
            else
            {
                Debug.Log("don't");
                yield return new WaitForSecondsRealtime(1f);
            }
        }
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
        GameObject leBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        leBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(dashForce * 1f, dashForce * 1f);

        GameObject leBullet2 = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        leBullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(dashForce * -1f, dashForce * 1f);
    }

    void AfterExplosion()
    {
        Destroy(this.gameObject);
    }
}
